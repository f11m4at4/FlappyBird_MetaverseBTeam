using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ڰ� ������ư�� ������ �����ϰ� �ʹ�.
// �ʿ�Ӽ� : �����Ŀ�, Rigidbody2D
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bird : MonoBehaviour
{
    // �ʿ�Ӽ� : �����Ŀ�, Rigidbody2D
    public float jumpPower = 2;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ����ڰ� ������ư�� ������ �����ϰ� �ʹ�.
        // 1. ������ư�� �������ϱ�
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
        {
            // 2. �����ϰ� �ʹ�.
            rb.velocity = Vector2.up * jumpPower;
        }
    }
}
