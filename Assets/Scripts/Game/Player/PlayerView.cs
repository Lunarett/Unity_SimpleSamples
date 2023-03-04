using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float sensitivity = 3.0f;
    [SerializeField] private Vector2 verticalClamp = new Vector2(-89.0f, 89.0f);

    private float rotationY = 0.0f;
    private bool isInputActive = true;

    public Camera PlayerCamera => playerCamera;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!isInputActive) return;
        
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, verticalClamp.x, verticalClamp.y);

        // Horizontal Rotation
        transform.localEulerAngles = new Vector3(0, rotationX, 0);
        
        // Vertical Rotation
        Transform cameraTransform = playerCamera.transform;
        cameraTransform.localEulerAngles = new Vector3(-rotationY, cameraTransform.localEulerAngles.y, 0);
    }
    
    public void SetInputActive(bool isActive)
    {
        isInputActive = isActive;
    }
}
