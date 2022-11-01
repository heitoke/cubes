using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelWindow : MonoBehaviour
{
    public string Name;

    public bool isActive = false;

    public Button saveButton;

    public void DestroyModel()
    {
        Destroy(this.gameObject, 10);
    }
}
