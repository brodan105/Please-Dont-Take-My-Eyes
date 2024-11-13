using UnityEngine;
using System.Collections;
using System;

public class KeyCollide : MonoBehaviour
{

    public static KeyCollide instance;

    public bool hasKey = false;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Key")
        {
            TimeController.instance.StopTimer();
            Destroy(collision.gameObject);

            hasKey = true;
        }
    }
}
