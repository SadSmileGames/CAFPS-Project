using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float smoothing;
    [SerializeField] private bool clampRotation;

    private Vector2 targetRot;
    private Vector2 smoothV;

    private Transform character;

    private void Awake()
    {
        LockCursor();
    }

    private void Start()
    {
        character = this.transform.parent;
    }

    private void Update()
    {
        // Temp. Replace by call of Input Class
        CameraRotation(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")));
    }

    private void CameraRotation(Vector2 mouseDirection)
    {
        mouseDirection = Vector2.Scale(mouseDirection, new Vector2(mouseSensitivity * smoothing, mouseSensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, mouseDirection.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, mouseDirection.y, 1f / smoothing);

        targetRot += smoothV;

        if (clampRotation)
            targetRot.y = Mathf.Clamp(targetRot.y, -90f, 90f);

        transform.localRotation = Quaternion.AngleAxis(-targetRot.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(targetRot.x, character.transform.up);
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
