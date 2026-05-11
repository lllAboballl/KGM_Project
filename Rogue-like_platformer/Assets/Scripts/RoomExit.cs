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
        if (GameManager.transitionedFromScene == transitionTo)
        {
            Player_Controller.Instance.transform.position = startPoint.position;

            StartCoroutine(Player_Controller.Instance.WalkIntoNewScene(exitDirection, exitTime));
        }

        Debug.Log(UIManager.Instance.sceneFader.Fade(SceneFader.FadeDirection.Out));
        StartCoroutine(UIManager.Instance.sceneFader.Fade(SceneFader.FadeDirection.Out));
    }

    void Update()
    {
        Debug.Log(UIManager.Instance.sceneFader.Fade(SceneFader.FadeDirection.Out));
        
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