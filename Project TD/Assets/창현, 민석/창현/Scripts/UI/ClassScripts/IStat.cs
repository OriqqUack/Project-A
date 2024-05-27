using Newtonsoft.Json;
using UnityEngine;
public interface IStat
{
    void Increase(float amount);
    void Remove(float amount);

    float GetValue();
}

[JsonObject(MemberSerialization.OptIn)]
public class EXP : IStat
{
    [JsonProperty]
    private int _level;

    [JsonProperty]
    private int _nowExp;

    public int NowExp { get { return _nowExp; } private set { } }

    private int[] _xpTable = new int[10]
    {
        100, 200, 300, 400, 500, 600, 700, 800 ,900, 1000
    };

    public void AddExp(int amount)
    {
        _nowExp += amount;
    }

    public int GetRequiredExp()
    {
        return _xpTable[_level];
    }

    public float GetValue()
    {
        return _level;
    }

    public void Increase(float amount)
    {
        _level++;
    }

    public void Remove(float amount)
    {
        _level--;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class Health : IStat
{
    [JsonProperty]
    private float _value;

    public Health(float value)
    {
        this._value = value;
    }

    public void Increase(float amount)
    {
        _value += amount;
    }

    public void Remove(float amount)
    {
        _value -= amount;
    }

    public float GetValue()
    {
        return _value;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class Speed : IStat
{
    [JsonProperty]
    private float _value;

    public Speed(float value)
    {
        this._value = value;
    }

    public void Increase(float amount)
    {
        _value += amount;
    }

    public void Remove(float amount)
    {
        _value -= amount;
    }

    public float GetValue()
    {
        return _value;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class AttackStat : IStat
{
    [JsonProperty]
    private float _value;

    public AttackStat(float value)
    {
        this._value = value;
    }

    public void Increase(float amount)
    {
        _value += amount;
    }

    public void Remove(float amount)
    {
        _value -= amount;
    }

    public float GetValue()
    {
        return _value;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class AttackSpeed : IStat
{
    [JsonProperty]
    private float _value;

    public AttackSpeed(float value)
    {
        this._value = value;
    }

    public void Increase(float amount)
    {
        _value += amount;
    }

    public void Remove(float amount)
    {
        _value -= amount;
    }

    public float GetValue()
    {
        return _value;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class Defense : IStat
{
    [JsonProperty]
    private float _value;

    public Defense(float value)
    {
        this._value = value;
    }

    public void Increase(float amount)
    {
        _value += amount;
    }

    public void Remove(float amount)
    {
        _value -= amount;
    }

    public float GetValue()
    {
        return _value;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class CriticalPer : IStat
{
    [JsonProperty]
    private float _value;

    public CriticalPer(float value)
    {
        this._value = value;
    }

    public void Increase(float amount)
    {
        _value += amount;
    }

    public void Remove(float amount)
    {
        _value -= amount;
    }

    public float GetValue()
    {
        return _value;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class CriticalAtk : IStat
{
    [JsonProperty]
    private float _value;

    public CriticalAtk(float value)
    {
        this._value = value;
    }

    public void Increase(float amount)
    {
        _value += amount;
    }

    public void Remove(float amount)
    {
        _value -= amount;
    }

    public float GetValue()
    {
        return _value;
    }
}