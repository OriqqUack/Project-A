
[System.Serializable]
public class GameData
{
    #region 플레이어 스탯
    public string _name;
    public int _level;
    public int _hp;
    public int _maxHp;
    public int _attack;
    public int _exp;
    public int _totalExp;
    public int _gold;
    public int _luck;

    public int _defense;
    public float _attackDistance;
    public float _moveSpeed;
    public float _dashingPower;
    public float _dashingTime;
    public float _dashingCooldown;
    #endregion

    #region 도감
    public Data.CollectionEntry[] collectionEntries;
    public bool[] collectionDiscovered;
    #endregion

    public GameData()
    {
        #region 플레이어 스탯
        this._name = "";
        this._level = 1;
        this._gold = 0;

        this._defense = 5;
        this._moveSpeed = 5.0f;
        this._dashingPower = 10.0f;
        this._dashingTime = 0.2f;
        this._dashingCooldown = 0.1f;
        this._attackDistance = 3;
        this._exp = 0;

        this._hp = 100;
        this._maxHp = 100;
        this._attack = 5;
        this._totalExp = 10;
        #endregion

        #region 도감
        #endregion
    }
}

