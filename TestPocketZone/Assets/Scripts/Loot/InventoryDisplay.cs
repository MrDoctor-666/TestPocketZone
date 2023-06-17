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
    [SerializeField] private Button nextButton;
    public Button deleteLootButton;
    
    public LootConfig CurrentDisplayed
    {
        get => lootItemDisplayed[displayedLootIndex];
    }

    List<LootConfig> lootItemDisplayed;
    private int displayedLootIndex = 0;

    private void Start()
    {
        selectButton.onClick.AddListener(OpenDelete);
        deleteLootButton.onClick.AddListener(UnsetUI);
        fullScreenButton.onClick.AddListener(CloseDelete);
        nextButton.onClick.AddListener(ShowNext);

        lootItemDisplayed = new List<LootConfig>();
    }

    public void SetUI(LootConfig config)
    {
        if (!lootItemDisplayed.Contains(config))
            lootItemDisplayed.Add(config);

        displayedLootIndex -= 1;
        ShowNext();
    }

    public void ShowNext()
    {
        if (lootItemDisplayed.Count <= 0) return;
        displayedLootIndex = (displayedLootIndex + 1) % lootItemDisplayed.Count;
        int amount = Root.PlayerReference.GetComponent<Inventory>().GetLootAmount(lootItemDisplayed[displayedLootIndex]);
        selectButton.gameObject.SetActive(true);
        if (amount == 1) amountText.gameObject.SetActive(false);
        else
        {
            amountText.gameObject.SetActive(true);
            amountText.text = amount.ToString();
        }

        selectButton.GetComponent<Image>().sprite = lootItemDisplayed[displayedLootIndex].sprite;

    }

    public void UnsetUI()
    {
        lootItemDisplayed.RemoveAt(displayedLootIndex);
        if (lootItemDisplayed.Count == 0)
            selectButton.gameObject.SetActive(false);
        else
        {
            displayedLootIndex -= 1;
            ShowNext();
        }
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
