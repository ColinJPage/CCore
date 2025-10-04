using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CursorLock
{
    public CursorLockMode lockmode { get; private set; }
    public delegate void ReleaseAction();
    private ReleaseAction releaseAction;
    private bool active = true;
    public CursorLock(CursorLockMode lockmode)
    {
        this.lockmode = lockmode;
    }
    public void SetReleaseAction(ReleaseAction releaseAction)
    {
        this.releaseAction = releaseAction;
    }
    public void Release()
    {
        if (!active) return; //can only be released once
        releaseAction?.Invoke();
        active = false;
    }
    public static implicit operator CursorLockMode(CursorLock c) => c.lockmode;
}