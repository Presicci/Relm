using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UI_UpgradeSelect : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerAttributes playerAttributes;
    [SerializeField] private UI_UpgradeSelectButton buttonPrefab;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private int offensiveUpgrades;
    [SerializeField] private int defensiveUpgrades;
    [SerializeField] private int utilityUpgrades;
    private bool _locked;
    
    public void GenerateChoices()
    {
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }
        List<UpgradeScriptableObject> possibleOffensive = new List<UpgradeScriptableObject>(UpgradeDef.LoadedOffensiveUpgrades);
        List<UpgradeScriptableObject> possibleDefensive= new List<UpgradeScriptableObject>(UpgradeDef.LoadedDefensiveUpgrades);
        List<UpgradeScriptableObject> possibleUtility = new List<UpgradeScriptableObject>(UpgradeDef.LoadedUtilityUpgrades);
        var max = Math.Min(offensiveUpgrades, possibleOffensive.Count);
        for (int index = 0; index < max; index++)
        {
            UI_UpgradeSelectButton button = Instantiate(buttonPrefab, buttonContainer);
            UpgradeScriptableObject upgrade = possibleOffensive[Random.Range(0, possibleOffensive.Count)];
            button.SetupButton(upgrade, this);
            possibleOffensive.Remove(upgrade);
        }
        max = Math.Min(defensiveUpgrades, possibleDefensive.Count);
        for (int index = 0; index < max; index++)
        {
            UI_UpgradeSelectButton button = Instantiate(buttonPrefab, buttonContainer);
            UpgradeScriptableObject upgrade = possibleDefensive[Random.Range(0, possibleDefensive.Count)];
            button.SetupButton(upgrade, this);
            possibleDefensive.Remove(upgrade);
        }
        max = Math.Min(utilityUpgrades, possibleUtility.Count);
        for (int index = 0; index < max; index++)
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
        gameObject.SetActive(false);
        _locked = true;
        playerController.ContinueGame();
        _locked = false;
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
