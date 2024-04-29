using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralExplosion : MonoBehaviour
{
    [SerializeField] float _explosionRange = 3f;

    MineralInteraction mineralInteraction;

    private void Awake()
    {
        mineralInteraction = GetComponent<MineralInteraction>();
        mineralInteraction.DestroyEvent += DestroyEvent;
    }

    public void DestroyEvent()
    {
        Debug.Log("Æø¹ß");
    }
}
