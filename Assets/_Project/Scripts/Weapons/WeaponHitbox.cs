using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    private Weapon weapon;
    private Collider hitbox;
    private PlayerCombat owner;
    private HashSet<EnemyHealth> damagedEnemies = new();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        owner = GetComponentInParent<PlayerCombat>();
        weapon = GetComponentInParent<Weapon>();
        hitbox = GetComponent<Collider>();
        DisableHitbox();
    }

    public void EnableHitbox()
    {
        damagedEnemies.Clear();
        hitbox.enabled = true;
    }
    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {   
        EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();

        if(enemyHealth== null)
            return;

        if ( other.GetComponentInParent<PlayerCombat>() == owner)
            return;

        if (damagedEnemies.Contains(enemyHealth))
            return;

        damagedEnemies.Add(enemyHealth);
        enemyHealth.TakeDamage(weapon.Damage); 

    }
}
