using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomExit : MonoBehaviour
{
    [SerializeField] string transitionTo;
    [SerializeField] Transform startPoint;
    [SerializeField] Vector2 exitDirection;
    [SerializeField] float exitTime;

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) { return; }

        if (!Input.GetButtonDown("Interact")) { return; }

        SceneManager.LoadScene(transitionTo);
    }
}














    /* [SerializeField] GameObject exitRoomText;
    [SerializeField] GameObject leftRoomExit;
    [SerializeField] GameObject rightRoomExit;
    [SerializeField] GameObject topRoomExit;
    [SerializeField] GameObject bottomRoomExit;

    void Awake()
    {
        exitRoomText.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) { return; }

        exitRoomText.SetActive(true);

        if (!Input.GetButtonDown("Interact")) { return; }
        Debug.Log("e");
        if (gameObject.CompareTag("RightExit"))
        {
            Debug.Log("RightExit");
            other.gameObject.transform.position = leftRoomExit.transform.position;
        }
        else if (gameObject.CompareTag("LeftExit"))
        {
            Debug.Log("LeftExit");
            other.gameObject.transform.position = rightRoomExit.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) { return; }

        exitRoomText.SetActive(false);
    }*/

