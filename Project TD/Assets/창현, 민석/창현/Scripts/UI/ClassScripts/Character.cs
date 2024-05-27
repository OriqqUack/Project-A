using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[JsonObject(MemberSerialization.OptIn)]
public class Character
{
    [JsonProperty]
    private Dictionary<string, IStat> _stats;
    [JsonProperty]
    public string ClassName { get; private set; }

    public Character(string className)
    {
        ClassName = className;
        //TODO : 클래스별로 switch로 나누어야함
        _stats = new Dictionary<string, IStat>
        {
            { "EXP", new EXP() },
            { "Health", new Health(10) },
            { "Speed", new Speed(10) },
            { "Attack", new AttackStat(10) },
            { "AttackSpeed", new AttackSpeed(10) },
            { "Defense", new Defense(10) },
            { "CriticalPer", new CriticalPer(10) },
            { "CriticalAtk", new CriticalAtk(10) }
        };
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
}
