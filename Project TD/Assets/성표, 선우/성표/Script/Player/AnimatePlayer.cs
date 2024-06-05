using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class AnimatePlayer : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        // Load component
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }



}
