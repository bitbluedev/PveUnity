using Pve.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public Text textUiComponent;
    bool running = true;

    void Start()
    {
        World.CurrentState = World.NewGameHandlerInstance;
    }

    void Update()
    {
        if (!running)
        {
            return;
        }
        if (World.Exit)
        {
            running = false;
            Application.Quit();
        }

        World.CurrentState.Execute();

        textUiComponent.text = World.Text + World.UserInput + Blink();
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
