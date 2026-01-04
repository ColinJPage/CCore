using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(DoubleReference))]
public class DoubleReferenceDrawer : ReferenceDrawer
{
}
#endif