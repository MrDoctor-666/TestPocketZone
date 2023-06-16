using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    private LootConfig lootConfig;
    public LootConfig GetLootConfig => lootConfig;
    public void Initialize(LootConfig loot)
    {
        lootConfig = loot;
        StartCoroutine(waitToInitialize());
    }

    IEnumerator waitToInitialize()
    {
        yield return new WaitForSeconds(1f);
    }

    public void CollectLoot()
    {
        Destroy(gameObject);
    }
}
