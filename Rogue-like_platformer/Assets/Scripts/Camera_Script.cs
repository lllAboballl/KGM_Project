using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Vector3 offset = new Vector3(0, 2, -10);
    [SerializeField] private float smoothSpeed = 5f;

    Player_Controller playerController;


    private void Start()
    {
        playerController = FindFirstObjectByType<Player_Controller>();
        if (Player_Controller.Instance.transform.position != null)
        {
            transform.position = Player_Controller.Instance.transform.position + offset;
        }
    }
    void LateUpdate()
 {
        if (Player_Controller.Instance.transform.position != null) 
     {
       Vector3 targetPos = Player_Controller.Instance.transform.position + offset;
       transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);  
     }
 }
}