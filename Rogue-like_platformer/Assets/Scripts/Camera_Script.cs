using UnityEngine;

public class CameraScript : MonoBehaviour
{
 //[SerializeField] Transform playerPosition;
 [SerializeField] Vector3 offset = new Vector3(0, 2, -10);
 [SerializeField] private float smoothSpeed = 5f;
 void LateUpdate()
 {
     if (Player_Controller.Instance.transform.position == null) return;
     {
       Vector3 targetPos = Player_Controller.Instance.transform.position + offset;
       transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);  
     }
 }
}