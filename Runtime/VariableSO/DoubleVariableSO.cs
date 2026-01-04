using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Variable/Double")]
public class DoubleVariableSO : VariableSO<double>
{
    public void PrintValue()
    {
        Debug.Log(name + " value: " + Value);
    }

}