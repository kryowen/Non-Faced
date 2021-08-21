using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Missile : MonoBehaviour
{
    public float movePower = 5f; // (기본)
    public float timeCount = 0; // 투사체의 지속시간을 육안으로 확인하기위해 public 선언

    public int missileDamage = 0; // 투사체 데미지(원거리는 투사체 데미지만 적용됨)
    public int type = 0; // 0: 기본, 1: 다시 되돌아온다.
    int toggle = 1;

    Vector3 targetPos;
    Animator animator;

    void Start()
    {
        timeCount = 0f; // 초기화

        targetPos = GameObject.FindGameObjectWithTag("Player").transform.position; // 목적위치

        animator = gameObject.GetComponentInChildren<Animator>();
        animator.SetBool("isFlying", true);
    }

    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount > 4f && type.Equals(0))
        {
            Destroy(gameObject);
        } // 타입이 0인 경우, 일정시간이 지나면 사라진다.

        move();
    }

    void move()
    {
        if (type.Equals(1) && timeCount > 2f)
        {
            targetPos = GameObject.FindGameObjectWithTag("Enermy_Boss").transform.position;
            toggle = -1;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, (movePower * Time.deltaTime));
    }

    //Collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("투사체 → Player : " + missileDamage); // 플레이어 콜라이더를 통해서 데미지를 적용 시킬 것!!
            animator.SetBool("isFlying", false);
            Destroy(gameObject); // 파괴
        }

        if (other.gameObject.tag.Equals("Wall"))
        {
            Debug.Log("투사체 → 벽");
            animator.SetBool("isFlying", false);
            Destroy(gameObject); // 파괴
        }

        if (other.gameObject.tag.Equals("Enermy_Boss") && type.Equals(1) && toggle <0)
        {
            Debug.Log("투사체 도착");
            animator.SetBool("isFlying", false);
            Destroy(gameObject); // 파괴
        }

    }

}
