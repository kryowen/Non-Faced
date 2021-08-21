using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float movePower = 2f; // (기본)
    public float timeCount = 0; // 투사체의 지속시간을 육안으로 확인하기위해 public 선언

    public int missileDamage = 0; // 투사체 데미지(원거리는 투사체 데미지만 적용됨)
    public int type = 0; // 0: 기본, 1: 지속시간이 존재하지 않는다.

    public GameObject _Enermy_Range;
    private Enermy_Range range;

    Vector3 targetPos;

    void Start()
    {
        timeCount = 0; // 초기화
        targetPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount > 5f && type.Equals(0))
        {
            Destroy(gameObject);
        } // 타입이 0인 경우, 일정시간이 지나면 사라진다.

        Move();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (type.Equals(0) || type.Equals(1))
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, (movePower * Time.deltaTime));
        }
    }

    //Collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("투사체 → Player : " + missileDamage); // 플레이어 콜라이더를 통해서 데미지를 적용 시킬 것!!
            Destroy(gameObject); // 파괴
        }

        if (other.gameObject.tag.Equals("Wall"))
        {
            Debug.Log("투사체 → 벽");
            Destroy(gameObject); // 파괴
        }

    }
}
