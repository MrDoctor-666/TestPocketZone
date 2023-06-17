using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDataSaved
{
    private IHealth health;
    private IAttackBehaviour attackBehaviour;

    public void Initialize()
    {
        health = GetComponent<IHealth>();
        health.Initialize();
        attackBehaviour = GetComponent<IAttackBehaviour>();

        health.OnDie += OnDie;
        Root.UIManager.attackButton.onClick.AddListener(delegate { attackBehaviour.Attack(); });
    }

    private void OnDestroy()
    {
        health.OnDie -= OnDie;
    }

    private void OnDie()
    {
        Root.UIManager.OpenEndGamePanel();
    }

    public void LoadData(GameData data)
    {
        health.TakeDamage(health.MaxHealth - data.currentHP);
        transform.position = data.currentPosition;
    }

    public void SaveData(GameData data)
    {
        data.currentHP = health.Health;
        data.currentPosition = transform.position;
    }
}
