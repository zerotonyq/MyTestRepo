using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointRotator : MonoBehaviour
{
    [SerializeField]
    private float muscle_force = 10.0f, acceptable_delta = 5.0f, angular_velocity_limit = 10.0f, current_muscle_force_coef = 0.0f;
    [SerializeField, Range(0, 360)] private float target_angle = 0.0f;
    private Rigidbody2D _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        target_angle = transform.localEulerAngles.z;
    }
    private void FixedUpdate()
    {
        //cashing
        float currentDeltaTime = Time.deltaTime;
        float currentEulerAnglesZ = transform.localEulerAngles.z;
        float current_angle_difference = target_angle - currentEulerAnglesZ;
        float current_angle_difference_abs = Mathf.Abs(target_angle - currentEulerAnglesZ);
        float current_angle_difference_sign = Mathf.Sign(target_angle - currentEulerAnglesZ);
        //limit the angular velocity
        if (Mathf.Abs(_rigidbody.angularVelocity) > angular_velocity_limit)
        {
            _rigidbody.angularVelocity = angular_velocity_limit * Mathf.Sign(_rigidbody.angularVelocity);
        }
        //calculating the shortest way to turn the object to target direction
        if(current_angle_difference_abs > 180)
        {
            current_angle_difference = (360 - current_angle_difference_abs) * -current_angle_difference_sign;
        }
        //calculate coef based on difference between angles: current and target. this helps to remove visual shaking and sharply moving
        current_muscle_force_coef = current_angle_difference / acceptable_delta;

        //physics rotating the object based on rb.addTorgue
        _rigidbody.AddTorque(currentDeltaTime * current_muscle_force_coef * muscle_force, ForceMode2D.Force);
    }
    public void ChangeTargetAngle(float angle = 0.0f)
    {
        angle %= 360.0f;
        target_angle = angle;
    }
}
