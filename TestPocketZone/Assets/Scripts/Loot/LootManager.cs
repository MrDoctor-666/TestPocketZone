using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour, IDataSaved
{
    public void LoadData(GameData data)
    {
        foreach(var loot in data.lootNameToPlace)
        {
            LootConfig config = Resources.Load<LootConfig>(loot.Key);
            var spawnedLoot = Instantiate(config.prefab, loot.Value, Quaternion.identity);
            spawnedLoot.GetComponent<Loot>().Initialize(config);
        }
    }

    public void SaveData(GameData data)
    {
        var loot = FindObjectsOfType<Loot>();
        foreach(var lootPiece in loot)
        {
            data.lootNameToPlace[lootPiece.GetLootConfig.name] = lootPiece.transform.position;
        }
    }
}
