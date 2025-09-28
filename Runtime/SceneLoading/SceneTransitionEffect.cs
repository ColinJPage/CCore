using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneTransitionEffect : MonoBehaviour
{
    [SerializeField] private float outDuration = 0f; // How long after begining the Out animation that the scene may start loading
    [SerializeField] private float inDuration = 5f;
    public float OutDuration => outDuration;
    private SceneTransitionEffectBehavior behavior;
    [SerializeField] UnityEvent OutEvent;
    [SerializeField] UnityEvent InEvent;

    [SerializeField] bool DEBUG = false;
    
    //[SerializeField] private bool startLoadingNextSceneImmediately = false;

    //public Event OutFinishedEvent = new Event();

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        behavior = GetComponentInChildren<SceneTransitionEffectBehavior>();
        DontDestroyOnLoad(gameObject);
    }
#if UNITY_EDITOR
    private void Start()
    {
        if (DEBUG)
        {
            SamplePlay();
        }
    }
#endif
    [ContextMenu("Sample Play")]
    void SamplePlay()
    {
        this.Invoke(Out, 1f);
        this.Invoke(In, 6f);
    }
    public void Out()
    {
        behavior?.Out();
    }

    public void In()
    {
        behavior?.In();
        animator?.SetTrigger("inReady");
        Destroy(gameObject, inDuration);
    }

    //public void OnOutFinished()
    //{
    //    OutFinishedEvent.Trigger();
    //}
}
