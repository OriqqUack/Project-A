using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

[JsonObject(MemberSerialization.OptIn)]
public class Character
{
    [JsonProperty]
    private Dictionary<string, IStat> _stats;
    [JsonProperty]
    private float _nowExp = 0;
    [JsonProperty]
    private List<ITrait> _traits;
    [JsonProperty]
    private string _className;

    public string ClassName => _className;  // Getter for _className
    public List<ITrait> Traits => _traits;  // Getter for _traits

    public Character(string className, List<ITrait> traits)
    {
        _traits = new List<ITrait>();
        if(traits != null)
            _traits = traits;

        _className = className;

        //TODO : 클래스별로 switch로 나누어야함
        _stats = new Dictionary<string, IStat>
        {
            { "Level", new Level(1)},
            { "StatPoint", new StatPoint(0) },
            { "Health", new ClassHealth(0) },
            { "Speed", new Speed(0) },
            { "Attack", new AttackStat(0) },
            { "AttackSpeed", new AttackSpeed(0) },
            { "Defense", new Defense(0) },
            { "CriticalPer", new CriticalPer(0) },
            { "CriticalAtk", new CriticalAtk(0) }
        };
        _className = className;
    }

    public void IncreaseStat(string statName, float amount)
    {
        if (_stats.ContainsKey(statName))
        {
            _stats[statName].Increase(amount);
        }
        else
        {
            throw new ArgumentException("Invalid stat name");
        }
    }

    public void IncreaseExp(float amount)
    {
        _nowExp += amount;
    }

    public void ReemoveExp(float amount)
    {
        _nowExp -= amount;
    }

    public void RemoveStat(string statName, float amount)
    {
        if (_stats.ContainsKey(statName))
        {
            _stats[statName].Remove(amount);
        }
        else
        {
            throw new ArgumentException("Invalid stat name");
        }
    }

    public float GetStatValue(string statName)
    {
        if (_stats.ContainsKey(statName))
        {
            return _stats[statName].GetValue();
        }
        else
        {
            throw new ArgumentException("Invalid stat name");
        }
    }

    public float GetNowExp()
    {
        return _nowExp;
    }
}
