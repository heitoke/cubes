using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public static List<ModelWindow> windows = new List<ModelWindow>();

    public ModelWindow[] models;

    public Player player;

    [Header("Modals")]
    public ModelChangeColor ChangeColor;


    public void OpenModel(ModelWindow modelPrefab)
    {
        modelPrefab.isActive = true;

        Instantiate(modelPrefab, this.transform);

        windows.Add(modelPrefab);

        SetActive(true);
    }

    public void SetActive(bool enable = false)
    {
        Canvas canvas = this.GetComponent<Canvas>();

        canvas.enabled = enable;

        /*if (!enable)
        {
            ModelWindow lastModel = windows[windows.Count - 1];

            if (lastModel == null) return;

            lastModel.DestroyModel();

            windows.RemoveAt(windows.Count - 1);
        }*/
    }
}
