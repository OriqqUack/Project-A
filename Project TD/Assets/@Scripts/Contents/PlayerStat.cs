using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStat : Stat, IGameDataPersistence
{
	[SerializeField]
	protected string _name;
	[SerializeField]
	protected int _exp;
	[SerializeField]
	protected int _totalExp;
	[SerializeField]
	protected int _energe;
	[SerializeField]
	protected int _luck;


	public Action<int> OnLuckChanged;

	public string Name { get { return _name; } set { _name = value; } }

	public int Exp
	{
		get { return _exp; }
		set
		{
			_exp = value;

			while (true)
			{
				if (_exp < _totalExp)
					break;
				Level++;
				Debug.Log($"Level Up!{_level}");
				SetStat(_level);
				Exp = 0;
			}
		}
	}

	public int Energe { get { return _energe; } set { _energe = value; } }

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
	public Define.Players PlayerType { get; protected set; } = Define.Players.Unknown; // Despawn 하기위해


	private void Start()
    {
    }

    public void SetStat(int level)
	{
		/*Hp = stat.maxHp;
		MaxHp = stat.maxHp;
		Attack = stat.attack;
		TotalExp = stat.totalExp;*/
	}

	protected override void OnDead(Stat attacker)
	{
		Debug.Log("Player Dead");
	}

    public void LoadData(GameData data)
    {
		this.Level = data._level;
		Debug.Log($"Level : { this._level}");

		this._name = data._name;
		this.Hp = data._hp;
		this.MaxHp = data._maxHp;
		this.Attack = data._attack;
		this.TotalExp = data._totalExp;
		this.Exp = data._exp;
		Debug.Log($"EXP : { data._exp}");
		Debug.Log($"TotalEXP : { data._totalExp}");

		this._energe = data._energe;
		this.Luck = data._luck;

		this.Defense = data._defense;
		this.AttackDistance = data._attackDistance;
		//this.MoveSpeed = data._moveSpeed;
		this._dashingPower = data._dashingPower;
		this._dashingTime = data._dashingTime;
		this._dashingCooldown = data._dashingCooldown;
    }

    public void SaveData(ref GameData data)
    {
		data._name = this._name;
		data._level = this._level;
		data._hp = this._hp;
		data._maxHp = this._maxHp;
		data._attack = this._attack;
		data._exp = this._exp;
		data._totalExp = this._totalExp;
		data._energe = this._energe;
		data._luck = this._luck;

		data._defense = this._defense;
		data._attackDistance = this._attackDistance;
		data._moveSpeed = this._moveSpeed;
		data._dashingPower = this._dashingPower;
		data._dashingTime = this._dashingTime;
		data._dashingCooldown = this._dashingCooldown;
	}
}
