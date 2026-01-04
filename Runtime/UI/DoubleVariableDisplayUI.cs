using UnityEngine;

public class DoubleVariableDisplayUI : VariableDisplayUI<double>
{
    [SerializeField] string formatString = "";
    [SerializeField] double factor = 1;
    protected override string GetString()
    {
        return (GetValue()*factor).ToString(formatString);
    }
}
