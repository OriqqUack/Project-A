using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NpcController : MonoBehaviour
{
    
    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

    public void Awake()
    {
        WorldObjectType = Define.WorldObject.Npc;
    }

    protected void FixedUpdate()
    {
        NpcScript("UI_NPC_Text");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            if (gameObject.GetComponentInChildren<UI_NPC>() == null)
                Managers.UI.MakeWorldSpaceUI<UI_NPC>(gameObject.transform, "UI_NPC_Text");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject go = Util.FindChild(gameObject, "UI_NPC_Text", true);
        Destroy(go);
    }

    private void NpcScript(string prefab = null)
    {
        GameObject root = Managers.UI.Root.gameObject;

        if (Util.FindChild(gameObject, prefab, true) == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F) && Util.FindChild(root, "Dialogue", true) == null)
        {
            Managers.UI.ShowPopupUI<UI_Popup>("Script/Dialogue");
            GameObject go = Util.FindChild(gameObject, "UI_NPC_Text", true);
            Destroy(go);
        }
    }
}
