using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UndoableEventListener : MonoBehaviour
{
    [SerializeField] UndoableEventSO undoableEvent;

    private void OnEnable()
    {
        undoableEvent.Subscribe(OnEvent);
    }
    private void OnDisable()
    {
        undoableEvent.Unsubscribe(OnEvent);
    }
    protected abstract void OnEvent(UndoableEventParams parameters);
}
