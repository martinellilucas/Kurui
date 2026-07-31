using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float followSmoothTime=0.15f;

    [SerializeField]
    private float lookAheadSmoothTime=0.08f;

    [SerializeField]
    private float lookAheadDistance=2f;

    private PlayerMovement playerMovement;

    private Vector3 currentVelocity;
    private Vector3 lookAheadOffset;
    private Vector3 lookAheadVelocity;

    private void Start()
    {
        playerMovement = target.GetComponent<PlayerMovement>();
    }
    // LateUpdate is called once per frame after the updates()
    void LateUpdate()
    {
        LookAhead();
        FollowTarget();
    }


    private void FollowTarget()
    {
        transform.position = Vector3.SmoothDamp(transform.position,target.position + lookAheadOffset,ref currentVelocity, followSmoothTime);
    }
  
    
    private void LookAhead()
    {
        Vector3 desiredOffset = playerMovement.MoveDirection * lookAheadDistance;

        lookAheadOffset = Vector3.SmoothDamp(lookAheadOffset, desiredOffset,ref lookAheadVelocity, lookAheadSmoothTime);

        
    }
}
