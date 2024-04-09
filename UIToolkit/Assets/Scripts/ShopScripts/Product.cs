using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Product
{
    [SerializeField] private string _entryName;
    [SerializeField] private int _priceProduct;
    [SerializeField] private int _lvl;
    [SerializeField] private UnityEvent _callback;

    public string EntryName { get => _entryName; set => _entryName = value; }  
    public int Lvl { get => _lvl; set => _lvl = value; }
    public UnityEvent Callback { get => _callback; set => _callback = value; }
    public int PriceProduct { get => _priceProduct; set => _priceProduct = value; }
}