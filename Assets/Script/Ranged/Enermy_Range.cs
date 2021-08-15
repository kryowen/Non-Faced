using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy_Range : MonoBehaviour
{
    public int HP;
    public int damage;

    public float movePower = 1f;
    public float _movePower = 1.8f;
    public float shootDelay = 3f;

    public Missile preFab; // 투사체 preFab 변수

    public Animator animator; // 원거리 애니메이터 넣는자리
    Vector3 tracePos; // 객체와 플레이어 사이의 거리를 측정한다.

    bool isMoving = false; // 움직임
    public bool runAway = false; // 켜지면 빤스런
    public bool canShoot = false; // 발사가능

    int movementFlag = 0; // 0 : Idle, 1 : Left, 2 : Right, 3 : Up, 4 : Down
    float shootTimer;
    float rotateDg;

    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();

        StartCoroutine("ChangeMovement");
    }

    void Update()
    { 
        if(HP < 1)
        {
            Debug.Log("몬스터 제거됨");
            Destroy(gameObject); // 해당 객체를 제거한다.
        }

        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            tracePos = GameObject.FindGameObjectWithTag("Player").transform.position;
        }

        shootTimer += Time.deltaTime;
        move();
    }

    void move()
    {
        // Debug.Log("move 확인");
        Vector3 moveVelocity = Vector3.zero;

        if (runAway) // 이동 수정
        {
            Vector3 pos = tracePos;

            if (Mathf.Abs(pos.x - this.transform.position.x) > Mathf.Abs(pos.y - this.transform.position.y))
            {
                if (pos.x > transform.position.x && pos.x != transform.position.x)
                {
                    //Debug.Log("좌측");
                    //animator.SetInteger("none_tracing", 1);
                    moveVelocity = Vector3.left;
                }

                else if (pos.x < transform.position.x && pos.x != transform.position.x)
                {
                    //Debug.Log("우측");
                    //animator.SetInteger("none_tracing", 2);
                    moveVelocity = Vector3.right;
                }
            }

            else
            {
                if (pos.y > transform.position.y && pos.y != transform.position.y)
                {
                    //Debug.Log("위");
                    //animator.SetInteger("none_tracing", 4);
                    moveVelocity = Vector3.down;
                }

                else if (pos.y < transform.position.y && pos.x != transform.position.y)
                {
                    //Debug.Log("아래");
                    //animator.SetInteger("none_tracing", 3);
                    moveVelocity = Vector3.up;
                }
            }

            transform.position += moveVelocity * _movePower * Time.deltaTime;
            // animator.SetBool("isMoving", true);
        }

        else
        {
            if (movementFlag == 1)
            {
                moveVelocity = Vector3.left;
                //animator.SetInteger("none_tracing", 1);
            }

            else if (movementFlag == 2)
            {
                moveVelocity = Vector3.right;
                //animator.SetInteger("none_tracing", 2);
            }

            else if (movementFlag == 3)
            {
                moveVelocity = Vector3.up;
                //animator.SetInteger("none_tracing", 3);
            }

            else if (movementFlag == 4)
            {
                moveVelocity = Vector3.down;
                //animator.SetInteger("none_tracing", 4);
            }

            transform.position += moveVelocity * movePower * Time.deltaTime;
            // animator.SetBool("isMoving", true);
        }
    }

    public void shootControl()
    {
        Vector3 pos = tracePos;

        float dx = (Mathf.Abs(pos.x) - Mathf.Abs(transform.position.x));
        float dy = (Mathf.Abs(pos.y) - Mathf.Abs(transform.position.y));

        rotateDg = Mathf.Atan2(dx, dy) * Mathf.Rad2Deg + 90;

        if (canShoot)
        {
            Debug.Log("Player가 사정권에 들어옴");

            if (shootTimer >= shootDelay)
            {
                Instantiate(preFab, transform.position, Quaternion.Euler(0,0,rotateDg)); // 이거 맞나 확인 좀
                shootTimer = 0f;
            }

        }

        // canShoot = false;
    } // 애니메이션 추가

    // Collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player_attack")
        {
            Debug.Log("공격 당함");
        }
    }

    // Coroutine
    IEnumerator ChangeMovement()
    {
        // Debug.Log("Front Logic : " + Time.time);

        movementFlag = Random.Range(0, 5);

        if (movementFlag == 0)
        {
           // animator.SetBool("isMoving", false);
        }

        else
        {
            //animator.SetBool("isMoving", true);
        }

        yield return new WaitForSeconds(3f);

        // Debug.Log("Behind Logic : " + Time.time);

        StartCoroutine("ChangeMovement");
    }
}
