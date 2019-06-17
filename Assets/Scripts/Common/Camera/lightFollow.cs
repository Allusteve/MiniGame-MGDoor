using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        offset = new Vector3(0, 1.6f, 0);
        transform.position = player.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
    }
}
