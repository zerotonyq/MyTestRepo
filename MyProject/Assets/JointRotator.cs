using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointRotator : MonoBehaviour
{
    [SerializeField] private float muscle_force = 10.0f, acceptable_delta = 5.0f, angular_velocity_limit = 10.0f;
    [SerializeField, Range(0, 359)] private float target_angle = 0.0f;
    private float current_angle_difference;
    private Rigidbody2D _rigdbody;
    private void Start()
    {
        _rigdbody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    private void Update()
    {
        if(_rigdbody.angularVelocity > angular_velocity_limit)
        {
            _rigdbody.angularVelocity = angular_velocity_limit;
        }
        current_angle_difference = Mathf.Abs(transform.rotation.eulerAngles.z - target_angle);
        if (current_angle_difference > acceptable_delta)
        {
            if(transform.rotation.eulerAngles.z > target_angle)
            {
                if(current_angle_difference > 180.0f)
                {
                    _rigdbody.AddTorque(Time.deltaTime * muscle_force * 100.0f);
                }
                else
                {
                    _rigdbody.AddTorque(Time.deltaTime * muscle_force * -100.0f);
                }
            }
            else
            {
                if (current_angle_difference > 180.0f)
                {
                    _rigdbody.AddTorque(Time.deltaTime * muscle_force * -100.0f);
                }
                else
                {
                    _rigdbody.AddTorque(Time.deltaTime * muscle_force * 100.0f);
                }
            }
            
        }
    }
}
