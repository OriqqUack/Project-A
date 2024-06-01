using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int startingHealth;
    private int currentHealth;

    /// <summary>
    /// ���� ü�� ����
    /// </summary>
    /// <param name="startingHealth"></param>
    public void SetStartingHealth(int startingHealth)
    {
        this.startingHealth = startingHealth;
        currentHealth = startingHealth;
    }

    /// <summary>
    /// ���� ü�� �ҷ�����
    /// </summary>
    /// <returns></returns>
    public int GetStartingHelth()
    {
        return startingHealth;
    }
}
