using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6.0f;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float gravity = -20.0f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isInputActive = true;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!isInputActive) return;
        
        if (controller.isGrounded)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            
            moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        moveDirection.y += gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void SetInputActive(bool isActive)
    {
        isInputActive = isActive;
    }
}
