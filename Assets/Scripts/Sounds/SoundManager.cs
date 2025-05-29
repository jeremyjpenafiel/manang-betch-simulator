using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Settings")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    [Header("Audio Clips")]
    public List<AudioClipData> audioClips;

    private Dictionary<string, AudioClipData> clipDict;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        clipDict = new Dictionary<string, AudioClipData>();
        foreach (var clipData in audioClips) {
            clipDict[clipData.name] = clipData;
        }
    }

    public void Play(string name) {
        if (!clipDict.TryGetValue(name, out var clipData)) {
            Debug.LogWarning($"Sound {name} not found!");
            return;
        }

        float volume = clipData.volume * masterVolume;

        if (clipData.type == SoundType.Music) {
            musicSource.clip = clipData.clip;
            musicSource.volume = volume * musicVolume;
            musicSource.Play();
        } else {
            sfxSource.PlayOneShot(clipData.clip, volume * sfxVolume);
        }
    }

    public void StopMusic() => musicSource.Stop();
    public void SetMasterVolume(float value) => masterVolume = Mathf.Clamp01(value);
    public void SetMusicVolume(float value) => musicVolume = Mathf.Clamp01(value);
    public void SetSFXVolume(float value) => sfxVolume = Mathf.Clamp01(value);
}
