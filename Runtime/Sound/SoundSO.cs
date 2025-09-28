using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SoundSO", menuName = "SO/Sound/Sound", order = 1)]
public class SoundSO : ScriptableObject
{
    [SerializeField]
    AudioClip[] clips;
    [SerializeField]
    float volume = 1f;
    [Range(0f, 1f)]
    public float spatialBlend = 1f;

    public Vector2 pitchRange = new Vector2(1f, 1f);//The range of pitches that will be randomly chosen when played

    public AudioMixerGroup group;

    private int lastClipIndex = 0;
    [SerializeField] bool dontRepeatClips = true;

    [SerializeField] CooldownScaledTime cooldown;

    public float minPlayDelay;
    [SerializeField] public bool dontDestroyOnLoad = false;

    [SerializeField] bool DEBUG = false;


    private void OnValidate()
    {
        cooldown.SetCooldownTime(minPlayDelay);
    }
    private void OnEnable()
    {
        cooldown.SetCooldownTime(minPlayDelay);
        cooldown.Reset();
        cooldown.SetCooldownTime(minPlayDelay);
    }
    /*!Returns a non consecutive AudioClip from clips[]*/
    public AudioClip Clip
    {
        get
        {
            int randomIndex = Random.Range(0, clips.Length);

            if (dontRepeatClips && randomIndex == lastClipIndex)
                randomIndex = (randomIndex + 1) % clips.Length;

            lastClipIndex = randomIndex;

            return clips[randomIndex];
        }
    }
    public bool HasAnyClips => clips.Length > 0;
    public float Volume
    {
        get
        {
            return volume;
        }
    }
    /*!Calls the SoundManager Play() method and passes itself as a param if the cooldown time has passed.*/
    public AudioSource Play()
    {
        //if (cooldown.IsBeforeTime) { return null; }
        //if (DEBUG) Debug.Log($"Playing sound {name}");
        //cooldown.Do();

        //if (!HasAnyClips) return null;
        ////SoundManager.PlayOneShot(this);
        //return SoundManager.Play(this);
        return Play(Vector3.zero);
    }
    /*!Implementation of Play() that includes a vector3 position.*/
    public AudioSource Play(Vector3 position)
    {
        if (cooldown.IsBeforeTime) { return null; }
        if (DEBUG) Debug.Log($"Playing sound {name}");
        cooldown.Do();

        if (!HasAnyClips) return null;

        return SoundManager.Play(this, position);
    }
    public AudioSource PlayInTransform(Transform parent)
    {
        var source = Play(parent.position);
        if (source)
            source.transform.SetParent(parent);

        return source;
    }
    // For inspector (button and events)
    public void PlayVoid()
    {
        Play();
    }
}
