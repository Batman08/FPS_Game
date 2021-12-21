using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [HideInInspector]
    public float Health = 1000f;

    public void TakeDamage(float damage)
    {
        Health -= damage;

        bool targetHasNoHealth = Health <= 0;
        if (targetHasNoHealth)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Killed Object");
        Destroy(gameObject);
    }
}
