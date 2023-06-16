using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private IHealth health;
    private IAttackBehaviour attackBehaviour;

    private void Start()
    {
        health = GetComponent<IHealth>();
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
        //show "you died ui"
    }
}
