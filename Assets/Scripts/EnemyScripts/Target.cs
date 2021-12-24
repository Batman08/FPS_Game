using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Target : MonoBehaviour
{
    public NavMeshAgent Enemy;
    public Transform Player;

    public LayerMask IsPlayer;
    public LayerMask IsGround;
    public float TargetHealth = 1000f;
    public float AttackRange;
    public float TimeBetweenAttacks;
    public float TargetDamage;
    public bool PlayerInAttackRange;
    public bool AlreadyAttacked;


    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        TargetHealth = 1000f;
        TargetDamage = 100f;
    }

    public void TargetTakeDamage(float damage)
    {
        TargetHealth -= damage;

        bool targetHasNoHealth = TargetHealth <= 0;
        if (targetHasNoHealth)
        {
            TargetDie();
        }
    }

    private void TargetDie()
    {
        Debug.Log("Killed Object");
        Destroy(gameObject);
    }

    private void Movement()
    {
        Enemy.SetDestination(Player.position);
    }

    private void AttackPlayer()
    {
        //Stop moving
        Enemy.SetDestination(transform.position);

        transform.LookAt(Player);

        //if hasn't attacked then attack
        if (!AlreadyAttacked)
        {
            AlreadyAttacked = true;
            _playerController.PlayerTakeDamage(damage: TargetDamage);
            Invoke(nameof(ResetAttack), TimeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        AlreadyAttacked = false;
    }

    private void FixedUpdate()
    {
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, IsPlayer);

        if (PlayerInAttackRange)
        {
            AttackPlayer();
        }
        Movement();
    }
}




/*
- start of with 100 health (base health)
- increase by 100 every round up till round 10
- once round 10 starts to increase by 10 percent
 */
