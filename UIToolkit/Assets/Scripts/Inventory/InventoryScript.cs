
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]

public class InventoryScript : MonoBehaviour
{
    private VisualElement _rootVisualElement;


    void Start()
    {
        CreateWindow();

    }


    public void CreateWindow()
    {
        _rootVisualElement = GetComponent<UIDocument>().rootVisualElement;


        DragAndDropManipulator manipulator =
        new(_rootVisualElement.Q<VisualElement>("object"));
    }
}
