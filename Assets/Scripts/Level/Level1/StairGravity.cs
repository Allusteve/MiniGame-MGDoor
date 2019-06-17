using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairGravity : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("PlayerRegion"))
        {
            GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("PlayerRegion"))
        {
            GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 3.0f;
        }
    }
}
