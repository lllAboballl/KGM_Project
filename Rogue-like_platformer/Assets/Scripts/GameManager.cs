using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static string transitionedFromScene;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); }

        else { Instance = this; }
        DontDestroyOnLoad(gameObject);
    }
    
}
