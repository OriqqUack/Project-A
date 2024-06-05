using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[JsonObject(MemberSerialization.OptIn)]
public class RocketStat
{
    [JsonProperty]
    private Dictionary<string, float> _rocketStats;

    public RocketStat()
    {
        _rocketStats = new Dictionary<string, float>
        {
            { "RocketHealth", 0 },
            { "EnergyProductionRate", 0 },
            { "PurificationEfficiency", 0 },
            { "Level", 1},
            { "EXP" , 0},
            { "StatPoint", 0 }
        };
    }

    public void IncreaseStat(string statName, float amount)
    {
        if (_rocketStats.ContainsKey(statName))
        {
            _rocketStats[statName] += amount;
        }
        else
        {
            throw new ArgumentException("Invalid stat name");
        }
    }

    public float GetStatValue(string statName)
    {
        if (_rocketStats.ContainsKey(statName))
        {
            return _rocketStats[statName];
        }
        else
        {
            throw new ArgumentException("Invalid stat name");
        }
    }

    public void RemoveStat(string statName, float amount)
    {
        if (_rocketStats.ContainsKey(statName))
        {
            _rocketStats[statName] -= amount;
        }
        else
        {
            throw new ArgumentException("Invalid stat name");
        }
    }
}
