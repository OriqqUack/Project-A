using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMaterial : MonoBehaviour
{
    public Shader shader;
    private Renderer[] renderers;
    private Material[][] originalMaterials; // 각 렌더러의 원래 머티리얼을 저장할 2D 배열


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
            // 각 렌더러의 머티리얼을 복사하여 저장
            originalMaterials[i] = renderers[i].materials;
        }
    }

    public void ChangeMaterial(Shader newShader)
    {
        // 새로운 머티리얼을 생성
        Material newMaterial = new Material(newShader);

        foreach (Renderer renderer in renderers)
        {
            // 각 렌더러의 머티리얼을 변경
            Material[] materials = renderer.materials; // 현재 머티리얼 가져오기
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = newMaterial; // 새로운 머티리얼로 설정
            }
            renderer.materials = materials; // 변경된 머티리얼을 렌더러에 적용
        }
    }

    public void RestoreOriginalMaterials()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].materials = originalMaterials[i]; // 원래 머티리얼로 복원
        }
    }
}


