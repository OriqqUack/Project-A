using Minseok.CollectionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipShow_Hide : MonoBehaviour
{
    [SerializeField]
    public CollectionTooltipUI _collectionTooltip;

    public void TooltipOn()
    {
        Define._Tooltip = true;
        _collectionTooltip.Show();
    }

    public void TooltipOff()
    {
        Define._Tooltip = false;
        _collectionTooltip.Hide();
    }
}
