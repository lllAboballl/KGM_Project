using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceManager : MonoBehaviour
{
    public int totalXp = 1;

    [SerializeField] Image fillimage;
    [SerializeField] TextMeshProUGUI levelText;

    [SerializeField] Canvas levelUpUI;

    StatManager statManager;
    LevelUpButton levelUpbutton;

    int currentLevel = 1;
    int levelBeforeCalc = 1;
    
    void Awake()
    {
        statManager = GetComponent<StatManager>();
        levelUpbutton = FindFirstObjectByType<LevelUpButton>();
        UpdateXpProgression();

    }

    void Update()
    {
        //UpdateXpProgression();
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
        levelBeforeCalc = currentLevel;
        currentLevel = LevelCalculator(totalXp);
        if (levelBeforeCalc < currentLevel ) { LevelUp(); }

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

    void LevelUp()
    {
        Debug.Log("Level Up!");
        levelUpUI.gameObject.SetActive(true);

        /*
        if ((float)currentLevel / 10f - Mathf.Floor((float)currentLevel / 10f) != 0)
        {
            Debug.Log("Damn");
        }
        else
        {
            Debug.Log("Zamn");
        }
        */
    }

}