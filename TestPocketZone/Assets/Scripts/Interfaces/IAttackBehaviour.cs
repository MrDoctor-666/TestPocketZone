using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackBehaviour
{
    float Damage { get; }
    public void Attack();

}


public enum State
{
    Idle,
    Combat
}