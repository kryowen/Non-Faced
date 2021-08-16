using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public int HP = 100; // 몬스터 체력
    public int damage = 0; // 몬스터의 데미지

    public float movePower = 1.5f;
    public float rushSpeed = 7.5f;
    public float attackDelay = 3f;

    float attackTimer = 0f;
    float range; // 플레이어와 객체 사이의 거리

    bool isAttack = false;
    bool isMoving = false;
    bool isRush = false;
    public bool isStunned = false;
    // 공격 애니메이션, 정수로 판단하게끔 한다.

    Animator animator;
    Vector3 targetPos; // 플레이어의 위치를 받을 변수
    public GameObject prefab; // 투사체

    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            targetPos = GameObject.FindGameObjectWithTag("Player").transform.position; // 초기화
        }

        StartCoroutine("PatternManager");
    }

    void Update()
    {
        if (HP < 1)
        {
            Debug.Log("보스몬스터 제거됨");
            Destroy(gameObject);
        }

        if(GameObject.FindGameObjectWithTag("Player") != null) // 플레이어가 존재할 경우
        {
            targetPos = GameObject.FindGameObjectWithTag("Player").transform.position; // 플레이어의 위치를 받아옴. (지속)
            range = Vector3.Distance(transform.position, targetPos); // (float) 객체와 플레이어 사이의 거리
            // 콜라이더 + bool 변수?
        }

    }

    void move()
    {
        Vector3 PlayerPos = targetPos;

        if(GameObject.FindGameObjectWithTag("Player") != null && range < 0.6f)
        {
            StartCoroutine("AttackMelee");
        }


        if (Mathf.Abs(PlayerPos.x - this.transform.position.x) > Mathf.Abs(PlayerPos.y - this.transform.position.y))
        {
            if (PlayerPos.x > transform.position.x && PlayerPos.x != transform.position.x)
            {
                //Debug.Log("우측");
                animator.SetInteger("none_tracing", 2);
            }

            else if (PlayerPos.x < transform.position.x && PlayerPos.x != transform.position.x)
            {
                //Debug.Log("좌측");
                animator.SetInteger("none_tracing", 1);
            }
        }

        else
        {
            if (PlayerPos.y > transform.position.y && PlayerPos.y != transform.position.y)
            {
                //Debug.Log("위");
                animator.SetInteger("none_tracing", 3);
            }

            else if (PlayerPos.y < transform.position.y && PlayerPos.x != transform.position.y)
            {
                //Debug.Log("아래");
                animator.SetInteger("none_tracing", 4);
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, PlayerPos, (movePower * Time.deltaTime));
    }

    // Collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Wall") && isRush)
        {
            StartCoroutine("Stunned");
        } // 돌진 후에 벽에 부딫힘.

        if(other.gameObject.tag.Equals("Player_attack"))
        {
            // 피격당함.
        }
    }

    // Coroutine
    IEnumerator PatternManager()
    {
        if (range > 10) // 원거리
        {
            Debug.Log("원거리");
            int r = Random.Range(0, 2); // 0 → 직선형 충격파, 1 → 돌진

            if (r.Equals(0)) // 직선형 충격파
            {
                Debug.Log("직선형 충격파 : " + r);
                StartCoroutine("LinearImpulse");
            }

            else if(r.Equals(1)) // 돌진
            {
               Debug.Log("돌진 : " + r);
               isRush = true;
               StartCoroutine("Rush");
            }
        }

        else if (range > 5 && range < 10) // 중거리
        {
            Debug.Log("중거리");
            // StartCoroutine("CircularImpulse");        
        }

        else // 근거리
        {
            Debug.Log("근거리");
            isAttack = true;
            float cnt = 0;

            while (cnt < 5f)
            {
                yield return new WaitForFixedUpdate();
                cnt += Time.deltaTime;

                move();
            }

            isAttack = false;
        }

        yield return new WaitForSeconds(3f);

        StartCoroutine("PatternManager");
    }

    IEnumerator AttackMelee()
    {
        while (attackDelay > attackTimer)
        {
            yield return new WaitForFixedUpdate();
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackDelay)
            {
                Debug.Log("공격 → Player :  " + damage);
                // animator.SetBool("isAttack", true);
                attackTimer = 0;
            }

            // animator.SetBool("isAttack", false);
            StopCoroutine("AttackMelee");
        }
    }

    IEnumerator Rush()
    {
        float cnt = 0;
        Vector3 PlayerPos = new Vector3(targetPos.x, targetPos.y, targetPos.z);

        while (cnt < 3f)
        {
            yield return new WaitForFixedUpdate();
            cnt += Time.deltaTime;

            if (Mathf.Abs(PlayerPos.x - this.transform.position.x) > Mathf.Abs(PlayerPos.y - this.transform.position.y))
            {
                if (PlayerPos.x > transform.position.x && PlayerPos.x != transform.position.x)
                {
                    //Debug.Log("우측");
                    // animator.SetInteger("Direction", 2); // 이팩트 찾기
                }

                else if (PlayerPos.x < transform.position.x && PlayerPos.x != transform.position.x)
                {
                    //Debug.Log("좌측");
                    // 이팩트 찾기
                }
            }

            else
            {
                if (PlayerPos.y > transform.position.y && PlayerPos.y != transform.position.y)
                {
                    //Debug.Log("위");
                    // 이팩트 찾기
                }

                else if (PlayerPos.y < transform.position.y && PlayerPos.x != transform.position.y)
                {
                    //Debug.Log("아래");
                    // 이팩트 찾기
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, PlayerPos, (rushSpeed * Time.deltaTime));
        }
    }

    IEnumerator CircularImpulse()
    {
        // animator false
        yield return new WaitForSeconds(1.5f);
        //animator.SetBool("??", true);
        yield return new WaitForSeconds(0.5f);
        //animator.SetBool("??", false);

        StopCoroutine("CircularImpulse");
    }

    IEnumerator LinearImpulse()
    {
        float dx = ((transform.position.x) - (targetPos.x));
        float dy = ((transform.position.y) - (targetPos.y));
        float rotateDg = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg + 180;

        // animator 적용
        Instantiate(prefab, transform.position, Quaternion.Euler(0, 0, rotateDg));
        
        yield return new WaitForSeconds(1f);
        StopCoroutine("LinearImpulse");
    }

    IEnumerator Stun()
    {
        // animator. // 애니메이션 활성화
        yield return new WaitForSeconds(3f);
        // animator. // 애니메이션 비활성화
    }
}
