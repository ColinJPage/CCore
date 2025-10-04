using UnityEngine;

public class RigidbodyTrajectory : Trajectory
{
    [SerializeField] private Rigidbody rb;

    private void Awake()
    {
        if (!rb) rb = GetComponentInParent<Rigidbody>();
        InitialVelocityFunc = () => rb ? rb.linearVelocity : Vector3.zero;
    }
    public override int PhysicsStepsForFirstVertex(int physicsStepsPerVertex)
    {
        //return physicsStepsPerVertex;
        //When midair, we want to keep most vertices at the same place, so we stagger the starting step
        return physicsStepsPerVertex - (int)Mathf.Floor((float)(Time.timeAsDouble / (double)Time.fixedDeltaTime)) % physicsStepsPerVertex;

    }
}
