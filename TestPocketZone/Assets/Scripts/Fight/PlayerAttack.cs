using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttackBehaviour
{
    [SerializeField] float damage;
    [SerializeField] LayerMask m_LayerMask;
    [SerializeField] float radius;
    public float Damage => damage;

    public void Attack()
    {
        //find closest oponent in radius
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(
            new Vector2(transform.position.x, transform.position.y), 
            new Vector2(radius, radius), 
            0f,
            m_LayerMask);
        //if no one is in radius can do whatever
        if (hitColliders.Length <= 0)
        {
            Root.UIManager.OpenPanel();
            return;
        }
        Collider2D chosen = hitColliders[0];
        float bestDistance = radius * 2;
        foreach (var collider in hitColliders)
        {
            float distance = Vector3.Distance(collider.transform.position, this.transform.position);
            Debug.Log("Hit : " + collider.name + distance);

            if (distance < bestDistance)
            {
                bestDistance = distance;
                chosen = collider;
            }
        }

        chosen.GetComponent<IHealth>().TakeDamage(Damage);

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, radius* transform.localScale);
    }
}
