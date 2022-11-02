using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelChangeColor : MonoBehaviour
{
    public Color color;

    public Button saveButton;
    public InputField colorInput;

    public delegate void GetColor();

    public event GetColor save;

    private void Start()
    {
        saveButton.onClick.AddListener(() =>
        {
            save?.Invoke();
        });
    }
}
