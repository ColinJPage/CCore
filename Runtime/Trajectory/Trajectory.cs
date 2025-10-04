using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteAlways]
[RequireComponent(typeof(LineRenderer))]
public class Trajectory : MonoBehaviour
{
    LineRenderer lineRenderer;
    [Tooltip("How many seconds ahead to predict")]
    [SerializeField] float maxPredictionTime = 5f;

    [Tooltip("How thick the object is. It is better to overestimate here.")]
    [Min(0.01f)]
    [SerializeField] float objectRadius = 0.3f;

    [SerializeField] Transform endTarget;
    
    [SerializeField] LayerMask collisionMask; // = LayerMask.GetMask("Default", "Ground", "Clone");

    [SerializeField] float gameTimeBetweenVertices = 0.2f;
    [SerializeField] int maxBounces = 0; // 0 means it stops at the first wall

    public Event UpdateEvent { get; } = new Event();
    public List<Vector3> linePoints { get; private set; }
    private Vector3 targetNormal = Vector3.up;

    public Func<Vector3> InitialVelocityFunc { get; protected set; } = () => Vector3.forward;
    protected Vector3 InitialVelocity => InitialVelocityFunc();
    public Func<Vector3> InitialPositionFunc { get; protected set; }
    protected Vector3 InitialPosition => InitialPositionFunc != null ? InitialPositionFunc() : transform.position;
    protected virtual float Drag => 0f;
    protected virtual Vector3 SampleAcceleration(Vector3 worldPos)
    {
        return Physics.gravity;
    }
    protected virtual Vector3 SampleAcceleration(SimState simState)
    {
        return SampleAcceleration(simState.currentPos);
    }
    private void Awake()
    {
        if (InitialPositionFunc == null) InitialPositionFunc = () => transform.position;
    }
    protected virtual void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void LateUpdate()
    {
        UpdateLine();
    }

    void UpdateLine()
    {
        linePoints = CalculateLinePoints();
        lineRenderer.positionCount = linePoints.Count;
        for(int i = 0; i < linePoints.Count; ++i)
        {
            lineRenderer.SetPosition(i, linePoints[i]);
        }

        if (endTarget)
        {
            endTarget.position = linePoints[linePoints.Count - 1];

            var forward = Vector3.ProjectOnPlane(InitialVelocity, Vector3.zero);
            var right = Vector3.Cross(targetNormal, forward);
            endTarget.rotation = Quaternion.LookRotation(Quaternion.AngleAxis(90f, right) * targetNormal, targetNormal);
        }


        UpdateEvent.Trigger();
    }
    public virtual int PhysicsStepsForFirstVertex(int physicsStepsPerVertex)
    {
        return physicsStepsPerVertex;
        //When midair, we want to keep most vertices at the same place, so we stagger the starting step
        //return physicsStepsPerVertex - (int)Mathf.Floor((float)(Time.timeAsDouble / (double)Time.fixedDeltaTime)) % physicsStepsPerVertex;

    }

    protected class SimState
    {
        public Vector3 currentPos;
        public Vector3 currentVel;
        public float time;
        public int bouncesRemaining;
        public SimState(Vector3 initialPos, Vector3 initialVelocity)
        {
            currentPos = initialPos;
            currentVel = initialVelocity;
            time = 0f;
        }
    }
    List<Vector3> CalculateLinePoints()
    {
        var points = new List<Vector3>(); // line render vertices
        float time = 0f; // how far into the future we're currently looking

        var sim = new SimState(InitialPosition, InitialVelocity);
        sim.bouncesRemaining = maxBounces;

        points.Add(sim.currentPos);

        int physicsStepsPerVertex = Mathf.Max(1, Mathf.FloorToInt(gameTimeBetweenVertices / Mathf.Max(0.0001f, Time.fixedDeltaTime)));

        // How many fixed updates were skipped between this vertex and the last
        int fixedUpdateSteps = PhysicsStepsForFirstVertex(physicsStepsPerVertex);
        int currentPhysicsStepIndex = 0;

        while(time < maxPredictionTime)
        {
            var deltaTime = Time.fixedDeltaTime * fixedUpdateSteps;
            time = Mathf.Min(time + deltaTime, maxPredictionTime);

            //nextPosition += simulationVelocity * thisStep + 0.5f * acceleration * Mathf.Pow(thisStep, 2f);

            Vector3 acceleration = SampleAcceleration(sim);
            // the margin every physics frame between the physically correct future position and unity's discretely calculated position
            Vector3 errorOffsetPerPhysicsStep = Mathf.Pow(Time.fixedDeltaTime, 2f) * acceleration * 0.5f;

            currentPhysicsStepIndex += fixedUpdateSteps;
            Vector3 nextPosition = sim.currentPos + sim.currentVel * deltaTime + 0.5f * acceleration * Mathf.Pow(deltaTime, 2f); //mathematically accurate
            nextPosition += errorOffsetPerPhysicsStep * currentPhysicsStepIndex; // compensate for discrete unity physics steps
            //nextPosition += errorOffsetPerPhysicsStep;

            sim.currentVel += acceleration * deltaTime;

            fixedUpdateSteps = physicsStepsPerVertex;

            RaycastHit hit;
            var segment = nextPosition - sim.currentPos;
            var ray = new Ray(sim.currentPos, segment);

            bool didHitWall = false;
            Vector3 hitWallNormal = Vector3.zero;
            Vector3 hitWallPoint = Vector3.zero;

            if (Physics.SphereCast(ray, objectRadius, out hit, segment.magnitude, collisionMask, QueryTriggerInteraction.Ignore))
            {
                didHitWall = true;
                hitWallNormal = hit.normal; //hit.normal is direction from contact point to sphere center, NOT surface normal. So we do a regular raycast
                if(Physics.Raycast(sim.currentPos, hit.point - sim.currentPos, out var normalHit, hit.distance*2f, collisionMask))
                {
                    hitWallNormal = normalHit.normal;
                }
                hitWallPoint = hit.point;
                
            }
            // The sphere didn't hit anything, but we might have begun already in a wall. We'll check for that here
            else if(Physics.Raycast(ray, out hit, segment.magnitude+objectRadius, collisionMask, QueryTriggerInteraction.Ignore))
            {
                didHitWall = true;
                hitWallPoint = hit.point;
                hitWallNormal = hit.normal;
            }
            if (didHitWall)
            {
                points.Add(hitWallPoint);
                if (sim.bouncesRemaining > 0)
                {
                    sim.bouncesRemaining--;
                    var prevVel = sim.currentVel;
                    sim.currentVel = Vector3.Reflect(sim.currentVel, hitWallNormal);
                    //nextPosition = hitWallPoint + Vector3.Reflect(hitWallPoint - nextPosition, hitWallNormal);
                    //nextPosition = ray.GetPoint(hit.distance) + sim.currentVel.normalized * (segment.magnitude - hit.distance);
                    nextPosition = ray.GetPoint(hit.distance);
                    //nextPosition = hitWallPoint + currentVel.normalized * objectRadius;
                    //print($"predict bounce vel {prevVel} tp {sim.currentVel}, normal {hitWallNormal} at pos {ray.GetPoint(hit.distance)} or {nextPosition}");
                }
                else
                {
                    targetNormal = hitWallNormal;
                    break;
                }
            }
            else
            {
                points.Add(nextPosition);

            }

            sim.currentPos = nextPosition;
            
        }

        return points;
    }
}
