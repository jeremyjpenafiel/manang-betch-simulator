using UnityEngine;

[System.Serializable]
public class AudioClipData {
    public string name;
    public AudioClip clip;
    public SoundType type;
    [Range(0f, 1f)] public float volume = 1f;
}
