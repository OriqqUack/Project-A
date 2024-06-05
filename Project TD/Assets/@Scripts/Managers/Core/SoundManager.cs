using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    AudioMixer audioMixer;

    // MP3 Player   -> AudioSource
    // MP3 음원     -> AudioClip
    // 관객(귀)     -> AudioListener

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            audioMixer = GetOrAddAudioMixer("MasterAudioMixer");
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                switch (soundNames[i])
                {
                    case "BGM":
                        _audioSources[i].outputAudioMixerGroup = GetAudioMixerGroup("BGM");
                        break;
                    case "SFX":
                        _audioSources[i].outputAudioMixerGroup = GetAudioMixerGroup("SFX");
                        break;
                }
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.BGM].loop = true;
        }
    }

    private AudioMixerGroup GetAudioMixerGroup(string groupName)
    {
        AudioMixerGroup[] groups = audioMixer.FindMatchingGroups(groupName);
        if (groups.Length > 0)
        {
            Debug.Log(groups.Length);
            return groups[0];
        }
        else
        {
            Debug.LogWarning($"AudioMixerGroup '{groupName}' not found.");
            return audioMixer.FindMatchingGroups("Master")[0];
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }

    public void Play(string path, Define.Sound type = Define.Sound.SFX, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

	public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.SFX, float pitch = 1.0f)
	{
        if (audioClip == null)
            return;

		if (type == Define.Sound.BGM)
		{
			AudioSource audioSource = _audioSources[(int)Define.Sound.BGM];
			if (audioSource.isPlaying)
				audioSource.Stop();

			audioSource.pitch = pitch;
			audioSource.clip = audioClip;
			audioSource.Play();
		}
		else
		{
			AudioSource audioSource = _audioSources[(int)Define.Sound.SFX];
			audioSource.pitch = pitch;
			audioSource.PlayOneShot(audioClip);
		}
	}

	AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.SFX)
    {
		if (path.Contains("Sounds/") == false)
			path = $"Sounds/{path}";

		AudioClip audioClip = null;

		if (type == Define.Sound.BGM)
		{
			audioClip = Managers.Resource.Load<AudioClip>(path);
		}
		else
		{
			if (_audioClips.TryGetValue(path, out audioClip) == false)
			{
				audioClip = Managers.Resource.Load<AudioClip>(path);
				_audioClips.Add(path, audioClip);
			}
		}

		if (audioClip == null)
			Debug.Log($"AudioClip Missing ! {path}");

		return audioClip;
    }

    AudioMixer GetOrAddAudioMixer(string path)
    {
        if (path.Contains("Sounds/AudioMixer/") == false)
            path = $"Sounds/AudioMixer/{path}";

        if (audioMixer == null)
        {
            audioMixer = Managers.Resource.Load<AudioMixer>(path);
        }
        else
        {
            Debug.Log("AudioMixer already exist");
        }

        return audioMixer;
    }
}
