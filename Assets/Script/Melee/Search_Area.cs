using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search_Area : MonoBehaviour
{
    GameObject traceTarget; // 추적대상

    public GameObject _MeleeMovement;
    private Melee_Movement melee;

    void Start()
    {
        melee = _MeleeMovement.GetComponent<Melee_Movement>();
    }

    // Collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player 감지");
            traceTarget = other.gameObject; // 추적 대상(플레이어) 설정
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {     
        if (other.gameObject.tag == "Player")
        {
            melee.isTracing = true;
            melee.animator.SetBool("isMoving", true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            melee.isTracing = false;
            melee.StartCoroutine("ChangeMovement");
        }
    }
}
