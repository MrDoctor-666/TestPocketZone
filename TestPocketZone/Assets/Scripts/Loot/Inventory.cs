using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IDataSaved
{
    private Dictionary<LootConfig, int> storedLoot = new Dictionary<LootConfig, int>();
    private InventoryDisplay inventoryDisplay;

    private void Start()
    {
        inventoryDisplay = Root.UIManager.inventory;
        inventoryDisplay.deleteLootButton.onClick.AddListener(delegate { DeleteLoot(inventoryDisplay.CurrentDisplayed); });
        foreach(var item in storedLoot)
            inventoryDisplay.SetUI(item.Key);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Loot")
        {
            var loot = other.gameObject.GetComponent<Loot>();
            var config = loot.GetLootConfig;
            if (!storedLoot.ContainsKey(config))
            {
                storedLoot[config] = config.amount;
                loot.CollectLoot();
            }
            else if (storedLoot.ContainsKey(config))
            {
                storedLoot[config] = storedLoot[config] + config.amount;
                loot.CollectLoot();
            }
            //open ui
            inventoryDisplay.SetUI(config);
        }
    }

    public void DeleteLoot(LootConfig config) => storedLoot.Remove(config);

    public void LoadData(GameData data)
    {
        foreach (var item in data.inventoryNameToAmount)
        {
            var loot = Resources.Load<LootConfig>(item.Key);
            storedLoot[loot] = item.Value;
        }
    }

    public void SaveData(GameData data)
    {
        foreach (var item in storedLoot)
        {
            Debug.Log("Object name: " + item.Key.name);
            data.inventoryNameToAmount[item.Key.name] = item.Value;
        }
    }

    public int GetLootAmount(LootConfig config) => storedLoot[config];
}
