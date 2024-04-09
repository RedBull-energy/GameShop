using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TextBar
{
    [SerializeField] private string textName;
    [SerializeField] private int coins;
    [SerializeField] private Texture2D newTexture;
    [SerializeField] private UnityEvent callback;

    public string TextName { get => textName; set => textName = value; }
    public int Coins { get => coins; set => coins = value; }
    public Texture2D NewTexture { get => newTexture; set => newTexture = value; }
    public UnityEvent Callback { get => callback; set => callback = value; }
}