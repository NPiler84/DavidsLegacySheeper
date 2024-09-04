using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    public HeartSystem heart;

    private void onCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            heart.vida--;
        }
    }
}
