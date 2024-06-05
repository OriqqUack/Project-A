using Newtonsoft.Json;
using UnityEngine;
public interface IStat
{
    void Increase(float amount);
    void Remove(float amount);
    float GetValue();
}

[JsonObject(MemberSerialization.OptIn)]
public class Level : IStat
{
    [JsonProperty]
    public int _levelValue;

    public Level(int level)
    {
        this._levelValue = level;
    }

    public void Increase(float amount)
    {
        _levelValue++;
    }

    public void Remove(float amount)
    {
        _levelValue--;
    }

    public float GetValue()
    {
        return _levelValue;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class StatPoint : IStat
{
    [JsonProperty]
    public int _statPoints;

    public StatPoint(int statPoint) 
    {
        this._statPoints = statPoint;
    }

    public void Increase(float amount)
    {
        _statPoints++;
    }

    public void Remove(float amount)
    {
        _statPoints--;
    }

    public float GetValue()
    {
        return _statPoints;
    }
}


[JsonObject(MemberSerialization.OptIn)]
public class ClassHealth : IStat
{
    [JsonProperty]
    private float _healthValue;

    public ClassHealth(float value)
    {
        this._healthValue = value;
    }

    public void Increase(float amount)
    {
        _healthValue += amount;
    }

    public void Remove(float amount)
    {
        _healthValue -= amount;
    }

    public float GetValue()
    {
        return _healthValue;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class Speed : IStat
{
    [JsonProperty]
    private float _speedValue;

    public Speed(float value)
    {
        this._speedValue = value;
    }

    public void Increase(float amount)
    {
        _speedValue += amount;
    }

    public void Remove(float amount)
    {
        _speedValue -= amount;
    }

    public float GetValue()
    {
        return _speedValue;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class AttackStat : IStat
{
    [JsonProperty]
    private float _attackValue;

    public AttackStat(float value)
    {
        this._attackValue = value;
    }

    public void Increase(float amount)
    {
        _attackValue += amount;
    }

    public void Remove(float amount)
    {
        _attackValue -= amount;
    }

    public float GetValue()
    {
        return _attackValue;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class AttackSpeed : IStat
{
    [JsonProperty]
    private float _attackSpeedValue;

    public AttackSpeed(float value)
    {
        this._attackSpeedValue = value;
    }

    public void Increase(float amount)
    {
        _attackSpeedValue += amount;
    }

    public void Remove(float amount)
    {
        _attackSpeedValue -= amount;
    }

    public float GetValue()
    {
        return _attackSpeedValue;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class Defense : IStat
{
    [JsonProperty]
    private float _defenseValue;

    public Defense(float value)
    {
        this._defenseValue = value;
    }

    public void Increase(float amount)
    {
        _defenseValue += amount;
    }

    public void Remove(float amount)
    {
        _defenseValue -= amount;
    }

    public float GetValue()
    {
        return _defenseValue;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class CriticalPer : IStat
{
    [JsonProperty]
    private float _criticalValue;

    public CriticalPer(float value)
    {
        this._criticalValue = value;
    }

    public void Increase(float amount)
    {
        _criticalValue += amount;
    }

    public void Remove(float amount)
    {
        _criticalValue -= amount;
    }

    public float GetValue()
    {
        return _criticalValue;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class CriticalAtk : IStat
{
    [JsonProperty]
    private float _criticalAtkValue;

    public CriticalAtk(float value)
    {
        this._criticalAtkValue = value;
    }

    public void Increase(float amount)
    {
        _criticalAtkValue += amount;
    }

    public void Remove(float amount)
    {
        _criticalAtkValue -= amount;
    }

    public float GetValue()
    {
        return _criticalAtkValue;
    }
}