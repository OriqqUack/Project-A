using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GraphicSetting : UI_Base
{
    enum GameObjects
    {
        Toggle1920,
        Toggle2560
    }
    public override void Init()
    {
        Bind<Toggle>(typeof(GameObjects));

        GetToggle((int)GameObjects.Toggle1920).onValueChanged.AddListener(delegate { OnToggle1920(); });
        GetToggle((int)GameObjects.Toggle2560).onValueChanged.AddListener(delegate { OnToggle2560(); });
    }

    public void OnToggle1920()
    {
        Screen.SetResolution(1080, 1920, true);
    }

    public void OnToggle2560()
    {
        Screen.SetResolution(1440, 2560, true);
    }
}
