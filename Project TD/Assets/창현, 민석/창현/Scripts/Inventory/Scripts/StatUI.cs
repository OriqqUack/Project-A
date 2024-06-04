using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private TextMeshProUGUI maxHpText;
    [SerializeField]
    private TextMeshProUGUI speedText;
    [SerializeField]
    private TextMeshProUGUI attackText;
    [SerializeField]
    private TextMeshProUGUI luckText;

    private PlayerStat _playerStat;

    private void Start()
    {
        _playerStat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
        _playerStat.OnLevelChanged += UpdateLevelText;
        _playerStat.OnMaxHpChanged += UpdateMaxHpText;
        _playerStat.OnMoveSpeedChanged += UpdateSpeedText;
        _playerStat.OnAttackChanged += UpdateAttackText;
        _playerStat.OnLuckChanged += UpdateLuckText;

        UpdateUI();
    }

    void UpdateUI()
    {
        levelText.text = _playerStat.Level.ToString();
        maxHpText.text = _playerStat.MaxHp.ToString();
        speedText.text = _playerStat.MoveSpeed.ToString();
        attackText.text = _playerStat.Attack.ToString();
        luckText.text = _playerStat.Luck.ToString();
    }
    void UpdateLevelText(int newLevel) => levelText.text = newLevel.ToString();

    void UpdateMaxHpText(int newHealth) => maxHpText.text = newHealth.ToString();

    void UpdateSpeedText(float newSpeed) => speedText.text = newSpeed.ToString();

    void UpdateAttackText(int newAttack) => attackText.text = newAttack.ToString();

    void UpdateLuckText(int newLuck) => luckText.text = newLuck.ToString();

}
