using UnityEngine;

public class CursorModVariable : ModVariable<CursorState, Modifier<CursorState>>
{
    private CursorAppearance baseAppearance = CursorAppearance.None;
    protected override CursorState BaseValue()
    {
        return new CursorState(baseAppearance);
    }
    public void SetBaseAppearance(CursorAppearance appearance)
    {
        baseAppearance = appearance;
    }
}
public abstract class CursorStateModifier : Modifier<CursorState> { }
public class CursorStateMod : CursorStateModifier
{
    public CursorState state;
    private bool overrideLock = true;
    private bool overrideVisibility = true;
    public bool overrideAppearance = false;
    public CursorStateMod(CursorState state)
    {
        this.state = state;
    }
    public override CursorState Modify(ref CursorState data, in bool mode)
    {
        if (overrideLock) data.lockMode = state.lockMode;
        if (overrideVisibility) data.visible = state.visible;
        if (overrideAppearance) data.appearance = state.appearance;
        return data;
    }
}
public class CursorAppearanceMod : CursorStateModifier
{
    private CursorAppearance appearance;
    public CursorAppearanceMod(CursorAppearance appearance)
    {
        this.appearance = appearance;
    }
    public void SetApperance(CursorAppearance newAppearance)
    {
        appearance = newAppearance;
    }
    public override CursorState Modify(ref CursorState data, in bool mode)
    {
        data.appearance = appearance;
        return data;
    }
}
public class CursorVisibilityMod : CursorStateModifier
{
    private bool visible = false;
    public CursorVisibilityMod(bool visible)
    {
        this.visible = visible;
    }
    public override CursorState Modify(ref CursorState data, in bool mode)
    {
        data.visible = visible;
        return data;
    }
}
public class CursorState
{
    public CursorLockMode lockMode;
    public bool visible = true;
    public CursorAppearance appearance = CursorAppearance.None;
    public CursorState()
    {
        lockMode = CursorLockMode.None;
        visible = true;
    }
    public CursorState(CursorLockMode lockMode) : base()
    {
        this.lockMode = lockMode;
        visible = lockMode != CursorLockMode.Locked;
    }
    public CursorState(CursorAppearance appearance) : base()
    {
        this.appearance = appearance;
    }
}