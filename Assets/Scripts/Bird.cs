using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ����ڰ� ������ư�� ������ �����ϰ� �ʹ�.
// �ʿ�Ӽ� : �����Ŀ�, Rigidbody2D
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bird : MonoBehaviour
{
    // Bird ����
    enum BirdState
    {
        Ready,
        Start,
        Playing,
        Die
    }

    BirdState state = BirdState.Ready;
    // �ʿ�Ӽ� : �����Ŀ�, Rigidbody2D
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

    // �����ð� �Ŀ� ���¸� Start �� ��ȯ
    // �ʿ�Ӽ� : ����ð�, ���ð�
    public float readyDelayTime = 2;
    float currentTime = 0;
    private void Ready()
    {
        // �����ð� �Ŀ� ���¸� Start �� ��ȯ
        // 1. �ð��� �귶���ϱ�
        currentTime += Time.deltaTime;
        // 2. �����ð� �������ϱ�
        if (currentTime > readyDelayTime)
        {
            // 3. ���¸� start �� ��ȯ
            currentTime = 0;
            state = BirdState.Start;
            anim.SetTrigger("Fly");
            readyText.SetActive(false);
        }
    }

    // ����ڰ� ����Ȥ�� �߻� ��ư ������ ���¸� Playing ���� ��ȯ
    // ������ ����
    private void GameStart()
    {
        // ����ڰ� ������ư�� ������ �����ϰ� �ʹ�.
        // 1. ������ư�� �������ϱ�
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
        {
            rb.simulated = true;
            // 2. �����ϰ� �ʹ�.
            rb.velocity = Vector2.up * jumpPower;
            state = BirdState.Playing;
        }
    }

    private void Playing()
    {
        // ����ڰ� ������ư�� ������ �����ϰ� �ʹ�.
        // 1. ������ư�� �������ϱ�
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
        {
            // 2. �����ϰ� �ʹ�.
            rb.velocity = Vector2.up * jumpPower;
        }
    }

    // ȭ���� Ŭ���ϸ� ������ �ٽ� �����ϰ� �ʹ�.
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
        // ���¸� Die �� ��ȯ
        state = BirdState.Die;
        // �ִϸ��̼ǵ� Die �� ��ȯ
        anim.SetTrigger("Die");
        rb.velocity = dieSpeed;
    }
}
