using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search_Area_Ranged : MonoBehaviour
{
    public GameObject _Enermy_Range;
    private Enermy_Range ranged;

    void Start()
    {
        ranged = _Enermy_Range.GetComponent<Enermy_Range>();
    }

    // Collider
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            ranged.runAway = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            ranged.runAway = false;
        }
    }
}
