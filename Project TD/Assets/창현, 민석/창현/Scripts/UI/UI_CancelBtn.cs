using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using URPGlitch.Runtime.AnalogGlitch;
using UnityEngine.Rendering;


public class UI_CancelBtn : MonoBehaviour
{
    public CinemachineVirtualCamera menuCam;

    [SerializeField]
    private Volume volume;
    private AnalogGlitchVolume analog;

    public void OnClickedCancelBtn()
    {
        menuCam.MoveToTopOfPrioritySubqueue();
        StartCoroutine(TriggerGlitch(true));
        Debug.Log("ClickedCancel");
    }

    IEnumerator TriggerGlitch(bool isCancelBtn)
    {
        if (volume.profile.TryGet(out analog))
        {
            analog.active = true;
        }
        yield return new WaitForSeconds(1.3f);

        if (isCancelBtn)
            gameObject.GetComponent<Canvas>().enabled = true;
        analog.active = false;
    }
}
