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
    string increaseIntensity = "Increase";
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

        int randomNumber = Random.Range(1,5);
        if (randomNumber == 2) { statIncreaseValue *= 2; }
        Debug.Log(randomNumber);

        if(statIncreaseValue > statIncreaseRange.y) { increaseIntensity = "Drastically increase"; }
        else if (statIncreaseValue > statIncreaseRange.y / 2) { increaseIntensity = "Increase"; }
        else { increaseIntensity = "Slightly increase"; }

        string upgradeInfo = $" {increaseIntensity} {statName}\n{statValue} -> {(statValue + statIncreaseValue)}";
        buttonText.text = upgradeInfo;

        Debug.Log("button " + buttonIndex + " Stat value:" + statValue +
          " Stat Name: " + statName + " Increase with: " + statIncreaseValue);
    }

    public void LevelUpButtonFunction()
    {
        Debug.Log("Pressed Button " + buttonIndex);
        statManager.IncreaseStat(statName, statIncreaseValue); 
    }
}