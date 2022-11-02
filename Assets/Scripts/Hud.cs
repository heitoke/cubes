using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Button setColorButton;    

    private void Start()
    {
        setColorButton.onClick.AddListener(() =>
        {
            Window window = GameObject.FindWithTag("Window").GetComponent<Window>();

            window.GetComponent<Canvas>().enabled = true;

            window.ChangeColor.saveButton.onClick.AddListener(() =>
            {
                window.player.CmdSetColor(window.ChangeColor.colorInput.text);

                window.GetComponent<Canvas>().enabled = false;
            });
        });
    }

    void test()
    {
        Debug.Log(123);
    }
}
