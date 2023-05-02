using TMPro;
using UnityEngine;

public class UI_UpgradeSelectButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    private UpgradeScriptableObject _upgrade;
    private UI_UpgradeSelect _upgradeSelect;

    public void Click()
    {
        Debug.Log("click");
        _upgradeSelect.TakeUpgrade(_upgrade);
    }
    
    public void SetupButton(UpgradeScriptableObject upgrade, UI_UpgradeSelect uiUpgradeSelect)
    {
        _upgradeSelect = uiUpgradeSelect;
        _upgrade = upgrade;
        gameObject.SetActive(true);
        textMesh.text = upgrade.upgradeDescription;
    }
}
