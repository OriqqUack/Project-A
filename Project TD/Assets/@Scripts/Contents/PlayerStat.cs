using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStat : Stat
{
	[SerializeField]
	protected int _exp;
	[SerializeField]
	protected int _totalExp;
	[SerializeField]
	protected int _gold;
	[SerializeField]
	protected int _luck;


	public Action<int> OnLuckChanged;

	public int Exp
	{
		get { return _exp; }
		set
		{
			_exp = value;

			while (true)
			{
				Data.Stat stat;
				if (Managers.Data.StatDict.TryGetValue(Level, out stat) == false)
					break;
				if (_exp < stat.totalExp)
					break;
				Level++;
				Debug.Log($"Level Up!{Level}");
				SetStat(Level);
				Exp = 0;
			}
		}
	}

	public int Gold { get { return _gold; } set { _gold = value; } }

	public int TotalExp { get { return _totalExp; } set { _totalExp = value; } }

	public int Luck
    {
        get { return _luck; }
        set
        {
			_luck = value;
			OnLuckChanged?.Invoke(_luck);
        }
    }

	private void Awake()
	{
		_level = 1;
		_exp = 0;
		_defense = 5;
		_moveSpeed = 5.0f;
		_dashingPower = 10.0f;
		_dashingTime = 0.2f;
		_dashingCooldown = 0.1f;
		_gold = 0;
		_attackDistance = 3;

		SetStat(_level);
	}

	public void SetStat(int level)
	{
		Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
		Data.Stat stat = dict[level];
		Hp = stat.maxHp;
		MaxHp = stat.maxHp;
		Attack = stat.attack;
		TotalExp = stat.totalExp;
	}

	protected override void OnDead(Stat attacker)
	{
		Debug.Log("Player Dead");
	}
}
