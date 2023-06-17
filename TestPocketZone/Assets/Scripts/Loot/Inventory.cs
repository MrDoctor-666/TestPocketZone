using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IDataSaved
{
    private KeyValuePair<LootConfig, int> storedLoot = new KeyValuePair<LootConfig, int>(null, 0);
    private InventoryDisplay inventoryDisplay;

    private void Start()
    {
        inventoryDisplay = Root.UIManager.inventory;
        inventoryDisplay.deleteLootButton.onClick.AddListener(DeleteLoot);
        if (storedLoot.Key != null)
            inventoryDisplay.SetUI(storedLoot.Key, storedLoot.Value);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Loot")
        {
            var loot = other.gameObject.GetComponent<Loot>();
            var config = loot.GetLootConfig;
            if (storedLoot.Key == null)
            {
                storedLoot = new KeyValuePair<LootConfig, int>(config, config.amount);
                loot.CollectLoot();
            }
            else if (storedLoot.Key == config)
            {
                storedLoot = new KeyValuePair<LootConfig, int>(config, storedLoot.Value + config.amount);
                loot.CollectLoot();
            }
            //open ui
            inventoryDisplay.SetUI(storedLoot.Key, storedLoot.Value);
        }
    }

    public void DeleteLoot() => storedLoot = new KeyValuePair<LootConfig, int>(null, 0);

    public void LoadData(GameData data)
    {
        foreach (var item in data.inventoryNameToAmount)
        {
            var loot = Resources.Load<LootConfig>(item.Key);
            storedLoot = new KeyValuePair<LootConfig, int>(loot, item.Value);
        }
    }

    public void SaveData(GameData data)
    {
        if (storedLoot.Key != null)
        {
            Debug.Log("Object name: " + storedLoot.Key.name);
            data.inventoryNameToAmount[storedLoot.Key.name] = storedLoot.Value;
        }
    }
}
