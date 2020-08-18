using Pve.GameEntity.Enemy;
using Pve.Util;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public Text textUiComponent;
    public Sprite bear;
    public Sprite dog;
    public Sprite giant;
    public Image rightPage;
    private bool running = true;

    void Start()
    {
        World.CurrentState = World.NewGameHandlerInstance;
        World.BearSprite = bear;
        World.DogSprite = dog;
        World.GiantSprite = giant;
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

        if (World.Enemy != null)
        {
            if (World.Enemy is EnemyGiant)
            {
                rightPage.sprite = giant;
            }
            else if (World.Enemy is EnemyBear)
            {
                rightPage.sprite = bear;
            }
            else if (World.Enemy is EnemyDog)
            {
                rightPage.sprite = dog;
            }
        }
        else
        {
            rightPage.sprite = null;
        }
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
