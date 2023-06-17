using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform hpBar;

    private IHealth health;
    private float maxValue;
    private float fullWidth;


    private void Awake()
    {
        health = GetComponentInParent<IHealth>();
        maxValue = health.MaxHealth;
        fullWidth = hpBar.sizeDelta.x;
        health.OnTakeDamage += TakeDamage;
    }

    private void OnDestroy()
    {
        health.OnTakeDamage -= TakeDamage;
    }

    private void TakeDamage(float hp)
    {
        float width = hp * fullWidth / maxValue;
        hpBar.sizeDelta = new Vector2(width, hpBar.sizeDelta.y);
    }
}
