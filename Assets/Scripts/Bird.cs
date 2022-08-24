using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 사용자가 점프버튼을 누르면 점프하고 싶다.
// 필요속성 : 점프파워, Rigidbody2D
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bird : MonoBehaviour
{
    // Bird 상태
    enum BirdState
    {
        Ready,
        Start,
        Playing,
        Die
    }

    BirdState state = BirdState.Ready;
    // 필요속성 : 점프파워, Rigidbody2D
    public float jumpPower = 2;
    Rigidbody2D rb;

    Animator anim;

    public GameObject readyText;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case BirdState.Ready:
                Ready();
                break;
            case BirdState.Start:
                GameStart();
                break;
            case BirdState.Playing:
                Playing();
                break;
            case BirdState.Die:
                Die();
                break;
        }
        
    }

    // 일정시간 후에 상태를 Start 로 전환
    // 필요속성 : 경과시간, 대기시간
    public float readyDelayTime = 2;
    float currentTime = 0;
    private void Ready()
    {
        // 일정시간 후에 상태를 Start 로 전환
        // 1. 시간이 흘렀으니까
        currentTime += Time.deltaTime;
        // 2. 일정시간 지났으니까
        if (currentTime > readyDelayTime)
        {
            // 3. 상태를 start 로 전환
            currentTime = 0;
            state = BirdState.Start;
            anim.SetTrigger("Fly");
            readyText.SetActive(false);
        }
    }

    // 사용자가 점프혹은 발사 버튼 누르면 상태를 Playing 으로 전환
    // 점프도 진행
    private void GameStart()
    {
        // 사용자가 점프버튼을 누르면 점프하고 싶다.
        // 1. 점프버튼을 눌렀으니까
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
        {
            rb.simulated = true;
            // 2. 점프하고 싶다.
            rb.velocity = Vector2.up * jumpPower;
            state = BirdState.Playing;
        }
    }

    private void Playing()
    {
        // 사용자가 점프버튼을 누르면 점프하고 싶다.
        // 1. 점프버튼을 눌렀으니까
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
        {
            // 2. 점프하고 싶다.
            rb.velocity = Vector2.up * jumpPower;
        }
    }

    // 화면을 클릭하면 게임을 다시 시작하고 싶다.
    private void Die()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }
    }

    public Vector2 dieSpeed = new Vector2(-1, 2);
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(state == BirdState.Die)
        {
            return;
        }
        // 상태를 Die 로 전환
        state = BirdState.Die;
        // 애니메이션도 Die 로 전환
        anim.SetTrigger("Die");
        rb.velocity = dieSpeed;
    }
}
