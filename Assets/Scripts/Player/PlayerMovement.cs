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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Movement();
    }
    private void FixedUpdate()
    {
        if (isLocked)
        {
            rb.velocity = Vector2.zero;  // ������������� ��������, ���� �������������
            return;
        }

        rb.velocity = movement * runningSpeed;
    }
    private void Movement()
    {
        if (isLocked) return;

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        movement = new Vector2(h, v);

        if (movement.magnitude > 1) //������������, ����� ���� ��������� �� ���������
            movement = movement.normalized;
    }
    public void LockMovement()
    {
        lockCount++;
    }
    public void UnlockMovement()
    {
        lockCount = Mathf.Max(0, lockCount - 1); //���������, �� �� ������ 0
    }
    //TODO: ��������� ���������� � �������.
}
