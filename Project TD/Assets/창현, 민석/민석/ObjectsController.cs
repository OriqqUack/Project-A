using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsController : MonoBehaviour
{
    public void Awake()
    {
        
    }

    protected void FixedUpdate()
    {
        NpcScript("UI_NPC_Text");
        BoxScript("UI_Box_Text");
        TPScript("UI_TP_Text");
        ROKETScript("UI_ROKET_Text");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            if (gameObject.GetComponentInChildren<UI_NPC>() == null)
                Managers.UI.MakeWorldSpaceUI<UI_NPC>(gameObject.transform, "UI_NPC_Text");
        }

        if (other.CompareTag("BOX"))
        {
            if (gameObject.GetComponentInChildren<UI_NPC>() == null)
                Managers.UI.MakeWorldSpaceUI<UI_NPC>(gameObject.transform, "UI_Box_Text");
        }

        if (other.CompareTag("TP"))
        {
            if (gameObject.GetComponentInChildren<UI_NPC>() == null)
                Managers.UI.MakeWorldSpaceUI<UI_NPC>(gameObject.transform, "UI_TP_Text");
        }

        if (other.CompareTag("ROKET"))
        {
            if (gameObject.GetComponentInChildren<UI_NPC>() == null)
                Managers.UI.MakeWorldSpaceUI<UI_NPC>(gameObject.transform, "UI_ROKET_Text");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject go = Util.FindChild(gameObject, "UI_NPC_Text", true);
        Destroy(go);

        GameObject go1 = Util.FindChild(gameObject, "UI_Box_Text", true);
        Destroy(go1);

        GameObject go2 = Util.FindChild(gameObject, "UI_TP_Text", true);
        Destroy(go2);

        GameObject go3 = Util.FindChild(gameObject, "UI_ROKET_Text", true);
        Destroy(go3);
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

    private void ROKETScript(string prefab = null)
    {
        GameObject root = Managers.UI.Root.gameObject;

        if (Util.FindChild(gameObject, prefab, true) == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F) && Util.FindChild(root, "ROKET Text", true) == null)
        {
            Managers.UI.ShowPopupUI<UI_Popup>("Script/ROKET Text");
            GameObject go = Util.FindChild(gameObject, "UI_ROKET_Text", true);
            Destroy(go);
        }
    }
}
