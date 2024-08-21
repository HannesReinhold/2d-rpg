using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 1;
    public float crouchSpeed = 0.5f;
    public float sprintSpeed = 1.5f;
    public float acceleration = 0.1f;


    private bool isMoving;
    private bool isCrouching;
    private bool isSprinting;

    private float currentTargetSpeed;
    private float currentSpeed = 0;

    public PlayerAnimator playerAnimator;

    public Vector2 currentDirection;

    private bool canMove = true;
    private float movementMult = 1;



    // Update is called once per frame
    void FixedUpdate()
    {
        movementMult = canMove ? 1 : 0;

        float moveX = Input.GetAxis("Horizontal") * movementMult;
        float moveY = Input.GetAxis("Vertical") * movementMult;

        float moveXRaw = Input.GetAxisRaw("Horizontal") * movementMult;
        float moveYRaw = Input.GetAxisRaw("Vertical") * movementMult;

        // set state
        isMoving = (moveXRaw != 0 || moveYRaw != 0);
        isSprinting = Input.GetKey(KeyCode.LeftControl);
        isCrouching = Input.GetKey(KeyCode.LeftShift);


        // save last direction
        if (moveXRaw != 0) { currentDirection.x = moveXRaw; currentDirection.y = 0; }
        if (moveYRaw != 0) {currentDirection.y = moveYRaw; currentDirection.x = 0; }


        // set speed based on movement state
        if (isSprinting) currentTargetSpeed = sprintSpeed;
        else if (isCrouching) currentTargetSpeed = crouchSpeed;
        else if (isMoving) currentTargetSpeed = walkSpeed;
        else currentTargetSpeed = 0;

        // apply movement to player
        currentSpeed = (1 - acceleration) * currentSpeed + acceleration * currentTargetSpeed;
        Vector3 moveDirection = new Vector3(moveX, moveY,0).normalized;
        transform.position += moveDirection * Time.fixedDeltaTime * currentSpeed;


        // controll animator
        playerAnimator.SetState(currentDirection, new Vector2(moveX,moveY).magnitude);
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }


}

public enum PlayerDirection
{
    Up,
    Right,
    Down,
    Left
}

public enum PlayerMovementState
{
    Idle,
    Walking,
    Running,
    Crouching
}