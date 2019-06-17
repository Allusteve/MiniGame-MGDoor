using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1PlayerOutToPipe : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().CanStand = true;
            Destroy(gameObject);
        }
    }
}
