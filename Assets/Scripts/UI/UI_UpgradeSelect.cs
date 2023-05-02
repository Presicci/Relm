using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UI_UpgradeSelect : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    [SerializeField] private UI_UpgradeSelectButton buttonPrefab;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private int offensiveUpgrades;
    [SerializeField] private int defensiveUpgrades;
    [SerializeField] private int utilityUpgrades;
    private bool _locked;
    
    public void GenerateChoices()
    {
        foreach (GameObject child in buttonContainer)
        {
            Destroy(child);
        }
        List<UpgradeScriptableObject> possibleOffensive = new List<UpgradeScriptableObject>(UpgradeDef.LoadedOffensiveUpgrades);
        List<UpgradeScriptableObject> possibleDefensive= new List<UpgradeScriptableObject>(UpgradeDef.LoadedDefensiveUpgrades);
        List<UpgradeScriptableObject> possibleUtility = new List<UpgradeScriptableObject>(UpgradeDef.LoadedUtilityUpgrades);
        for (int index = 0; index < Math.Min(offensiveUpgrades, possibleOffensive.Count); index++)
        {
            UI_UpgradeSelectButton button = Instantiate(buttonPrefab, buttonContainer);
            UpgradeScriptableObject upgrade = possibleOffensive[Random.Range(0, possibleOffensive.Count)];
            button.SetupButton(upgrade, this);
            possibleOffensive.Remove(upgrade);
        }
        for (int index = 0; index < Math.Min(defensiveUpgrades, possibleDefensive.Count); index++)
        {
            UI_UpgradeSelectButton button = Instantiate(buttonPrefab, buttonContainer);
            UpgradeScriptableObject upgrade = possibleDefensive[Random.Range(0, possibleDefensive.Count)];
            button.SetupButton(upgrade, this);
            possibleDefensive.Remove(upgrade);
        }
        for (int index = 0; index < Math.Min(utilityUpgrades, possibleUtility.Count); index++)
        {
            UI_UpgradeSelectButton button = Instantiate(buttonPrefab, buttonContainer);
            UpgradeScriptableObject upgrade = possibleUtility[Random.Range(0, possibleUtility.Count)];
            button.SetupButton(upgrade, this);
            possibleUtility.Remove(upgrade);
        }
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    private void Close()
    {
        _locked = true;
        Time.timeScale = 1f;
        _locked = false;
        gameObject.SetActive(false);
    }

    private IEnumerator CloseWithDelay()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        Time.timeScale = 1f;
        _locked = false;
        gameObject.SetActive(false);
    }

    public void TakeUpgrade(UpgradeScriptableObject upgrade)
    {
        if (_locked) return;
        playerAttributes.IncrementAttribute(upgrade.upgradeAttribute, upgrade.increase);
        Close();
    }
}
