using UnityEngine;

public class SceneTransitionEffectBehavior : MonoBehaviour
{
    /// <summary>
    /// Transition out of the scene and cover the screen
    /// </summary>
    public virtual void Out() { }
    /// <summary>
    /// Transition into the scene and clear the screen
    /// </summary>
    public virtual void In() { }
}
