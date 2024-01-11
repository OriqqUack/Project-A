using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            zRotation = (rotationAngle + i * (Mathf.PI / 4)) * Mathf.Rad2Deg + 90; // ȸ����
            buildingsPos.Add(rotationAngle + i * (Mathf.PI / 4));
            Vector3 SinCos = new Vector3(Mathf.Cos(buildingsPos[i]), Mathf.Sin(buildingsPos[i]), 0); // Adjust the calculation for SinCos
            buildings[i].transform.position = center.position + SinCos * radius;
            buildings[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
        }
    }

    IEnumerator RotateBuildingsRight() // �ε��� ���� ��ġ�� ����
    {
        isRotating = true;

        for(float j = Mathf.PI / 2; j <= Mathf.PI / 2 + Mathf.PI / 4; j += Mathf.PI / 256) //�ʱ� ���� pi���� �� �¾� �������� ���� 0�̸� ���� ����
        {
            for (int i = 0; i < buildings.Count; i++)
            {
                buildingsPos[i] -= Mathf.PI / 256;
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

        for (float j = Mathf.PI / 2; j <= Mathf.PI / 2 + Mathf.PI / 4; j += Mathf.PI / 256)
        {
            for (int i = 0; i < buildings.Count; i++)
            {
                buildingsPos[i] += Mathf.PI / 256;
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
        //���������� �ϳ� ���� ���ʰ� �Ǹ� ù��°�� �ڷ� ����ǰ� 
        if (!isRotating)
        {
            if (buildings.Count <= clickCount+1)
                return; // Ŭ�� Ƚ���� ���̺��� ��� ���Ѿ
            clickCount++;
            Debug.Log("RightClick");
            StartCoroutine(RotateBuildingsRight());
        }
    }

    public void OnButtonClickLeft()
    {
        //���������� �ϳ� ���� ���ʰ� �Ǹ� ù��°�� �ڷ� ����ǰ� 
        if (!isRotating)
        {
            if (clickCount<=0)
                return; // Ŭ�� Ƚ���� ���̺��� ��� ���Ѿ
            clickCount--;
            Debug.Log("LeftClick");
            StartCoroutine(RotateBuildingsLeft());
        }
    }
}
