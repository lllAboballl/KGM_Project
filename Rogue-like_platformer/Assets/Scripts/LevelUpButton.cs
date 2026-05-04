using UnityEngine;
using TMPro;

public class LevelUpButton : MonoBehaviour
{
    [SerializeField] int buttonIndex;
    [SerializeField] Vector2 buttonPosition;

    [SerializeField] TextMeshProUGUI buttonText; 

    StatClass statClass = new StatClass();

    StatManager statManager;

    string statName;
    float statValue;
    Vector2 statIncreaseRange;
    float statIncreaseValue;

    void Awake()
    {
        statManager = FindFirstObjectByType<StatManager>();
    }
    
    void OnEnable()
    {
        statClass = statManager.GetStat("random");

        statName = statClass.name;
        statValue = statClass.statValue;
        statIncreaseRange = statClass.increaseRange;

        statIncreaseValue = Random.Range(statIncreaseRange.x, statIncreaseRange.y);
        statIncreaseValue = Mathf.Round(10 * statIncreaseValue) / 10;

        buttonText.text = "Stat value:" + statValue +
          " Stat Name: " + statName + " Increase with: " + statIncreaseValue;

        Debug.Log("button " + buttonIndex + " Stat value:" + statValue +
          " Stat Name: " + statName + " Increase with: " + statIncreaseValue);
    }

    public void LevelUpButtonFunction()
    {
        Debug.Log("Pressed Button " + buttonIndex);
        statManager.IncreaseStat(statName, statIncreaseValue);
    }
}