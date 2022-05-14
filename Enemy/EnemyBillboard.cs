using UnityEngine;

public class EnemyBillboard : MonoBehaviour
{
    public Transform playerCam;
    public int drawDistance;
    private float distance;

    private void Start()
    {
        playerCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Always called after Update(), to ensure cam interaction is stable
    void LateUpdate()
    {
        // face one ahead of current transform towards playerCamera
        distance = Vector3.Distance(playerCam.position, transform.position);
        transform.LookAt(transform.position + playerCam.forward);

        if (distance < drawDistance)
        {
            transform.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }
        else 
        {
            transform.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }
    }
}
