using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Movement : MonoBehaviour
{
    public int HP;
    public int damage;

    public float movePower = 1f; // 일반 상태의 이동속도
    public float _movePower = 1.8f; // 추적 상태의 이동속도

    public Animator animator;
    Rigidbody2D rigid;
    Vector3 tracePos; // 객체와 플레이어 사이의 거리를 측정한다. (거리가 1이면 공격 가능 상태로 전환)

    public bool isTracing = false; // 추적상태
    bool isMoving = false; // 움직임
    bool isAttack = false; // 공격 모션
    public bool isStunned = false; // 패링
    // bool isRanged = false;

    int movementFlag = 0; // 0 : Idle, 1 : Left, 2 : Right, 3 : Up, 4 : Down
    public float range; // 객체와 플레이어 사이의 거리
    public float attackTimer = 1.5f;
    float attackDelay = 1.5f;
    float tmp;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator>();

        tmp = _movePower;

        StartCoroutine("ChangeMovement");
    }

    // Update is called once per frame
    void Update()
    {   

        if (HP < 1)
        {
            Debug.Log("몬스터 제거됨");
            Destroy(gameObject); // 해당 객체를 제거한다.
        }

        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            tracePos = GameObject.FindGameObjectWithTag("Player").transform.position;
            range = Vector3.Distance(tracePos, transform.position);
        }

        if(!isStunned)
        {
            attackTimer += Time.deltaTime;
        }

        move();
    }

    void move()
    {
        // Debug.Log("move 확인");
        Vector3 moveVelocity = Vector3.zero;

        // Stop & Attack
        if (range < 0.6f && GameObject.FindGameObjectWithTag("Player") != null && !isStunned)
        {
            _movePower = 0f;
            animator.SetBool("isRanged", false);
            animator.SetBool("isMoving", false);

            isAttack = true;
            MeleeAttack();
        }

        else if(range >= 0.6f && GameObject.FindGameObjectWithTag("Player") != null)
        {
            _movePower = tmp;
            animator.SetBool("isRanged", true);

            animator.SetBool("isAttack", false);
            animator.SetBool("isMoving", true);

        } 

        if (isTracing)
        {   
            Vector3 PlayerPos = tracePos;

            if(Mathf.Abs(PlayerPos.x - this.transform.position.x) > Mathf.Abs(PlayerPos.y - this.transform.position.y))
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

            transform.position = Vector3.MoveTowards(transform.position, PlayerPos, (_movePower * Time.deltaTime));
        }


        else // 플레이어가 콜라이더에 없을 때
        {
            if (movementFlag == 1)
            {
                moveVelocity = Vector3.left;
                animator.SetInteger("none_tracing", 1);
            }

            else if (movementFlag == 2)
            {
                moveVelocity = Vector3.right;
                animator.SetInteger("none_tracing", 2);
            }

            else if (movementFlag == 3)
            {
                moveVelocity = Vector3.up;
                animator.SetInteger("none_tracing", 3);
            }

            else if (movementFlag == 4)
            {
                moveVelocity = Vector3.down;
                animator.SetInteger("none_tracing", 4);
            }

            transform.position += moveVelocity * movePower * Time.deltaTime;
        }
    }
    
    void MeleeAttack()
    {
        if (isAttack)
        {
            if (attackTimer >= attackDelay)
            {
                animator.SetBool("isAttack", true);
                Debug.Log("공격함");
                attackTimer = 0f;
            }

            
            else {
                animator.SetBool("isAttack", false);
            }

        }

        isAttack = false;
    }

    //Collider // 피격 판정(만들었다메?)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Enermy_attack"))
        {
            // hp -= melee_damage;
        }

        if (other.gameObject.tag.Equals("Missile"))
        {
            // hp -= missile_damage;
        }
    }

    // Coroutine
    public IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 5);

        if (movementFlag == 0)
        {
            animator.SetBool("isMoving", false);
            isMoving = false;
        }

        else
        {
            isMoving = true;
            animator.SetBool("isMoving", true);
        }

        yield return new WaitForSeconds(3f);
        StartCoroutine("ChangeMovement");
    }
    
    IEnumerator Stunned()
    {
        Debug.Log("스턴당함");
        animator.SetBool("isAttack", false);
        animator.SetBool("isStunned", true);

        yield return new WaitForSeconds(3f);

        animator.SetBool("isStunned", false);
        isStunned = false;

        StopCoroutine("Stunned");
    }
}