using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrying : MonoBehaviour
{
    public GameObject _MeleeMovement;
    private Melee_Movement melee;

    // Start is called before the first frame update
    void Start()
    {
        melee = _MeleeMovement.GetComponent<Melee_Movement>();
    }

    // Collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Shield Effect")
        {
            Debug.Log("패링당함");
            melee.isStunned = true;
            melee.StartCoroutine("Stunned");
        }
    }
}
