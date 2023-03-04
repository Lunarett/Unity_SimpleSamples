using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CameraBob : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float bobSpeed = 0.2f;
    [SerializeField] private float bobAmount = 0.1f;
    [SerializeField] private float midpoint = 2.0f;

    private float timer = 0.0f;
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Check if the character controller is moving and is grounded
        if (controller.velocity.magnitude > 0.1f && controller.isGrounded)
        {
            // Calculate the new position of the camera based on a sine wave
            float waveslice = Mathf.Sin(timer);
            timer = timer + bobSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }
            float translateChange = waveslice * bobAmount;
            float totalAxes = Mathf.Clamp(controller.velocity.magnitude, -1.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            Vector3 localPosition = cameraTransform.localPosition;
            localPosition.y = midpoint + translateChange;
            cameraTransform.localPosition = localPosition;
        }
        else
        {
            // Reset the camera position if the character controller is not moving
            Vector3 localPosition = cameraTransform.localPosition;
            localPosition.y = midpoint;
            cameraTransform.localPosition = localPosition;
        }
    }
}
