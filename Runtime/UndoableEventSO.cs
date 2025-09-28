using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventSO", menuName = "EventsSO/UndoableEventParams")]
public class UndoableEventSO : EventSO<UndoableEventParams>
{
    [SerializeField] public string eventName;
}

public class UndoableEventParams
{
    public string[] args;
    public CommandStack commandStack = new CommandStack();

    public bool DidAnything => !commandStack.IsEmpty;
}
