using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMaterial : MonoBehaviour
{
    public Shader shader;
    private Renderer[] renderers;
    private Material[][] originalMaterials; // �� �������� ���� ��Ƽ������ ������ 2D �迭


    private void OnEnable()
    {
        ChangeMaterial(shader);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangeMaterial(shader);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RestoreOriginalMaterials();
        }
    }

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        originalMaterials = new Material[renderers.Length][];

        for (int i = 0; i < renderers.Length; i++)
        {
            // �� �������� ��Ƽ������ �����Ͽ� ����
            originalMaterials[i] = renderers[i].materials;
        }
    }

    public void ChangeMaterial(Shader newShader)
    {
        // ���ο� ��Ƽ������ ����
        Material newMaterial = new Material(newShader);

        foreach (Renderer renderer in renderers)
        {
            // �� �������� ��Ƽ������ ����
            Material[] materials = renderer.materials; // ���� ��Ƽ���� ��������
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = newMaterial; // ���ο� ��Ƽ����� ����
            }
            renderer.materials = materials; // ����� ��Ƽ������ �������� ����
        }
    }

    public void RestoreOriginalMaterials()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].materials = originalMaterials[i]; // ���� ��Ƽ����� ����
        }
    }
}


