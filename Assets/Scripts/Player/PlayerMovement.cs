using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float runningSpeed = 20f;
    private int lockCount = 0;
    public bool isLocked => lockCount > 0;

    private Vector2 movement;

    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Movement(h,v);
        RunAnimationCheck(h, v);
    }
    private void FixedUpdate()
    {
        if (isLocked)
        {
            rb.velocity = Vector2.zero;  // Останавливаем движение, если заблокировано
            return;
        }

        rb.velocity = movement * runningSpeed;
    }
    private void Movement(float h, float v)
    {
        if (isLocked) return;


        movement = new Vector2(h, v);

        if (movement.magnitude > 1) //нормализация, когда перс двигается по диагонали
            movement = movement.normalized;
    }
    void RunAnimationCheck(float h, float v) 
    {

        bool isMoving = h != 0 || v != 0;

        anim.SetFloat("Horizontal", h);
        anim.SetFloat("Vertical", v);
        anim.SetBool("isMoving", isMoving);
    }
    public void LockMovement()
    {
        lockCount++;
    }
    public void UnlockMovement()
    {
        lockCount = Mathf.Max(0, lockCount - 1); //уменьшаем, но не меньше 0
    }
    //TODO: привязать блокировку к событию.
}
