﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Station
{

  public class UiCharacterPortraitWidget : MonoBehaviour
  {
    #region FIELDS

    [SerializeField] private TextMeshProUGUI characterName = null;
    [SerializeField] private TextMeshProUGUI characterClass = null;
    [SerializeField] private UiVitalBarWidget[] _vitals = null;
    [SerializeField] private UiCharacterStatusWidget _statusWidget = null;
    [SerializeField] private BaseAnimation _animation = null;
    [SerializeField] private UnityEngine.UI.Button _button = null;
    [SerializeField] private Image _icon = null;

    private Dictionary<string, UiVitalBarWidget> _vitalSliders = new Dictionary<string, UiVitalBarWidget>();
    private BaseCharacter _character;
    private VitalsDb _vitalsDb;
    private StationAction<BaseCharacter> _buttonCallback;
    private const string STATE_ALIVE = "alive";
    private const string STATE_DEAD = "dead";

    #endregion



    #region subscription

    public void Setup(BaseCharacter character, StationAction<BaseCharacter> buttonCallback)
    {
      _vitalSliders.Clear();
      Unsubscribe();

      _buttonCallback = buttonCallback;
      if (_vitals?.Length != 0)
      {
        foreach (var slider in _vitals)
        {
          slider.gameObject.SetActive(false);
        }
      }



      if (character == null)
      {
        if (_statusWidget != null)
        {
          _statusWidget.Setup(null);
        }

        characterName.text = string.Empty;
        characterClass.text = string.Empty;


        _character = null;
      }
      else
      {
        var dbSystem = RpgStation.GetSystemStatic<DbSystem>();
        _vitalsDb = dbSystem.GetDb<VitalsDb>();
        _character = character;
        if (_statusWidget != null)
        {
          _statusWidget.Setup(_character);
        }

        characterName.text = (string) _character.GetMeta("name");
        characterClass.text = (string) _character.GetMeta("class");
        if (_icon)
        {
          _icon.sprite = (Sprite) _character.GetMeta("icon");
        }


        Subscribe();


        int sliderIndex = 0;
        if (_vitals.Length != 0)
        {
          if (character.Stats != null && character.Stats.Vitals != null)
          {
            foreach (var vital in character.Stats.Vitals)
            {
              var instance = _vitals[sliderIndex];
              _vitalSliders.Add(vital.Key, instance);
              var dataEnergy = _vitalsDb.GetEntry(vital.Key);
              instance.Setup(dataEnergy);
              instance.gameObject.SetActive(true);
              sliderIndex++;
            }
          }
        }

     

        if (_icon != null)
        {
          _icon.sprite = (Sprite)character.GetMeta(StationConst.ICON_ID);
        }

      
        OnVitalsUpdated(character);
        SetStates();
      }


    }

    private void OnDestroy()
    {
      Unsubscribe();
    }

    private void Subscribe()
    {
      _character.OnCharacterInitialized += OnCharacterInitialized;
      _character.OnVitalsUpdated += OnVitalsUpdated;
      _character.OnDamaged += OnReceiveDamage;
      _character.OnHealed += OnHealed;
      _character.OnDie += OnDie;
      _character.OnRevived += OnRespawn;

      GameGlobalEvents.OnLeaderChanged.AddListener(OnLeaderChanged);

    }

    private void Unsubscribe()
    {
      if (_character == null) return;

      _buttonCallback = null;
      _character.OnCharacterInitialized -= OnCharacterInitialized;
      _character.OnVitalsUpdated -= OnVitalsUpdated;
      _character.OnDamaged -= OnReceiveDamage;
      _character.OnHealed -= OnHealed;
      _character.OnDie -= OnDie;
      _character.OnRevived -= OnRespawn;
      GameGlobalEvents.OnLeaderChanged.RemoveListener(OnLeaderChanged);
    }


    private void OnLeaderChanged(BaseCharacter character)
    {
      if (_button == null) return;

      if (_character == character)
      {
        SetSelected();
      }
      else
      {
        SetNotSelected();
      }
    }

    #endregion

    private void OnVitalsUpdated(BaseCharacter character)
    {
      var playerStats = character.Stats;
      if (playerStats == null) return;
      foreach (var vital in playerStats.Vitals)
      {
        if (_vitalSliders.ContainsKey(vital.Key))
        {
          _vitalSliders[vital.Key].SetVitalValue(vital.Value.Current, vital.Value.MaximumValue);
        }
      }
    }

    private void OnRespawn(BaseCharacter character)
    {
      SetStates();
    }

    private void OnDie(BaseCharacter character)
    {
      if (_animation && _character)
      {
        _animation.PlayState(STATE_DEAD);
      }
    }

    private void OnHealed(BaseCharacter character, VitalChangeData data)
    {
    }

    private void OnReceiveDamage(BaseCharacter character, VitalChangeData data)
    {
    }

    private void OnCharacterInitialized(BaseCharacter character)
    {
      //  var playerClass = Resource.PlayerClassesDatabase.GetEntry(character.CharacterData.GetClass());
      //  _icon.sprite = playerClass.Icon;
    }

    public void OnClick()
    {
      if (_character)
      {
        _buttonCallback?.Invoke(_character);
      }
    }

    public void SetSelected()
    {
      _button.interactable = false;
    }

    public void SetNotSelected()
    {
      _button.interactable = true;
    }


    private void SetStates()
    {
      if (_animation && _character)
      {
        _animation.PlayState(_character.IsDead? STATE_DEAD : STATE_ALIVE);
      }
    }
  }
}

