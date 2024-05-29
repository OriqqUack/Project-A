using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager
{
    public Dictionary<string, Character> _characters;
    public int[] _levelUpXpTable = new int[10]
    {
        100, 200, 300, 400, 500, 600, 700, 800, 900, 1000
    };


    public CharacterManager()
    {
        _characters = new Dictionary<string, Character>
        {
            { "Knight", new Character("Knight") },
            { "Gunner", new Character("Gunner") },
            { "Engineer", new Character("Engineer") },
            { "Normal", new Character("Normal") },
            { "Miner", new Character("Miner") },
            { "Researcher", new Character("Researcher") },
        };
    }

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

    public void IncreaseCharacterExp(string characterName, float amount)
    {
        if (_characters.ContainsKey(characterName))
        {
            _characters[characterName].IncreaseExp(amount);
            IsLevelUp(characterName);
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

    public void IsLevelUp(string characterName)
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

    public void LoadData(GameData data)
    {
        if (data == null)
        {
            Debug.Log("Data is null");
            return;
        }
        Managers.Character._characters = data.Characters;
    }

    public void SaveData(ref GameData data)
    {
        data.Characters = Managers.Character._characters;
    }
}
