using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceManager : MonoBehaviour
{
    public int totalXp = 1;

    [SerializeField] int currentLevel = 0;

    [SerializeField] Image fillimage;

    [SerializeField] TextMeshProUGUI levelText;

    void Start()
    {
        
    }

    void Update()
    {
        UpdateXpBar();
    }

    int LevelCalculator(int xp)
    {
        int calculatedLevel = 0;
        while (xp > 0)
        {
            xp -= ((int)Mathf.Pow(calculatedLevel, 2.25f) + 2);
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

    public void UpdateXpBar()
    {
        currentLevel = LevelCalculator(totalXp);
        levelText.text = currentLevel.ToString();

        int xpRequiredForNextLevel = GetXpRequiredForLevel(currentLevel + 1);
        int xpRequiredForCurrentLevel = GetXpRequiredForLevel(currentLevel);

        float xpAfterLastLevel = (float)totalXp -
            (float)xpRequiredForCurrentLevel;

        float xpDifferenceBetweenLastAndNextLevel = (float)xpRequiredForNextLevel -
            (float)xpRequiredForCurrentLevel;

        float xpBarPercentage = xpAfterLastLevel / xpDifferenceBetweenLastAndNextLevel;

        fillimage.fillAmount = xpBarPercentage;

    }

}

