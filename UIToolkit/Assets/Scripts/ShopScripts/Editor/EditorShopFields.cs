using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Unity.VisualScripting;

[CustomEditor(typeof(ShopSystem))]
public class EditorShopFields : Editor
{
    private ShopSystem _shopSystem;
    

    public void OnEnable()
    {
        _shopSystem = (ShopSystem)target;
    }

    public override void OnInspectorGUI()
    {
        if (_shopSystem.SystemButtonEntries.Count > 0)
        {
            foreach (SystemButton systemButton in _shopSystem.SystemButtonEntries)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
                {
                    _shopSystem.SystemButtonEntries.Remove(systemButton);
                    break;
                }
                EditorGUILayout.EndVertical();

                systemButton.SystemButtonName = EditorGUILayout.TextField("Название кнопки", systemButton.SystemButtonName);
                systemButton.CloneVisualElement = EditorGUILayout.TextField("Клонируемый VisualElement", systemButton.CloneVisualElement);
                systemButton.CloneButton = EditorGUILayout.TextField("Клонируемая кнопка", systemButton.CloneButton);
                systemButton.VisualElementToClone = EditorGUILayout.TextField("VisualElement в который клониурем", systemButton.VisualElementToClone);
                systemButton.NewTexture = (Texture2D)EditorGUILayout.ObjectField("картинка", systemButton.NewTexture, typeof(Texture2D), false);
                EditorGUILayout.EndVertical();

            }
            
        }
        if (GUILayout.Button("Добавить системную кнопку")) _shopSystem.SystemButtonEntries.Add(new SystemButton());
        if (_shopSystem.ProductEntries.Count > 0)
        {
            foreach (Product product in _shopSystem.ProductEntries)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
                {
                    _shopSystem.ProductEntries.Remove(product);
                    break;
                }
                EditorGUILayout.EndVertical();

                product.EntryName = EditorGUILayout.TextField("Product name", product.EntryName);
                product.PriceProduct = EditorGUILayout.IntField("Product price", product.PriceProduct);
                product.Lvl = EditorGUILayout.IntField("Product lvl", product.Lvl);
                EditorGUILayout.EndVertical();
            }
            
        }
        if (GUILayout.Button("Добавить продукт")) _shopSystem.ProductEntries.Add(new Product());
        if (_shopSystem.TextEntries.Count > 0)
        {
            foreach (TextBar textBar in _shopSystem.TextEntries)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
                {
                    _shopSystem.TextEntries.Remove(textBar);
                    break;
                }
                EditorGUILayout.EndVertical();
                textBar.TextName = EditorGUILayout.TextField("Наименование", textBar.TextName);
                textBar.Coins = EditorGUILayout.IntField("Количество монет", textBar.Coins);
                textBar.NewTexture = (Texture2D)EditorGUILayout.ObjectField("картинка", textBar.NewTexture, typeof(Texture2D), false);
                EditorGUILayout.EndVertical();
            }

        }
        if (GUILayout.Button("Добавить текстовое поле")) _shopSystem.TextEntries.Add(new TextBar());
        else EditorGUILayout.LabelField("Нет элементов в списке");
        
        if (GUI.changed) SetObjectDirty(_shopSystem.gameObject);
    }

    public static void SetObjectDirty(GameObject shop)
    {
        EditorUtility.SetDirty(shop);
        EditorSceneManager.MarkSceneDirty(shop.scene);
    }
}
