using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    Button startbutton;
    Button exitbutton;
    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        startbutton = transform.Find("StartButton").GetComponent<Button>();
        exitbutton = transform.Find("ExitButton").GetComponent <Button>();

        startbutton.onClick.AddListener(OnClickStartButton);
        exitbutton.onClick.AddListener(OnClickExitButton);
    }

    void OnClickStartButton()
    {

        uiManager.OnClickStart();
    }

    public void OnClickExitButton()
    {
        uiManager.OnClickExit();

    }

}
