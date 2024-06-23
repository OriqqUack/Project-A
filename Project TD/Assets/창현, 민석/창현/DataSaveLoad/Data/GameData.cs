using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public class GameData
{
    #region 플레이어 스탯
    public string _name;
    public string _job;
    public int _level;
    public int _hp;
    public int _energe;
    public int _luck;

    public int _exp;
    public int _totalExp;
    public int _maxHp;
    public float _moveSpeed;
    public int _attack;
    public float _attackSpeed;
    public int _defense;
    public float _criticalPer;
    public float _criticalAtk;

    public float _attackDistance;
    public float _dashingPower;
    public float _dashingTime;
    public float _dashingCooldown;

    public Dictionary<string, Character> Characters { get; set; }
    public RocketStat RocketStat { get; set; }
    #endregion

    #region 도감
    public Data.CollectionEntry[] collectionEntries;
    public bool[] collectionDiscovered;
    #endregion

    public GameData()
    {
        #region 플레이어 스탯
        this._name = "";
        this._job = "Normal";
        this._level = 1;
        this._energe = 0;

        this._moveSpeed = 5.0f;
        this._dashingPower = 10.0f;
        this._dashingTime = 0.2f;
        this._dashingCooldown = 0.1f;
        this._attackDistance = 3;
        this._exp = 0;
        this._totalExp = 100;

        Characters = new Dictionary<string, Character>()
        {
            { "Knight", new Character("Knight", new List<ITrait>{ new SpeedBoostTrait(), new SpeedBoostTrait() , new SpeedBoostTrait() }) },
            { "Gunner", new Character("Gunner", new List<ITrait>{ }) },
            { "Engineer", new Character("Engineer", new List<ITrait>{ }) },
            { "Normal", new Character("Normal", new List<ITrait>{ }) },
            { "Miner", new Character("Miner", new List<ITrait>{ }) },
            { "Researcher", new Character("Researcher", new List<ITrait>{ }) }
        };

        RocketStat = new RocketStat();
        #endregion

        #region 도감
        #endregion
    }
}

