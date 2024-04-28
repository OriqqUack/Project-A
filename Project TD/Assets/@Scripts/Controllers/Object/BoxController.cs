using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class BoxController : MonoBehaviour
{
    
    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

    public void Awake()
    {
        WorldObjectType = Define.WorldObject.Box;
    }

    protected void FixedUpdate()
    {
        BoxScript("UI_Box_Text");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BOX"))
        {
            if (gameObject.GetComponentInChildren<UI_NPC>() == null)
                Managers.UI.MakeWorldSpaceUI<UI_NPC>(gameObject.transform, "UI_Box_Text");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject go = Util.FindChild(gameObject, "UI_Box_Text", true);
        Destroy(go);
    }

    private void BoxScript(string prefab = null)
    {
        GameObject root = Managers.UI.Root.gameObject;

        if (Util.FindChild(gameObject, prefab, true) == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F) && Util.FindChild(root, "CoBox", true) == null)
        {
            Managers.UI.ShowPopupUI<UI_Popup>("Box/CoBox");
            GameObject go = Util.FindChild(gameObject, "UI_Box_Text", true);
            Destroy(go);
        }
    }
}
