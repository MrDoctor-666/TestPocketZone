using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour, IAttackBehaviour
{
    [SerializeField] float damage;
    public float Damage => damage;

    IHealth currentOpponent;

    public void Attack()
    {
        if (currentOpponent == null)
        {
            currentOpponent = FindObjectOfType<PlayerController>().GetComponent<IHealth>();
            //can seach in radius -> 
            //in future can attack anyone with specific traits
        }

        if (currentOpponent != null)
        {
            currentOpponent.TakeDamage(Damage);
        }
    }
}
