using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public SceneFader sceneFader;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); } 

        else { Instance = this; }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        sceneFader = GetComponentInChildren<SceneFader>();
    }
}
