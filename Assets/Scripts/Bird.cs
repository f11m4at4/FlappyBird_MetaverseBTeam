using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자가 점프버튼을 누르면 점프하고 싶다.
// 필요속성 : 점프파워, Rigidbody2D
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bird : MonoBehaviour
{
    // 필요속성 : 점프파워, Rigidbody2D
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
        // 사용자가 점프버튼을 누르면 점프하고 싶다.
        // 1. 점프버튼을 눌렀으니까
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
        {
            // 2. 점프하고 싶다.
            rb.velocity = Vector2.up * jumpPower;
        }
    }
}
