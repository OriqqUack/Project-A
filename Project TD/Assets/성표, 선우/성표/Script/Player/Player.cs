using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region REQUIRE COMPONENT
[RequireComponent(typeof(PlayerControl))]
[RequireComponent(typeof(AnimatePlayer))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody))]
#endregion REQUIRE COMPONENT

[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerDetailsSO playerDetails;
    [HideInInspector] public AnimatePlayer animatePlayer;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Health health;

    private void Awake()
    {
        // Load Component 
        animatePlayer = GetComponent<AnimatePlayer>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    public void Initalize(PlayerDetailsSO playerDetails)
    {
        this.playerDetails = playerDetails;

        // set player starting health
        SetPlayerHealth();
    }

    private void SetPlayerHealth()
    {
        health.SetStartingHealth(playerDetails.playerHealthAmount);
    }

}
