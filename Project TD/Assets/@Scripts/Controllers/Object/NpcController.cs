using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    
    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

    public void Awake()
    {
        WorldObjectType = Define.WorldObject.Npc;
    }


    private void OnTriggerEnter(Collider other)
    {
        GameObject _player = Managers.Game.GetPlayer();

        if (other.CompareTag("Player"))
        {
            if (gameObject.GetComponentInChildren<UI_NPC>() == null)
                Managers.UI.MakeWorldSpaceUI<UI_NPC>(_player.transform, "UI_NPC_Text");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject _player = Managers.Game.GetPlayer();

        GameObject go = Util.FindChild(_player, "UI_NPC_Text", true);
        Destroy(go);
    }
}
