using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerDetails_",menuName ="Scriptable Objects/Player/PlayerDetails")]
public class PlayerDetailsSO : ScriptableObject
{
    #region 플레이어 기본정보 헤더
    [Space(10)]
    [Header("Player base Details")]
    #endregion 플레이어 기본정보 헤더

    #region Tooltip
    [Tooltip("캐릭터 이름")]
    #endregion Tooltip
    public string playerCharacterName;
    #region Tooltip
    [Tooltip("캐릭터 프리팹")]
    #endregion Tooltip
    public GameObject playerPrefab;

    #region 체력 헤더
    [Space(10)]
    [Header("HEALTH")]
    #endregion Header Health
    #region Tooltip
    [Tooltip("플레이어 시작 체력")]
    #endregion Tooltip
    public int playerHealthAmount;


    #region Validattion
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilitie.ValidateCheckEmptyString(this, nameof(playerCharacterName), playerCharacterName);
        HelperUtilitie.ValidateCheckNullValue(this, nameof(playerPrefab), playerPrefab);
        HelperUtilitie.ValidateCheckPositiveValue(this, nameof(playerHealthAmount), playerHealthAmount, false);
    }
#endif
    #endregion Validattion
}
