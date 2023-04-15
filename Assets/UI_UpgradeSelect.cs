using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_UpgradeSelect : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private TextMeshProUGUI middleText;
    [SerializeField] private TextMeshProUGUI rightText;
    
    private UpgradeScriptableObject _leftUpgrade;
    private UpgradeScriptableObject _middleUpgrade;
    private UpgradeScriptableObject _rightUpgrade;
    private bool _locked;
    
    public void GenerateChoices()
    {
        List<UpgradeScriptableObject> possibleUpgrades = new List<UpgradeScriptableObject>(UpgradeDef.LoadedUpgrades);
        if (possibleUpgrades.Count < 3)
        {
            Debug.LogError("ERROR: Not enough upgrades possible.");
            return;
        }
        Time.timeScale = 0f;
        _leftUpgrade = possibleUpgrades[Random.Range(0, possibleUpgrades.Count)];
        possibleUpgrades.Remove(_leftUpgrade); 
        _middleUpgrade = possibleUpgrades[Random.Range(0, possibleUpgrades.Count)];
        possibleUpgrades.Remove(_middleUpgrade);
        _rightUpgrade = possibleUpgrades[Random.Range(0, possibleUpgrades.Count)];
        leftText.text = _leftUpgrade.upgradeName;
        middleText.text = _middleUpgrade.upgradeName;
        rightText.text = _rightUpgrade.upgradeName;
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

    public void TakeLeftUpgrade()
    {
        if (_locked) return;
        playerAttributes.IncrementAttribute(_leftUpgrade.upgradeAttribute, _leftUpgrade.increase);
        Close();
    }
    
    public void TakeMiddleUpgrade()
    {
        if (_locked) return;
        playerAttributes.IncrementAttribute(_middleUpgrade.upgradeAttribute, _middleUpgrade.increase);
        Close();
    }
    
    public void TakeRightUpgrade()
    {
        if (_locked) return;
        playerAttributes.IncrementAttribute(_rightUpgrade.upgradeAttribute, _rightUpgrade.increase);
        Close();
    }
}
