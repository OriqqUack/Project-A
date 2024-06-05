using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int startingHealth;
    private int currentHealth;

    /// <summary>
    /// 시작 체력 설정
    /// </summary>
    /// <param name="startingHealth"></param>
    public void SetStartingHealth(int startingHealth)
    {
        this.startingHealth = startingHealth;
        currentHealth = startingHealth;
    }

    /// <summary>
    /// 시작 체력 불러오기
    /// </summary>
    /// <returns></returns>
    public int GetStartingHelth()
    {
        return startingHealth;
    }
}
