using UnityEngine;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening
{


    public class DotweenExtensions : MonoBehaviour
    {
        public static Tween To(DOGetter<Vector3> getter, DOSetter<Vector3> setter, DOGetter<Vector3> endValue, float duration)
        {
            return DOTween.To(() => endValue() - getter(), (v) => setter(endValue() - v), Vector3.zero, duration);
        }
        public static Tween ToTransformPosition(Transform moveT, Transform targetT, float duration)
        {
            return DotweenExtensions.To(() => moveT.position, (v) => moveT.position = v, ()=>targetT.position, duration);
        }
        //public static TweenerCore<T, T, NoOptions> To<T>(DOGetter<T> getter, DOSetter<T> setter, DOGetter<T> endValue, float duration)
        //{
        //    return DOTween.To(()=>endValue()-getter(), )
        //}

    }
}