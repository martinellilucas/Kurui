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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()

    {

        TryTargetNearestEnemy();
  
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
            target = collider.GetComponent<CombatTarget>(); // collider es un objeto de unity entonces usamos getcomponent
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
}
