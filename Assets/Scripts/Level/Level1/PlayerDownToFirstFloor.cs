using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownToFirstFloor : MonoBehaviour
{
    private float moveStep = 0.5f;

    private Rigidbody2D body;
    private PlayerMovement playerMovement;
    private CharacterController2D characterController;

    void Start()
    {
        transform.position = new Vector3(24.17f, -14.97f, transform.position.z);

        body = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        characterController = GetComponent<CharacterController2D>();

        if (!characterController.m_FacingRight)
        {
            characterController.OnFlip();
        }

        body.gravityScale = 0;
        playerMovement.OnDowning();
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - moveStep * Time.deltaTime, transform.position.z);
    }

    public void destroy()
    {
        body.gravityScale = 3;
        playerMovement.OnDowning();
        Destroy(this);
    }
}
