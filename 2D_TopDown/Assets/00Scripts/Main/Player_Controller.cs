using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed = 6.0f;
    private int currentHP;
    private int AttackPower;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().normalized;
    }

    private void Move()
    {
        rb.velocity = moveInput * moveSpeed;
        animator.SetFloat("move", rb.velocity.magnitude);
    }

    private void LateUpdate()
    {
        if (moveInput.x != 0)
        {
            spriteRenderer.flipX = moveInput.x < 0;
        }
    }

    public void ApplyItemEffect(Item_Data itemData)
    {
        if (itemData == null) return;

        currentHP += itemData.MaxHP;
        AttackPower += itemData.MaxAtk;
    }
}
