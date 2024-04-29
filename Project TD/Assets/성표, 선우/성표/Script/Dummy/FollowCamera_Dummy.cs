using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

enum offsetmode
{
    None = 0 , X =1, Y = 2, Z = 3
}

public class FollowCamera_Dummy : MonoBehaviour
{
    [SerializeField] Vector3 offset = new Vector3(0,0,0);
    [SerializeField] float offsetSettingSpeed = 0.3f;
    [SerializeField] Transform target;

    Vector3 zoomVector = new Vector3();
    offsetmode stat = offsetmode.None;

    float _inputTimer = Mathf.Infinity;
    float _inputInterval = 0.1f;


    private void LateUpdate()
    {
        FollowCamera();
        CameraSequnce();
    }

    void CameraSequnce()
    {
        _inputTimer += Time.deltaTime;

        if (_inputInterval <= _inputTimer)
        {
            OffsetControll();
            ZoomController();
            _inputTimer = 0f;
        }
    }

    void FollowCamera()
    {
        transform.position =target.position + offset;
    }

    void ZoomController()
    {
        float scrollInput = Input.mouseScrollDelta.y;
        if (scrollInput > 0f)
        {
            CameraZoom(1f);
        }
        if (scrollInput < 0f)
        {
            CameraZoom(-1f);
        }
    }

    void CameraZoom(float point)
    {
        zoomVector = target.position - transform.position;
        offset += zoomVector.normalized * point;
    }

    void OffsetControll()
    {
        ChageOffsetMode();
        if(stat == offsetmode.X)
        {
            CoorinateSeting(Vector3.right);
        }
        if(stat == offsetmode.Y)
        {
            CoorinateSeting(Vector3.up );
        }
        if(stat == offsetmode.Z)
        {
            CoorinateSeting(Vector3.forward);
        }
    }

    void CoorinateSeting(Vector3 vector)
    {
        if (Input.GetKey(KeyCode.LeftBracket))
        {
            offset -= vector * offsetSettingSpeed;
        }
        if (Input.GetKey(KeyCode.RightBracket))
        {
            offset += vector * offsetSettingSpeed; 
        }
    }

    void ChageOffsetMode()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            stat = offsetmode.X;
            Debug.Log("OFFSET MODE :" + stat);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            stat = offsetmode.Y;
            Debug.Log("OFFSET MODE :" + stat);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            stat = offsetmode.Z;
            Debug.Log("OFFSET MODE :" + stat);
        }
       
    }
}
