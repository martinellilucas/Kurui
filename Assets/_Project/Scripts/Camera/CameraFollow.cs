using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float followSmoothTime = 0.15f;

    [SerializeField]
    private float lookAheadSmoothTime = 0.08f;

    [SerializeField]
    private float lookAheadDistance = 2f;

    [SerializeField]
    private float combatMinCameraDistance = 4f;

    [SerializeField]
    private Transform cameraTransform;

    private float explorationCameraDistance;
    private PlayerMovement playerMovement;
    private CombatTargeting combatTargeting;
    private Vector3 currentVelocity;
    private Vector3 lookAheadOffset;
    private Vector3 lookAheadVelocity;


    private void Start()
    {
        combatTargeting = target.GetComponent<CombatTargeting>();
        playerMovement = target.GetComponent<PlayerMovement>();
        explorationCameraDistance = cameraTransform.localPosition.magnitude;
        Vector3 cameraOffset = cameraTransform.localPosition;
    }
    // LateUpdate is called once per frame after the updates()
    void LateUpdate()
    {
        LookAhead();
        FollowTarget();
    }


    private void FollowTarget()
    {
      
        if (combatTargeting.CurrentTarget != null)
        {
            Vector3 midPoint =
                (target.position +
                combatTargeting.CurrentTarget.transform.position) / 2f;

            float targetDistance = Vector3.Distance(
                target.position,
                combatTargeting.CurrentTarget.transform.position
            );

            float normalizedDistance = Mathf.InverseLerp(1f, 
                20f, 
                targetDistance);

            float combatDistance = Mathf.Lerp(
                combatMinCameraDistance,
                explorationCameraDistance,
                normalizedDistance
            );

            Vector3 cameraDirection = cameraTransform.localPosition.normalized;

            Vector3 desiredCameraPosition =
                midPoint + cameraDirection * combatDistance;

            Vector3 desiredRigPosition = desiredCameraPosition - cameraTransform.localPosition;
            transform.position = Vector3.SmoothDamp(
                transform.position,
                desiredRigPosition,
                ref currentVelocity,
                followSmoothTime
            );

            return;
        }

        transform.position = Vector3.SmoothDamp(transform.position,target.position + lookAheadOffset,ref currentVelocity, followSmoothTime);
    }
  
    
    private void LookAhead()
    {
        Vector3 desiredOffset;
        if (combatTargeting.CurrentTarget != null)
        {
            
        lookAheadOffset = Vector3.SmoothDamp(
            lookAheadOffset, 
            Vector3.zero,
            ref lookAheadVelocity, 
            lookAheadSmoothTime);
        return;
        }

        desiredOffset = playerMovement.MoveDirection * lookAheadDistance;
        
        lookAheadOffset = Vector3.SmoothDamp(
            lookAheadOffset, 
            desiredOffset,
            ref lookAheadVelocity, 
            lookAheadSmoothTime);
    }
}
