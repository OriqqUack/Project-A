using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerDetails_",menuName ="Scriptable Objects/Player/PlayerDetails")]
public class PlayerDetailsSO : ScriptableObject
{
    #region �÷��̾� �⺻���� ���
    [Space(10)]
    [Header("Player base Details")]
    #endregion �÷��̾� �⺻���� ���

    #region Tooltip
    [Tooltip("ĳ���� �̸�")]
    #endregion Tooltip
    public string playerCharacterName;
    #region Tooltip
    [Tooltip("ĳ���� ������")]
    #endregion Tooltip
    public GameObject playerPrefab;

    #region ü�� ���
    [Space(10)]
    [Header("HEALTH")]
    #endregion Header Health
    #region Tooltip
    [Tooltip("�÷��̾� ���� ü��")]
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
