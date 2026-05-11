using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomExit : MonoBehaviour
{
    [SerializeField] string transitionTo;
    [SerializeField] Transform startPoint;
    [SerializeField] Vector2 exitDirection;
    [SerializeField] float exitTime;

    [SerializeField] GameObject exitText;

    void Start()
    {
        exitText.SetActive(false);
        Debug.Log(GameManager.transitionedFromScene);
        if (GameManager.transitionedFromScene == transitionTo)
        {
            Player_Controller.Instance.transform.position = startPoint.position;

            StartCoroutine(Player_Controller.Instance.WalkIntoNewScene(exitDirection, exitTime));
        }

        StartCoroutine(UIManager.Instance.sceneFader.Fade(SceneFader.FadeDirection.Out));
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) { return; }
        exitText.SetActive(true);

        if (!Input.GetButtonDown("Interact")) { return; }

        GameManager.transitionedFromScene = SceneManager.GetActiveScene().name;

        Debug.Log(GameManager.transitionedFromScene);

        Player_Controller.Instance.playerState.transitioning = true;

        StartCoroutine(UIManager.Instance.sceneFader.FadeAndLoadScene(SceneFader.FadeDirection.In, transitionTo));
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) { return; }
        exitText.SetActive(false);
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

