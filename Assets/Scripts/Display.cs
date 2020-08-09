using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    private string text =
            "+-------+\n" +
            "| Go... |\n" +
            "+-------+\n";
    private string userinput = "";
    public Text textUiComponent;

    void Start()
    {
        textUiComponent.text = text;
    }

    void Update()
    {
        if (Input.inputString == "\r")
        {
            PlayTypeSound();
            ProcessUserInput();
        }
        else if (Input.inputString == "\b")
        {
            if (userinput.Length > 0)
            {
                PlayTypeSound();
                userinput = userinput.Substring(0, userinput.Length - 1);
            }
        }
        else
        {
            if (Input.inputString.Length > 0)
            {
                PlayTypeSound();
                userinput += Input.inputString;
            }
        }
        textUiComponent.text = text + userinput + Blink();
    }

    private void PlayTypeSound()
    {
        // TODO
    }

    private void ProcessUserInput()
    {
        // TODO
    }

    private string Blink()
    {
        if (Time.time % 2 < 1)
        {
            return "_";
        }
        return "";
    }
}
