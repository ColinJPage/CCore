using UnityEngine;

public class RotateAroundAxis : MonoBehaviour
{
    [SerializeField] Vector3 axis = Vector3.forward;
    [SerializeField] float degreeSpeed = 50f;

    private void Update()
    {
        transform.RotateAround(transform.position, axis, degreeSpeed * Time.deltaTime);
    }
}
