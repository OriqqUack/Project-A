using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementDetails_", menuName = "Scriptable Objects/Movement/MovementDetails")]
public class MovementDetailsSO : ScriptableObject
{

    #region Header Movement Details
    [Space(10)]
    [Header("MOVEMENT DETAILS")]
    #endregion Header Movement Details
    public float MoveSpeed = 8f;
    public float rollSpeed; // for player   
    public float rollDistance; // for player
    public float rollCooldownTime; // for player

    #region Validation
#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilitie.ValidateCheckPositiveValue(this, nameof(MoveSpeed), MoveSpeed, false);

        if (rollDistance != 0 || rollCooldownTime != 0 || rollSpeed != 0)
        {
            HelperUtilitie.ValidateCheckPositiveValue(this, nameof(rollDistance), rollDistance, false);
            HelperUtilitie.ValidateCheckPositiveValue(this, nameof(rollSpeed), rollSpeed, false);
            HelperUtilitie.ValidateCheckPositiveValue(this, nameof(rollCooldownTime), rollCooldownTime, false);
        }
    }

#endif
    #endregion Validation
}
