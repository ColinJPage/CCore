using UnityEngine;
using System;

// Has a value that be gotten or subscribed/unsubscribed
// but does NOT allow setting or other modification
public interface IVariableAccess<T> : ISubscribable<Action<T>>, ISubscribable<Action>
{
    public T GetValue();
}
