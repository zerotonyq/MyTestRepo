using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DistanceJoint2D))]
public class LegFoldController : MonoBehaviour
{
    private DistanceJoint2D joint;
    private Vector3 lastPosition;
    [SerializeField] private Transform balancer;
    void Start()
    {
        lastPosition = transform.position;
        joint = GetComponent<DistanceJoint2D>();
        if(joint.enabled == false)
        {
            enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (lastPosition != transform.position)
        {
            joint.distance = (transform.position - balancer.position).magnitude;
        }
    }
}
