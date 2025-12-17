using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    public Transform player; 

    [Header("Camera Settings")]
    public float followSpeed = 5f; 
    public float mouseOffsetAmount = 2f; 
    public float dragDistance = 5f; 
    public float returnSpeed = 5f; 

    private bool isDragging = false;

    void LateUpdate()
    {
        Vector3 targetPosition = player.position;

        
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; 

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isDragging = true;

            
            Vector3 dragDir = mouseWorldPosition - player.position;
            if (dragDir.magnitude > dragDistance)
                dragDir = dragDir.normalized * dragDistance;

            targetPosition += dragDir;
        }
        else
        {
            if (isDragging)
            {
                isDragging = false;
            }

            
            Vector3 offset = (mouseWorldPosition - player.position).normalized * mouseOffsetAmount;
            targetPosition += offset;
        }

        
        Vector3 smoothPosition;
        if (isDragging)
            smoothPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        else
            smoothPosition = Vector3.Lerp(transform.position, targetPosition, returnSpeed * Time.deltaTime);

        smoothPosition.z = transform.position.z; 
        transform.position = smoothPosition;
    }
}
