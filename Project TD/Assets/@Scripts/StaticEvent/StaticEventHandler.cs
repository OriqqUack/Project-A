using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class StaticEventHandler
{
    public static event Action<AppearTileEventArgs> OnAppearTileEvent;

    public static void CallShowTileEvent(GameObject tile)
    {
        OnAppearTileEvent?.Invoke(new AppearTileEventArgs()
        {
            tile = tile
        });
    }
}

public class AppearTileEventArgs : EventArgs
{
    public GameObject tile;
}

