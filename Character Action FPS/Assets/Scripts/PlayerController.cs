using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("Movement Options")]
    public float movementSpeed;
    public bool smooth;
    public float smoothSpeed;

    [Header("Jump Options")]
    public float jumpHeight;

    [Header("Gravity")]
    public float gravity = 2.5f;

    // Private Variables

    // References
    private CharacterController controller;
    
    // Movement Vectors
    private Vector3 velocity;
    private float velocityY;

    private float currentGravity = 0;

    #endregion

    #region Unity Methods

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Gravity();

        if (Input.GetButtonDown("Jump"))
            Jump();

        Move();
    }

    #endregion

    #region Movement Methods

    private void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        velocity = moveDir * movementSpeed;

        velocity = transform.TransformDirection(velocity);

        velocity = velocity + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
    }

    #endregion

    #region Gravity/Grounding

    private void Gravity()
    {
        if (controller.isGrounded)
        {
            velocityY = -0.001f;
        }
        else
        {
            velocityY -= gravity * Time.deltaTime;
        }
    }

    #endregion

    #region Jumping

    private void Jump()
    {
        if(controller.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * -gravity * jumpHeight);
            velocityY = jumpVelocity;
        }
    }

    #endregion

}
