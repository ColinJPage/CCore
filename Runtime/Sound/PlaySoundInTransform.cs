using UnityEngine;

public class PlaySoundInTransform : MonoBehaviour
{
    [SerializeField] SoundSO sound;
    [SerializeField] bool playOnStart = true;
    [SerializeField] bool playInTransform = false;
    private void Start()
    {
        if (playOnStart) Play();
    }
    public void Play()
    {
        if (playInTransform)
        {
            sound?.PlayInTransform(transform);
        }
        else
        {
            sound?.Play();
        }
    }
}
