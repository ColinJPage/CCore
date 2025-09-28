using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(DoubleCurveReference))]
public class DoubleCurveReferenceDrawer : ReferenceDrawer
{
}
[CustomPropertyDrawer(typeof(AnimationCurveReference))]
public class AnimationCurveReferenceDrawer : ReferenceDrawer
{
}
#endif