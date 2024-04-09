using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SystemButton
{
    [SerializeField] private string _systemButtonName;
    [SerializeField] private string _cloneVisualElement;
    [SerializeField] private string _cloneButton;
    [SerializeField] private string _visualElementToClone;
    [SerializeField] private Texture2D _newTexture;
    [SerializeField] private UnityEvent _callback;

    public string SystemButtonName { get => _systemButtonName; set => _systemButtonName = value; }
    public string CloneVisualElement { get => _cloneVisualElement; set => _cloneVisualElement = value; }
    public string CloneButton { get => _cloneButton; set => _cloneButton = value; }
    public string VisualElementToClone { get => _visualElementToClone; set => _visualElementToClone = value; }
    public Texture2D NewTexture { get => _newTexture; set => _newTexture = value; }
    public UnityEvent Callback { get => _callback; set => _callback = value; }
}
