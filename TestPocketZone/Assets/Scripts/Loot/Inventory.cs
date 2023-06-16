using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private KeyValuePair<LootConfig, int> storedLoot;
    private InventoryDisplay inventoryDisplay;

    private void Start()
    {
        inventoryDisplay = Root.UIManager.inventory;
        storedLoot = new KeyValuePair<LootConfig, int>(null, 0);
        inventoryDisplay.deleteLootButton.onClick.AddListener(DeleteLoot);
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
            }
            //open ui
            inventoryDisplay.SetUI(storedLoot.Key, storedLoot.Value);
        }
    }

    public void DeleteLoot() => storedLoot = new KeyValuePair<LootConfig, int>(null, 0);
}
