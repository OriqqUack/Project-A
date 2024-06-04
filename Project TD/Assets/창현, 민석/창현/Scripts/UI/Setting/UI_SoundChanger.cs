using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UI_SoundChanger : UI_Base
{
    Slider bgmSlider;
    Slider soundFxSlider;

    public Slider BGMSlider
    {
        get
        {
            return bgmSlider;
        }
        set
        {

        }
    }

    public Slider SoundFxSlider
    {
        get
        {
            return soundFxSlider;
        }
        set
        {

        }
    }

    public AudioMixer audioMixer;

    enum GameObjects
    {
        BGMSlider,
        SoundFxSlider
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        bgmSlider = GetObject((int)GameObjects.BGMSlider).GetComponent<Slider>();
        soundFxSlider = GetObject((int)GameObjects.SoundFxSlider).GetComponent<Slider>();
        bgmSlider.onValueChanged.AddListener(OnBgmVolumeChanged);
        soundFxSlider.onValueChanged.AddListener(OnSoundFxVolumeChanged);

    }

    void OnBgmVolumeChanged(float value)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
    }

    void OnSoundFxVolumeChanged(float value)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }



}
