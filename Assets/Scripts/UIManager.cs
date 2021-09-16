using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text slimeText = null;
    [SerializeField]
    private UpgradePanel upgradePanelTemplet = null;

    private List<UpgradePanel> upgradePanelList = new List<UpgradePanel>();

    void Start()
    {
        UpdateEnergyPanel();
        CreatePanels();
    }

    public void CreatePanels()
    {
        GameObject panel = null;
        UpgradePanel panelComponent = null;
        foreach (Units units in GameManager.Instance.currentUser.unitList)
        {
            panel = Instantiate(upgradePanelTemplet.gameObject, upgradePanelTemplet.transform.parent);
            panelComponent = panel.GetComponent<UpgradePanel>();
            panelComponent.SetValue(units);
            panel.SetActive(true);
            upgradePanelList.Add(panelComponent);
        }
    }

    public void OnClickSlime()
    {
        GameManager.Instance.currentUser.slimes += GameManager.Instance.currentUser.slimePerClick;
        UpdateEnergyPanel();
    }
    public void UpdateEnergyPanel()
    {
        slimeText.text = string.Format("{0} ΩΩ∂Û¿”", GameManager.Instance.currentUser.slimes);
    }
}
