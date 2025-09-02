using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton <Player>
{
    private float h = 0f;
    private float v = 0f;

    [SerializeField] private float runningSpeed = 10f;
    private int lockCount = 0;
    public bool IsLocked => lockCount > 0;

    private Vector2 movement;

    private Rigidbody2D rb;
    private Animator anim;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!IsLocked)
        {
            v = Input.GetAxisRaw("Vertical");
            h = Input.GetAxisRaw("Horizontal");

            Movement(h, v);
        }
        else
        {
            movement = Vector2.zero;
        }
        RunAnimationCheck(h, v);
    }
    private void FixedUpdate()
    {
        if (IsLocked)
        {
            rb.velocity = Vector2.zero;  // Останавливаем движение, если заблокировано
            return;
        }
        rb.velocity = movement * runningSpeed;
    }
    private void Movement(float h, float v)
    {
        if (IsLocked) return;

        movement = new Vector2(h, v);

        if (movement.magnitude > 1) //нормализация, когда перс двигается по диагонали
            movement = movement.normalized;
    }
    void RunAnimationCheck(float h, float v)
    {
        if (IsLocked)
        {
            anim.SetBool("isMoving", false);
            return;
        }
        bool isMoving = h != 0 || v != 0;

        anim.SetFloat("Horizontal", h);
        anim.SetFloat("Vertical", v);
        anim.SetBool("isMoving", isMoving);

        //сохраняем последнее направление, если игрок двигается
        if (isMoving)
        {
            anim.SetFloat("LastHorizontal", h);
            anim.SetFloat("LastVertical", v);
        }
    }
    public void LockMovement()
    {
        lockCount++;
    }
    public void UnlockMovement()
    {
        lockCount = Mathf.Max(0, lockCount - 1); //уменьшаем, но не меньше 0
    }

}
