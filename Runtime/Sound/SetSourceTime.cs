using UnityEngine;

public class SetSourceTime : MonoBehaviour
{
    [SerializeField] Vector2 secondsRange;
    private void Start()
    {
        GetComponent<AudioSource>().time = secondsRange.RandomBetween();
    }
}
