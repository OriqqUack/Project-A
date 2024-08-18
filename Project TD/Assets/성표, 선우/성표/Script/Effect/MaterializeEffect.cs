using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MaterializeEffect : MonoBehaviour
{
    private Material[][] originalMaterials;
    private Renderer[] renderers;

    public IEnumerator MaterializeRoutine(Shader materializeShader, Color materializeColor, float materializeTime, string dissolveName)
    {
        InitalizeMaterial();

        Material materializeMaterial = new Material(materializeShader);

        materializeMaterial.SetColor("_EmissionColor", materializeColor);

        foreach (Renderer renderer in renderers)
        {
            // 각 렌더러의 머티리얼을 변경
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = materializeMaterial; 
            }
            renderer.materials = materials;
        }

        float dissolveAmount = 0f;

        while (dissolveAmount < 1f)
        {
            dissolveAmount += Time.deltaTime / materializeTime;

            materializeMaterial.SetFloat(dissolveName, dissolveAmount);

            yield return null;

        }

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].materials = originalMaterials[i];
        }

    }

    public void InitalizeMaterial()
    {
        renderers = GetComponentsInChildren<Renderer>();

        originalMaterials = new Material[renderers.Length][];

        for (int i = 0; i < renderers.Length; i++)
        {
            originalMaterials[i] = renderers[i].materials;
        }
    }
}