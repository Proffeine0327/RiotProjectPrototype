using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour, ISerializationCallbackReceiver
{
    public static SoundManager manager { get; private set; }

    [SerializeField] private List<string> keys = new List<string>();
    [SerializeField] private List<AudioClip> values = new List<AudioClip>();
    public Dictionary<string, AudioClip> audioclips = new Dictionary<string, AudioClip>();

    public void Play(string key, float volume)
    {
        var obj = new GameObject(audioclips[key].name);
        var comp = obj.AddComponent<AudioSource>();

        obj.transform.SetParent(transform);
        obj.transform.position = Vector3.zero;
        comp.clip = audioclips[key];
        comp.volume = volume;
        comp.Play();

        Destroy(obj, audioclips[key].length);
    }

    public void OnAfterDeserialize()
    {
        audioclips = new Dictionary<string, AudioClip>();

        for(int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
            audioclips.Add(keys[i], values[i]);
    }

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach(var kvp in audioclips)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }

    private void Awake() 
    {
        manager = this;
    }
}
