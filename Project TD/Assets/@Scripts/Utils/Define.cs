﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public static bool _hasEnerge = false;
    public static bool _TabletActive = false;
    public static bool _Tooltip = false;

    public enum Collection
    {
        // 도감이 채울 수 있는 갯수
        Achievement = 32,
        Monster = 32,
        Weapon = 32,
    }

    public enum WorldObject
    {
        Unknown,
        Player,
        Monster,
        Tower,
        TP,
        Box,
        Roket,
        Npc
    }
    public enum ObjectNumber
    {
        Box = 1,
        Npc = 5,
        TP = 3,
    }
    public enum Players
    {
        Unknown,
        Normal,
        Knight,
        Gunner,
        Miner,
        Engineer,
        Researcher,
        Medic
    }

    public enum Monsters
    {
        Unknown,
        Monster1,
        Monster2,
        Monster3,
        Monster4,
        Monster5,
        Monster6,
        Monster7,
        Monster8,
        Monster9,
        Monster10
    }

    public enum State
	{
		Die,
		Moving,
		Idle,
        Jump,
        FallDown,
		Attack,
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
        Loading,
        Start,
        Main,
        Login,
        City,
        Lobby,
        Game,
        Home,
        GridTestScene //삭제 해야함
    }

    public enum Sound
    {
        BGM,
        SFX,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        DoubleClick,
        Drag,
    }

    public enum UIText
    {
        Energe_Text,
        Timer_Text
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

    public enum Tile
    {
        Ground,
        HexTile
    }
}
