using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI titleTxt;
    public TextMeshProUGUI descriptionTxt;

    public void SetupTooltip(string title, string des)
    {
        titleTxt.text = title;
        descriptionTxt.text = des;
    }
}
