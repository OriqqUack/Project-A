using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : IGameDataPersistence
{
    public Dictionary<string, Character> _characters;
    public RocketStat _rocketStat;
    public TraitManager _traitManager;

    public string _nowClass;

    public int[] _levelUpXpTable = new int[10]
    {
        100, 200, 300, 400, 500, 600, 700, 800, 900, 1000
    };


    public CharacterManager()
    {
        _characters = new Dictionary<string, Character>();
        _traitManager = new TraitManager();
        _rocketStat = new RocketStat();
        _nowClass = "Knight";
    }
    #region CharacterStat
    public void IncreaseCharacterStat(string characterName, string statName, float amount)
    {
        if (_characters.ContainsKey(characterName))
        {
            _characters[characterName].IncreaseStat(statName, amount);
        }
        else
        {
            throw new ArgumentException("Invalid character name");
        }
    }

    public void IncreaseCharacterExp(string characterName, float amount) // NowExp
    {
        if (_characters.ContainsKey(characterName))
        {
            _characters[characterName].IncreaseExp(amount);
            IsCharacterLevelUp(characterName);
        }
        else
        {
            throw new ArgumentException("Invalid character name");
        }
    }

    public void RemoveCharacterStat(string characterName, string statName, float amount)
    {
        if (_characters.ContainsKey(characterName))
        {
            _characters[characterName].RemoveStat(statName, amount);
        }
        else
        {
            throw new ArgumentException("Invalid character name");
        }
    }

    public float GetCharacterStatValue(string characterName, string statName)
    {
        if (_characters.ContainsKey(characterName))
        {
            return _characters[characterName].GetStatValue(statName);
        }
        else
        {
            throw new ArgumentException($"Invalid character name : {characterName}");
        }
    }

    public float GetCharacterNowExp(string characterName)
    {
        if (_characters.ContainsKey(characterName))
        {
            return _characters[characterName].GetNowExp();
        }
        else
        {
            throw new ArgumentException("Invalid character name");
        }
    }

    public Character GetNowCharacter()
    {
        return _characters[_nowClass];
    }

    public void IsCharacterLevelUp(string characterName)
    {
        int index = (int)Managers.Character.GetCharacterStatValue(characterName, "Level");
        bool isLevelUp = GetCharacterNowExp(characterName) >= _levelUpXpTable[index-1];
        if (isLevelUp)
        {
            _characters[characterName].ReemoveExp(_levelUpXpTable[index - 1]);
            _characters[characterName].IncreaseStat("Level", 1);
            _characters[characterName].IncreaseStat("StatPoint", 1);
        }
    }
    #endregion

    #region RocketStat
    public void IncreaseRocketStat(string statName, float amount)
    {
        _rocketStat.IncreaseStat(statName, amount);
        IsRocketLevelUp();
    }

    public void RemoveRocketStat(string statName, float amount)
    {
        _rocketStat.RemoveStat(statName, amount);
    }

    public float GetRocketStat(string statName)
    {
        return _rocketStat.GetStatValue(statName);
    }

    public void IsRocketLevelUp()
    {
        int index = (int)Managers.Character.GetRocketStat("Level");
        bool isLevelUp = GetRocketStat("EXP") >= _levelUpXpTable[index - 1];
        if (isLevelUp)
        {
            _rocketStat.RemoveStat("EXP",_levelUpXpTable[index - 1]);
            _rocketStat.IncreaseStat("Level", 1);
            _rocketStat.IncreaseStat("StatPoint", 1);
        }
    }
    #endregion

    public void LoadData(GameData data)
    {
        if (data == null)
        {
            Debug.Log("Data is null");
            return;
        }
        Managers.Character._rocketStat = data.RocketStat;
        Managers.Character._characters = data.Characters;
    }

    public void SaveData(ref GameData data)
    {
        data.Characters = Managers.Character._characters;
        data.RocketStat = Managers.Character._rocketStat;
    }
}
