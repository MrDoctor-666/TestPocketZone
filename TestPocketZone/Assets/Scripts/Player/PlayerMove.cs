using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    private Rigidbody2D rb;
    private FixedJoystick fixedJoystick;
    private Vector2 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fixedJoystick = Root.UIManager.fixedJoystick;
    }

    void Update()
    {
        movementDirection = new Vector2(fixedJoystick.Horizontal, fixedJoystick.Vertical);
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection * movementSpeed * Time.deltaTime;
    }
}
