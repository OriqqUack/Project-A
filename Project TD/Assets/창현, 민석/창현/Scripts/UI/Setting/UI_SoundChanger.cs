using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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
        soundFxSlider.onValueChanged.AddListener(OnBgmVolumeChanged);

    }

    void OnBgmVolumeChanged(float value)
    {

    }

    void OnSoundFxVolumeChanged(float value)
    {

    }



}
