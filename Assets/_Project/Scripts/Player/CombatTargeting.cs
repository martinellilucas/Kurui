using System;
using UnityEngine;

public class CombatTargeting : MonoBehaviour
{
    private CombatTarget currentTarget;
    public CombatTarget CurrentTarget => currentTarget;
  
    [SerializeField]
    private float targetRadius = 20f;


    [SerializeField]
    private LayerMask targetLayer;
  
    private void Update()
    {
        ValidateTarget();
    }
    public bool TryTargetNearestEnemy()
    {
        CombatTarget target;
        float closestDistance = targetRadius;
        Vector3 playerPosition = transform.position;
        CombatTarget bestTarget = null ;
      
        Collider[] colliders = Physics.OverlapSphere(
            playerPosition, 
            targetRadius, 
            targetLayer
            );
        if (colliders.Length == 0)
        {
            currentTarget = null;
            return false;
        }
     

        foreach(Collider collider in colliders)
        {
            target = collider.GetComponentInParent<CombatTarget>(); // collider es un objeto de unity entonces usamos getcomponent
            if (target == null) {
                continue;
            }
         
            float distance = Vector3.Distance(playerPosition, target.transform.position);
            if (distance< closestDistance)
            {
                 closestDistance = distance;
                 bestTarget = target;
            }

        }
        if (bestTarget == null)
        {
            currentTarget = null;
            return false;
        }

        currentTarget = bestTarget;
        return true;

    }

    public void ValidateTarget()
    {
        if (currentTarget == null)
        {
            return;
        }
        float distance = Vector3.Distance(
            transform.position,
            currentTarget.transform.position
            );
        if (distance > targetRadius)
        {
            ClearTarget();
        }
    }
    public void ClearTarget()
    {
        currentTarget = null;
    }


}
