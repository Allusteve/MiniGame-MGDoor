using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "playerRegion")
        {
            GetComponent<Renderer>().enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "playerRegion")
        {
            GetComponent<Renderer>().enabled = true;
            GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.6f);
        }
    }

}
