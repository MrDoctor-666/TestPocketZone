using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDataSaved
{
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private float triggerRaidus = 4f;
    [SerializeField] private float attackDistance = 1f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private LayerMask playerMask;

    private int indexID;
    private IHealth health;
    private IAttackBehaviour attackBehaviour;
    private State state;

    private float currentCooldown = 0f;
    private Rigidbody2D rb;
    private Vector2 movementDirection;

    public void Initialize(int index)
    {
        indexID = index;
        health = GetComponent<IHealth>();
        health.Initialize();
        attackBehaviour = GetComponent<IAttackBehaviour>();
        state = State.Idle;

        rb = GetComponent<Rigidbody2D>();
        movementDirection = Vector2.zero;

        health.OnDie += OnDie;
    }

    private void Update()
    {
        Collider2D hitCollider = Physics2D.OverlapBox(
            new Vector2(transform.position.x, transform.position.y),
            new Vector2(triggerRaidus, triggerRaidus),
            0f,
            playerMask);

        if (hitCollider != null && state == State.Idle)
        {
            state = State.Combat;
            currentCooldown = 0;
        }
        else if (hitCollider == null && state == State.Combat) state = State.Idle;

        if (state == State.Combat)
        {
            //move to player if needed ??
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);

            if (distance > attackDistance)
                movementDirection = hitCollider.transform.position - transform.position;
            else movementDirection = Vector2.zero;

            currentCooldown = Mathf.Max(0f, currentCooldown - Time.deltaTime);

            if (currentCooldown <= 0 && distance <= attackDistance)
            {
                attackBehaviour.Attack();
                currentCooldown = attackCooldown;
            }

        }

        if (state == State.Idle)
            movementDirection = Vector2.zero;
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection * speed * Time.deltaTime;
    }

    private void OnDie()
    {
        GetComponent<DropLoot>().Drop();
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        health.OnDie -= OnDie;
    }

    public void LoadData(GameData data)
    {
        if (!data.enemyIndexToHP.ContainsKey(indexID) || data.enemyIndexToHP[indexID] <= 0)
            Destroy(gameObject);
        else
        {
            health.TakeDamage(health.MaxHealth - data.enemyIndexToHP[indexID]);
            transform.position = data.enemyIndexToPositions[indexID];
        }
    }

    public void SaveData(GameData data)
    {
        data.enemyIndexToPositions[indexID] = transform.position;
        data.enemyIndexToHP[indexID] = health.Health;
    }
}
