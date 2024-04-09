using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(UIDocument))]

public class ShopSystem : MonoBehaviour
{
    [SerializeField] private List<SystemButton> _systemButtonEntries;
    [SerializeField] private List<Product> _productEntries;
    [SerializeField] private List<TextBar> _textEntries;
    [SerializeField] private VisualTreeAsset _buttonTemplateShop;
    [SerializeField] private VisualTreeAsset _windowBuyProductTemplate;

    private VisualElement _containerShopButton;
    private VisualElement _containerSystemButton;
    private VisualElement _containerText;
    private VisualElement _windowBuyProduct;
    private Label _cachedLabel;
    private int coinsSave = 1000;

    public List<SystemButton> SystemButtonEntries { get => _systemButtonEntries; set => _systemButtonEntries = value; }
    public List<Product> ProductEntries { get => _productEntries; set => _productEntries = value; }
    public List<TextBar> TextEntries { get => _textEntries; set => _textEntries = value; }
    public VisualTreeAsset ButtonTemplateShop { get => _buttonTemplateShop; set => _buttonTemplateShop = value; }
    public VisualTreeAsset WindowBuyProductTemplate { get => _windowBuyProductTemplate; set => _windowBuyProductTemplate = value; }

    void Start()
    {
        LoadCoins();
        StartCoroutine(CreateShop());
    }

    private IEnumerator CreateShop()
    {
       
        _containerShopButton = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("product-containar");
        _containerText = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("text-container");

        foreach (var item in ProductEntries.Cast<object>().Concat(SystemButtonEntries).Concat(TextEntries))
        {
            if (item is Product product)
            {
                AddProductButton(product);
            }
            else if (item is SystemButton systemButton)
            {
                AddSystemButton(systemButton);
            }
            else if (item is TextBar textBar)
            {
                AddTextBar(textBar);
            }

            yield return null;
        }
    }

    private void AddProductButton(Product product)
    {
        VisualElement newElemet = ButtonTemplateShop.CloneTree().contentContainer.Q<VisualElement>("shop-window");
        
        Button button = newElemet.Q<Button>("product-button");
        VisualElement _updateLvl = newElemet.Q<VisualElement>("update-lvl");
        _containerShopButton.Add(newElemet);

        button.Q<Label>("label-price").text = product.PriceProduct.ToString() + " coins";
        button.Q<Label>("label-lvl").text = "lvl. " + product.Lvl.ToString();
        button.clicked += delegate { OpenShopWindow(product, button, _updateLvl); };
    }

    private void AddSystemButton(SystemButton systemButton)
    {
        _containerSystemButton = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>(systemButton.VisualElementToClone);
        VisualElement newElemetButton = ButtonTemplateShop.CloneTree().contentContainer.Q<VisualElement>(systemButton.CloneVisualElement);
        Button button = newElemetButton.Q<Button>(systemButton.CloneButton); 
        
        Image buttonImage = button.Q<Image>("button-image");
        buttonImage.image = systemButton.NewTexture;

        _containerSystemButton.Add(newElemetButton);
        button.Q<Label>("label-system-button").text = systemButton.SystemButtonName;
        if (systemButton.SystemButtonName == "Home")
        {
            button.clicked += delegate { ExitShop(); };
        }
        else if (systemButton.SystemButtonName == "Back")
        {
            button.clicked += delegate { ExitShopToInventary(); };
        }
        else
        {
            button.clicked += delegate { Pass(); };
        }
        
    }

    private void AddTextBar(TextBar textBar)
    {
        
        VisualElement textElement = ButtonTemplateShop.CloneTree().contentContainer.Q<VisualElement>("text-window");
        Label label = textElement.Q<Label>("label-text-bar");

        Image buttonImage = textElement.Q<Image>("money-image");
        buttonImage.image = textBar.NewTexture;

        _containerText.Add(textElement);
        _cachedLabel = label;
        if (textBar.Coins != coinsSave && textBar.TextName == "money")
        {
            label.text = coinsSave.ToString();
            
        }
        else
        {
            label.text = textBar.Coins.ToString();
        }
        
    }

    public void BuyProduct(Product product, Button buttonProduct, VisualElement updateLvl)
    {
        if (product.Lvl < 3)
        {
            if (int.Parse(_cachedLabel.text) >= product.PriceProduct)
            {
                coinsSave = int.Parse(_cachedLabel.text);
                coinsSave -= product.PriceProduct;
                _cachedLabel.text = coinsSave.ToString();
                product.PriceProduct *= 2;
                product.Lvl++;

                buttonProduct.Q<Label>("label-lvl").text = "lvl. " + product.Lvl.ToString();
                buttonProduct.Q<Label>("label-price").text = product.PriceProduct.ToString() + " coins";
                Image image = ButtonTemplateShop.CloneTree().contentContainer.Q<Image>("lvl-image");
                updateLvl.Add(image);

                Debug.Log("Вы купили товар!");
                CloseShopWindow();

            }
            else
            {
                Debug.Log("У вас не хватает монет!");
                CloseShopWindow();
            }
        }
        else
        {
            Debug.Log("У вас максимальный уровень!");
            CloseShopWindow();
        }
        SaveCoins();

    }

    public void ExitShop()
    {
        SceneManager.LoadScene(0);
    }

    private void ExitShopToInventary()
    {
        SceneManager.LoadScene(2);
    }

    void OpenShopWindow(Product product, Button buttonProduct, VisualElement updateLvl)
    {
        _windowBuyProduct = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("window-buy-product");
        bool existsByName = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("confirmation-window") != null;
        if (existsByName == true)
        {
            CloseShopWindow();
        }
        
        VisualElement shopWindow = WindowBuyProductTemplate.CloneTree();
        shopWindow.name = "confirmation-window";
        Button button = shopWindow.Q<Button>("exit-window-button");
        button.clicked += delegate { CloseShopWindow(); };

        Button button1 = shopWindow.Q<Button>("yes-button");
        button1.clicked += delegate { BuyProduct(product, buttonProduct, updateLvl); };

        Button button2 = shopWindow.Q<Button>("no-button");
        button2.clicked += delegate { CloseShopWindow(); };
        shopWindow.AddToClassList("confirmation-window");
        _windowBuyProduct.Add(shopWindow);
    }

    void CloseShopWindow()
    {
        VisualElement shopWindow = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("confirmation-window");
        shopWindow.RemoveFromHierarchy();
    }

    void Pass()
    {
        Debug.Log("сработала заглушка");
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("PlayerCoins", coinsSave);
        
    }

    public void LoadCoins()
    {
        coinsSave = PlayerPrefs.GetInt("PlayerCoins", coinsSave); 
    }
}
