using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class MinimumCameraDimensions : MonoBehaviour
{
    [Tooltip("The camera must cover this many units horizontally and vertically")]
    [SerializeField] Vector2 minSize = new Vector2(8, 5);
    [SerializeField] public float fovShrinkIndex = 1;
    [SerializeField] Camera cam;
    private CinemachineCamera cinemachineCam;

    private void OnEnable()
    {
        GetComponents();
        fovShrinkIndex = 1;
    }
    private void Update()
    {
        Set();
    }
    public void SetMinimumSize(Vector2 newMinSize)
    {
        minSize = newMinSize;
    }
    public void SetMinimumSize(float squareSize)
    {
        minSize = Vector2.one * squareSize;
    }
    private void OnValidate()
    {
        GetComponents();
        Set();
    }
    void GetComponents()
    {
        //cam = GetComponent<Camera>();
        cinemachineCam = GetComponent<CinemachineCamera>();
    }

    void Set()
    {
        float camAspect;
        if (cinemachineCam)
        {
            camAspect = cinemachineCam.Lens.Aspect; //stuck at 1?
        }
        else
        {
            camAspect = cam.aspect;
        }
        var minAspect = minSize.x / minSize.y;

        float orthoSize;
        if (camAspect > minAspect) //camera is too wide
        {
            orthoSize = minSize.y / fovShrinkIndex;
        }
        else //camera is too narrow
        {
            orthoSize = minSize.x / camAspect / fovShrinkIndex;
        }

        if (cinemachineCam)
        {
            cinemachineCam.Lens.OrthographicSize = orthoSize;
            cinemachineCam.Lens.FieldOfView = orthoSize;
        }
        else
        {
            cam.orthographicSize = orthoSize;
        }

    }
}

