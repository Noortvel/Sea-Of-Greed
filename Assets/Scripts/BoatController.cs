using UnityEditor;
using UnityEngine;

public class BoatController : MonoBehaviour {

    [SerializeField, Min(1)]
    private float maxForwardVelocity = 1;
    [SerializeField]
    private AnimationCurve speedIncreace = null;
    [SerializeField]
    private float power = 1;
    [SerializeField, Min(1)]
    private float maxReverseVelocity = 1;
    [SerializeField]
    private float reversePower = 1;

    [SerializeField]
    private AnimationCurve rotationSpeedIncreace = null;
    [SerializeField]
    private float rotationSpeed = 1;

    [SerializeField]
    private float brakingSpeed = 1;
    [SerializeField]
    private float sideBrakingScale = 1;
    [SerializeField]
    private float forwardThrustScale = 10;

    private Vector3 input;

    private Rigidbody rb;
    private BoatWaterPhysic waterPhysic;

    public Vector3 ForceInputs
    {
        get => input;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        waterPhysic = GetComponent<BoatWaterPhysic>();
    }
    void Update()
    {
        InputUpdate();
    }
    void FixedUpdate()
    {
        MotionFixedUpdate();
    }
    void InputUpdate()
    {
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");
    }
    public float ForwardSpeed
    {
        get => currentSpeed;
    }
    public float MaxForwardSpeed
    {
        get => maxForwardVelocity;
    }

    private Vector3 currentVelocity;
    private float currentSpeed;
    private Vector3 mFwdVelocityNormalized;
    private Vector3 forwardNormalized;

    private void MotionFixedUpdate()
    {
        if (waterPhysic.isSignificantForcePointsIsUnderwater())
        {
            currentVelocity = GetForwardVelocity();
            currentSpeed = currentVelocity.magnitude;
            mFwdVelocityNormalized = currentVelocity.normalized;
            forwardNormalized = SubstractYAndNorm(transform.forward);

            MoveAccelerationTick();
            MoveBrakingTick();
            ForwardThrustTick();
            MoveSideResistForceApplyerTick();
            RotateTick();
        }
    }
    private void MoveAccelerationTick()
    {
        var currentPower = input.z > 0 ? power : reversePower;
        var currentMaxVelocity = input.z > 0 ? maxForwardVelocity : maxReverseVelocity;
        var invRatio = 
            speedIncreace.Evaluate(Inverce01To10(currentSpeed / currentMaxVelocity));
        var force = forwardNormalized * invRatio * input.z * currentPower * Time.deltaTime;
        rb.AddForce(force);
    }
    
    private void ForwardThrustTick()
    {
        var speedRatio = currentSpeed / maxForwardVelocity;
        var scaledForward = forwardNormalized * currentSpeed;
        var meanTowardsVelocity = Vector3.MoveTowards(currentVelocity, scaledForward,
            speedRatio * forwardThrustScale * Time.deltaTime);
        meanTowardsVelocity.y = rb.velocity.y;
        rb.velocity = meanTowardsVelocity;
    }
    private void MoveBrakingTick()
    {
        var brakingForceScale = brakingSpeed * currentSpeed * Time.deltaTime;
        rb.AddForce(-mFwdVelocityNormalized * brakingForceScale);
    }
    private void MoveSideResistForceApplyerTick()
    {
        if (currentSpeed > 0.01f)
        {
            var stableFactor = DotFwdVel() * sideBrakingScale * Time.deltaTime;
            rb.AddForce(stableFactor * -mFwdVelocityNormalized);
        }
    }
    void RotateTick()
    {
        if (Mathf.Abs(input.x) > 0)
        {
            var speedRatio = currentSpeed / maxForwardVelocity;
            Vector3 rotation = rb.rotation.eulerAngles;
            rotation.y += input.x * rotationSpeed *
                rotationSpeedIncreace.Evaluate(speedRatio) * Time.deltaTime;
            rb.MoveRotation(Quaternion.Euler(rotation));
            //rb.AddTorque(rotation);
        }
    }
    private Vector3 GetForwardVelocity()
    {
        var vel = rb.velocity;
        vel.y = 0;
        return vel;
    }
    private float DotFwdVel()
    {
        var vel = SubstractYAndNorm(rb.velocity);
        var fwd = SubstractYAndNorm(transform.forward);
        var val = Inverce01To10((Vector3.Dot(vel, fwd) + 1) / 2);
        return val;
    }
    private Vector3 SubstractYAndNorm(Vector3 vec)
    {
        vec.y = 0;
        vec.Normalize();
        return vec;
    }
    private float Inverce01To10(float val) {
        return (val - 1) * (-1);
    }
}