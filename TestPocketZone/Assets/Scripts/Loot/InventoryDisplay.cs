using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private Button selectButton;
    [SerializeField] private TextMeshProUGUI amountText;
    public Button deleteLootButton;

    private void Start()
    {
        selectButton.onClick.AddListener(OpenDelete);
        deleteLootButton.onClick.AddListener(UnsetUI);
    }

    public void SetUI(LootConfig config, int amount)
    {
        selectButton.gameObject.SetActive(true);
        if (amount == 1) amountText.gameObject.SetActive(false);
        else
        {
            amountText.gameObject.SetActive(true);
            amountText.text = amount.ToString();
        }

        selectButton.GetComponent<Image>().sprite = config.sprite;
    }

    public void UnsetUI()
    {
        selectButton.gameObject.SetActive(false);
        CloseDelete();
    }

    public void OpenDelete()
    {
        deleteLootButton.gameObject.SetActive(true);
    }

    //need to open somewhere
    public void CloseDelete()
    {
        deleteLootButton.gameObject.SetActive(false);
    }

}
