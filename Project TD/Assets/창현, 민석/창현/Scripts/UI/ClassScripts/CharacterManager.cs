using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager
{
    public Dictionary<string, Character> _characters;

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
            throw new ArgumentException("Invalid character name");
        }
    }

    public void LoadData(GameData data)
    {
        Managers.Game.characterManager = new CharacterManager();
        if (data == null)
        {
            Debug.Log("Data is null");
            return;

        }
        Managers.Game.characterManager._characters = data.Characters;
    }

    public void SaveData(ref GameData data)
    {
        data.Characters = Managers.Game.characterManager._characters;
    }
}
