using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[System.Serializable]

// класс для создания одной кнопки 
public class MenuEntry
{
    public string EntryName;

    public UnityEvent Callback;
}

[RequireComponent(typeof(UIDocument))]

public class MenuSystem : MonoBehaviour
{
    [SerializeField] private List<MenuEntry> _menuEntries; // лист кнопок

    [SerializeField] private float _transitionDuration; //

    [SerializeField] private EasingMode _easing; // способ изменения (easing)

    [SerializeField] private float _buttonDelay; // задержка перед появлением
                                                 // 
    [SerializeField] private string _animatedClass;

    [SerializeField] private VisualTreeAsset _buttonTemplate;

    

    private VisualElement _container; // контейнер, в который создаются все кнопки

    private WaitForSeconds _pause;

    private List<TimeValue> _durationValues;
    private StyleList<EasingFunction> _easingValues;
    private void Start()
    {
        _pause = new WaitForSeconds(_buttonDelay);
        _durationValues = new List<TimeValue>() { new TimeValue(_transitionDuration, TimeUnit.Second) };
        _easingValues = new StyleList<EasingFunction>(new List<EasingFunction> { new EasingFunction(_easing) });
        StartCoroutine(CreateMenu());
    }


    private IEnumerator CreateMenu()
    {
        _container = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("container");
        foreach (MenuEntry entry in _menuEntries)
        {
            VisualElement newElemet = _buttonTemplate.CloneTree(); // создаем копию
            Button button = newElemet.Q<Button>("menu-button");
            
            _container.Add(newElemet);
            newElemet.style.transitionDuration = _durationValues;
            newElemet.style.transitionTimingFunction = _easingValues;

            newElemet.AddToClassList(_animatedClass);
            yield return null;
            
            newElemet.RemoveFromClassList(_animatedClass);
            button.text = entry.EntryName;
            button.clicked += delegate { OnClick(entry); };
            yield return _pause;
        }
    }

    private void OnClick(MenuEntry entry)
    {
        Debug.Log($"Button clicked: {entry.EntryName}");
        
        SceneManager.LoadScene(1);
        entry.Callback.Invoke();
    }
}
