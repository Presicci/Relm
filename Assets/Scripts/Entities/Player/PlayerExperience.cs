using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private Image experienceFill;
    [SerializeField] private int maxLevel = 100;
    [SerializeField] private float experienceCurve = 1.5f;
    [SerializeField] private UI_UpgradeSelect upgradeSelect;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    
    private int _experience;
    private int _level;
    private int[] _levelExperience;
    
    private void Awake()            
    {
        experienceFill.fillAmount = Mathf.Clamp(0, 0, 1f);
        _levelExperience = new int[maxLevel];
        for (int index = 0; index < maxLevel; index++)
        {
            if (index == 0)
            {
                _levelExperience[0] = 30;
                continue;
            }
            var lastLevelExpReq = GetExperienceForLevel(index) - GetExperienceForLevel(index-1);
            _levelExperience[index] = (int) Math.Ceiling(lastLevelExpReq * experienceCurve) + GetExperienceForLevel(index);
        }
    }

    private int GetExperienceForLevel(int level)
    {
        if (level <= 0 || level >= _levelExperience.Length) return 0;
        return _levelExperience[level - 1];
    }

    private float GetFill()
    {
        var currentLevelExp = GetExperienceForLevel(_level);
        var value = (float) (_experience - currentLevelExp) / (GetExperienceForLevel(_level + 1) - currentLevelExp);
        return Mathf.Clamp(value, 0, 1f);
    }

    public void AddExperience(int experience)
    {
        _experience += experience;
        if (_experience >= GetExperienceForLevel(_level + 1))
        {
            LevelUp();
        }
        else
        {
            experienceFill.fillAmount = GetFill();
        }
    }

    private void LevelUp()
    {
        _level++;
        Debug.Log("Level up! " + _level);
        playerLevelText.text = "" + (_level + 1);
        upgradeSelect.GenerateChoices();
        experienceFill.fillAmount = GetFill();
    }
}
