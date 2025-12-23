using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(CinemachineCamera))]
public class ScreenShaker : MonoBehaviour
{
    public static FloatModVariable shakeMagnitude = new FloatModVariable(0f);

    private static float currentStrength = 0f;
    private Modifier<float> shakeMod;

    [SerializeField] float shakeFadeSpeed = 2f;
    //private CinemachineVirtualCamera cam;
    private CinemachineBasicMultiChannelPerlin perlin;
    private void Awake()
    {
        //cam = GetComponent<CinemachineVirtualCamera>();
        perlin = GetComponent<CinemachineCamera>().GetCinemachineComponent(CinemachineCore.Stage.Noise) as CinemachineBasicMultiChannelPerlin;
        shakeMod = new FloatAdder(() => currentStrength);
        currentStrength = 0f;
    }
    private void OnEnable()
    {
        shakeMagnitude.AddModifier(shakeMod);
    }
    private void OnDisable()
    {
        shakeMagnitude.RemoveModifier(shakeMod);
    }

    private void Update()
    {
        currentStrength = Mathf.Max(0f, currentStrength - Time.fixedDeltaTime*shakeFadeSpeed);
        OnMagnitudeChange();
    }

    void OnMagnitudeChange()
    {
        //var perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.AmplitudeGain = shakeMagnitude.GetValue();
    }

    public static void Shake(float magnitude)
    {
        currentStrength = Mathf.Max(currentStrength, magnitude);
    }

}
