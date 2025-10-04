using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This class is responsible for setting the cursor's lockstate.
/// The cursor should always be either (locked and invisible) or (unlocked and visible)
/// Setting Cursor.lockState to CursorLockMode.Locked also makes the cursor invisible automatically
/// </summary>
public static class CursorManager
{
    private const CursorLockMode defaultLockMode = CursorLockMode.None;
    private static IPriorityQueue<CursorLock> lockModeQueue = new PriorityQueue_List<CursorLock>();

    private static void SetCursorLockMode(CursorLockMode lockmode)
    {
        //Debug.Log($"Set lock state to {lockmode}. There are {lockModeQueue.Count()} locks active now.");
        Cursor.lockState = lockmode;
    }
    private static void PeekAndSetLockMode()
    {
        if (lockModeQueue.Empty())
        {
            SetCursorLockMode(defaultLockMode);
        }
        else
        {
            SetCursorLockMode(lockModeQueue.Peek());
        }
    }

    public static CursorLock AddLock(CursorLockMode lockmode, int priority = 0)
    {
        var newLock = new CursorLock(lockmode);
        lockModeQueue.Insert(newLock, priority);
        newLock.SetReleaseAction(() => {
            lockModeQueue.Remove(newLock);
            PeekAndSetLockMode();
        });
        PeekAndSetLockMode();
        return newLock;
    }
}
