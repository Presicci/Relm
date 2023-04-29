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
        List<UpgradeScriptableObject> possibleOffensive = new List<UpgradeScriptableObject>(UpgradeDef.LoadedOffensiveUpgrades);
        List<UpgradeScriptableObject> possibleUtility = new List<UpgradeScriptableObject>(UpgradeDef.LoadedUtilityUpgrades);
        if (possibleUtility.Count < 1 || possibleOffensive.Count < 1)
        {
            Debug.LogError("ERROR: Not enough upgrades possible.");
            return;
        }
        Time.timeScale = 0f;
        _leftUpgrade = possibleOffensive[Random.Range(0, possibleOffensive.Count)];
        possibleOffensive.Remove(_leftUpgrade);
        _middleUpgrade = possibleOffensive[Random.Range(0, possibleOffensive.Count)];
        _rightUpgrade = possibleUtility[Random.Range(0, possibleUtility.Count)];
        leftText.text = _leftUpgrade.upgradeDescription;
        middleText.text = _middleUpgrade.upgradeDescription;
        rightText.text = _rightUpgrade.upgradeDescription;
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
