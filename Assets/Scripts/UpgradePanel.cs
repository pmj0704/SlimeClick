using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    #region UIs

    [SerializeField]
    private Text nameText = null;

    [SerializeField]
    private Text amountText = null;

    [SerializeField]
    private Text priceText = null;

    [SerializeField]
    private Image unitImage = null;

    [SerializeField]
    private Image backgroundImage = null;

    [SerializeField]
    private Button buyButton = null;

    [SerializeField]
    private Sprite[] unitSprite = null;

    private Units unit = null;
    #endregion

    public void SetValue(Units unit)
    {
        this.unit = unit;
        UpdateUI();
    }

    public void UpdateUI()
    {
        unitImage.sprite = unitSprite[unit.imageNumber];
        nameText.text = unit.name;
        priceText.text = string.Format("{0}", unit.price);
        amountText.text = string.Format("{0}", unit.amount);
    }
    public void OnClickPurchase()
    {
        if (GameManager.Instance.currentUser.slimes < unit.price)
        {
            return;
        }

        GameManager.Instance.currentUser.slimes -= unit.price;
        Units unitsInList = GameManager.Instance.currentUser.unitList.Find((x) => x.name == unit.name);
        unitsInList.amount++;
        unitsInList.price = (long)(unitsInList.price + Mathf.Round((Mathf.Pow(unitsInList.amount, 1.5f))));
        UpdateUI();
        GameManager.Instance.uiManager.UpdateEnergyPanel();
    }

}
