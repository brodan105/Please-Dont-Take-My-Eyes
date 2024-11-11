using UnityEngine;
using System.Collections;
using System;

public class KeyCollide : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Key")
        {
            TimeController.instance.StopTimer();
            Destroy(collision.gameObject);
        }
    }
}
