using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController playerController;
    public float runSpeed = 40f;

    private float horizontalMove = 0f;
    public bool jump = false;
    // Update is called once per frame
    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump") && !jump)
        {
            jump = true;
            print("jump called");
        }
    }

    private void FixedUpdate()
    {
        playerController.Move(Time.fixedDeltaTime * horizontalMove, jump);
    }
}
