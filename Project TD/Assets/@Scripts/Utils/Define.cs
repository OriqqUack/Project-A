using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Monster,
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
		Skill,
        Stun,
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
        Login,
        Lobby,
        Game,
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
}
