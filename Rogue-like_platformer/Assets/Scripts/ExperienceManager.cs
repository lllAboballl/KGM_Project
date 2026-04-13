using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceManager : MonoBehaviour
{
    public int totalXp = 1;

    [SerializeField] int currentLevel = 0;

    [SerializeField] Image fillimage;
    [SerializeField] TextMeshProUGUI levelText;

    IncreaseStat increaseStat;

    float stat = 1f;

    void Awake()
    {
        increaseStat = GetComponent<IncreaseStat>();
        UpdateXpProgression();
    }

    void Update()
    {
        UpdateXpProgression();
    }

    int LevelCalculator(int xp)
    {
        int calculatedLevel = 0;
        while (xp > 0)
        {
            xp -= ((int)Mathf.Pow(calculatedLevel + 1, 1.5f) + 10);
            calculatedLevel++;
     
        }
        
        return calculatedLevel;
        
    }

    int GetXpRequiredForLevel(int levelToGet)
    {
        int requiredXp = 0;
        while(LevelCalculator(requiredXp) < levelToGet)
        {
            requiredXp++;
        }

        return requiredXp;
    }
    
    public void UpdateXpProgression()
    {
        currentLevel = LevelCalculator(totalXp);
        levelText.text = currentLevel.ToString();

        int xpRequiredForNextLevel = GetXpRequiredForLevel(currentLevel + 1);
        int xpRequiredForCurrentLevel = GetXpRequiredForLevel(currentLevel);

        if ((float)currentLevel / 10f - Mathf.Floor((float)currentLevel / 10f) != 0)
        {
            increaseStat.IncreaseStatFunction(stat);
            Debug.Log("Damn");
        }
        else
        {

            Debug.Log("Zamn");
        }

        float xpAfterLastLevel = (float)totalXp -
            (float)xpRequiredForCurrentLevel;
        float xpDifferenceBetweenLastAndNextLevel = (float)xpRequiredForNextLevel -
            (float)xpRequiredForCurrentLevel;
        float xpBarPercentage = xpAfterLastLevel / xpDifferenceBetweenLastAndNextLevel;

        fillimage.fillAmount = xpBarPercentage;

    }

}

