using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPriorityQueue<T>
{
    public void Insert(T item, int priority = 0);
    public T Peek();
    public T Pop();
    public int Count();
    public bool Empty();
    public void Remove(T itemToRemove);
}
