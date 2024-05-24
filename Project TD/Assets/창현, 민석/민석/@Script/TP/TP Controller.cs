using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TPController : MonoBehaviour
{
    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

    public void Awake()
    {
        WorldObjectType = Define.WorldObject.TP;
    }

    public void FixedUpdate()
    {
        TPScript("UI_TP_Text");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TP"))
        {
            if (gameObject.GetComponentInChildren<UI_NPC>() == null)
                Managers.UI.MakeWorldSpaceUI<UI_NPC>(gameObject.transform, "UI_TP_Text");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject go = Util.FindChild(gameObject, "UI_TP_Text", true);
        Destroy(go);
    }

    private void TPScript(string prefab = null)
    {
        GameObject root = Managers.UI.Root.gameObject;

        if (Util.FindChild(gameObject, prefab, true) == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F) && Util.FindChild(root, "TP Text", true) == null)
        {
            Managers.UI.ShowPopupUI<UI_Popup>("Script/TP Text");
            GameObject go = Util.FindChild(gameObject, "UI_TP_Text", true);
            Destroy(go);
        }
    }
}
