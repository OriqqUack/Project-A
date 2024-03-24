﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Monster,
        Tower,
        Box,
        Npc,
        TP
    }

    public enum ObjectNumber
    {
        Box = 1,
        Npc = 3,
        TP = 3,
    }

    public enum State
	{
		Die,
		Moving,
		Idle,
		Skill,
	}

    public enum Layer
    {
        Monster = 8,
        Ground = 9,
        Block = 10,
    }

    public enum Scene
    {
        Unknown,
        Start,
        Main,
        Login,
        Lobby,
        Game,
        Home
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        DoubleClick,
        Drag,
    }

    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }

    public enum CameraMode
    {
        QuarterView,
    }

    public enum LevelState
    {
        Intro,
        Building,
        SpawningEnemies,
        AllEnemiesSpawned,
        Lose,
        Win
    }

    public enum KeyEvent
    {
        MovePress,
        MoveDown,
        MoveUp,
    }
}
