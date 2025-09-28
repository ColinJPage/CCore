using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct SceneTransition
{
    public SceneTransitionStyleSO style;
    public SceneField sceneField;
    public float minLoadTime;

    public SceneTransition(SceneField _sceneField, SceneTransitionStyleSO _style)
    {
        style = _style;

        sceneField = _sceneField;
        minLoadTime = 0f;
    }
}
