using UnityEngine;

public class FloatVariableDisplayUI : VariableDisplayUI<float>
{
    [SerializeField] string formatString = "";
    [SerializeField] float factor = 1f;
    protected override string GetString()
    {
        return (GetValue()*factor).ToString(formatString);
    }
}
