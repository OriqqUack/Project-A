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
            { "Knight", new Character("Knight") },
            { "Gunner", new Character("Gunner") },
            { "Engineer", new Character("Engineer") },
            { "Normal", new Character("Normal") },
            { "Miner", new Character("Miner") },
            { "Researcher", new Character("Researcher") },
        };

        RocketStat = new RocketStat();
        #endregion

        #region 도감
        #endregion
    }
}

