using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UIElements;

public class BaseBuildingController : MonoBehaviour
{
    public List<GameObject> buildings; // �ǹ� �迭
    public List<float> buildingsPos;

    public Transform center;
    public float radius = 10.0f;

    private int clickCount = 0;
    private float rotationAngle = Mathf.PI / 2;
    private float zRotation = 0;
    private bool isRotating = false;

  
    
    void Start()
    {
        // �ʱ�ȭ �� �̺�Ʈ ���
        Init();
        
    }

    private void Update()
    {
        
    }

    // �ǹ��� ������ �Լ�
    void Init()
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            zRotation = (rotationAngle + i * (2*Mathf.PI/ buildings.Count)) * Mathf.Rad2Deg + 90; // ȸ����
            buildingsPos.Add(rotationAngle + i * (2*Mathf.PI/ buildings.Count));
            Vector3 SinCos = new Vector3(Mathf.Cos(buildingsPos[i]), Mathf.Sin(buildingsPos[i]), 0); // Adjust the calculation for SinCos
            buildings[i].transform.position = center.position + SinCos * radius;
            buildings[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
        }
    }

    IEnumerator RotateBuildingsRight() // �ε��� ���� ��ġ�� ����
    {
        isRotating = true;

        for(float j = Mathf.PI / 2; j <= Mathf.PI / 2 + 2 * Mathf.PI / buildings.Count; j += Mathf.PI / (10 * buildings.Count)) //�ʱ� ���� pi���� �� �¾� �������� ���� 0�̸� ���� ����
        {
            for (int i = 0; i < buildings.Count; i++)
            {
                buildingsPos[i] -= Mathf.PI / (10 * buildings.Count);
                zRotation = buildingsPos[i] * Mathf.Rad2Deg + 90;
                Vector3 SinCos = new Vector3(Mathf.Cos(buildingsPos[i]), Mathf.Sin(buildingsPos[i]),0); // Adjust the calculation for SinCos
                buildings[i].transform.position = center.position + SinCos * radius;
                buildings[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
            }

            yield return new WaitForSeconds(0.01f); // ��Ÿ Ÿ������ ��ĥ �ʿ� ����
        }

        
        isRotating = false;
    }

    IEnumerator RotateBuildingsLeft() // �ε��� ���� ��ġ�� ����
    {
        isRotating = true;

        for (float j = Mathf.PI / 2; j <= Mathf.PI / 2 + 2 * Mathf.PI / buildings.Count; j += Mathf.PI / (10 * buildings.Count))
        {
            for (int i = 0; i < buildings.Count; i++)
            {
                buildingsPos[i] += Mathf.PI / (10 * buildings.Count);
                zRotation = buildingsPos[i] * Mathf.Rad2Deg + 90;
                Vector3 SinCos = new Vector3(Mathf.Cos(buildingsPos[i]), Mathf.Sin(buildingsPos[i]), 0); // Adjust the calculation for SinCos
                buildings[i].transform.position = center.position + SinCos * radius;
                buildings[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
            }
            yield return new WaitForSeconds(0.01f); // ��Ÿ Ÿ������ ��ĥ �ʿ� ����
        }
        isRotating = false;
    }


    // ��ư Ŭ�� �� ȣ��� �Լ�, 
    public void OnButtonClickRight()
    { 
        if(!isRotating)
           StartCoroutine(RotateBuildingsRight());
    }

    public void OnButtonClickLeft()
    {
        if(!isRotating)
           StartCoroutine(RotateBuildingsLeft());
    }
}
