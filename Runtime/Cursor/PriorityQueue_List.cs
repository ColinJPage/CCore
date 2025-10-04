using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// THIS CLASS IS UNFINISHED AND DOES NOT YET USE PRIORITY.
/// It currently works like a stack
/// </summary>
/// <typeparam name="T"></typeparam>
public class PriorityQueue_List<T> : IPriorityQueue<T>
{
    //0 is the front, list.Count-1 is the back
    private List<T> list = new List<T>();
    //private bool latestHasPriority = true; //whether new elements are placed in front of existing elements of equal priority

    public void Insert(T item, int priority = 0)
    {
        list.Insert(0, item);
    }

    public T Peek()
    {
        if(Empty())
        {
            return default(T);
        }
        return list[0];
    }

    public T Pop()
    {
        var i = Peek();
        list.RemoveAt(0);
        return i;
    }
    public int Count()
    {
        return list.Count;
    }
    public bool Empty()
    {
        return list.Count == 0;
    }

    public void Remove(T itemToRemove)
    {
        for(int i = list.Count-1; i >= 0; --i)
        {
            if(list[i].Equals(itemToRemove))
            {
                list.RemoveAt(i);
                return;
            }
        }
    }
}
