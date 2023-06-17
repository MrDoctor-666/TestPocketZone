using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private Button selectButton;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private Button fullScreenButton;
    public Button deleteLootButton;

    private void Start()
    {
        selectButton.onClick.AddListener(OpenDelete);
        deleteLootButton.onClick.AddListener(UnsetUI);
        fullScreenButton.onClick.AddListener(CloseDelete);
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
        fullScreenButton.gameObject.SetActive(true);
    }

    public void CloseDelete()
    {
        fullScreenButton.gameObject.SetActive(false);
    }

}
