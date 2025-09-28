using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.Netcode;

public class Singleton<T> : MonoBehaviour where T:Component
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this as T;
        OnSetInstance();
    }
    protected virtual void OnSetInstance()
    {

    }
    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }
}
public class SingletonPersistent<T> : Singleton<T> where T : Component
{
    protected override void OnSetInstance()
    {
        DontDestroyOnLoad(Instance);
    }
}
//public class SingletonNetwork<T> : NetworkBehaviour where T : Component
//{
//    public static T Instance { get; private set; }

//    protected virtual void Awake()
//    {
//        if (Instance != null)
//        {
//            Destroy(gameObject);
//            return;
//        }
//        Instance = this as T;
//        OnSetInstance();
//    }
//    protected virtual void OnSetInstance()
//    {

//    }
//    public override void OnDestroy()
//    {
//        if (Instance == this)
//        {
//            Instance = null;
//        }
//        base.OnDestroy();
//    }
//}
//public class SingletonNetworkPersistent<T> : SingletonNetwork<T> where T : Component
//{
//    protected override void OnSetInstance()
//    {
//        DontDestroyOnLoad(Instance);
//    }
//}