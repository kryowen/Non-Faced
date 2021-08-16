using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class search_shoot : MonoBehaviour
{
    public GameObject _Enermy_Range;
    private Enermy_Range ranged;

    void Start()
    {
        ranged = _Enermy_Range.GetComponent<Enermy_Range>();
    }

    // Collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            ranged.canShoot = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            ranged.shootControl();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            ranged.canShoot = false;
        }
    }
}
