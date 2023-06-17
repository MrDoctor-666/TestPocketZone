using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour
{
    [SerializeField] List<LootConfig> lootConfig;

    public void Drop()
    {
        int index = Random.Range(0, lootConfig.Count);
        var loot = Instantiate(lootConfig[index].prefab, this.transform.position, Quaternion.identity);
        loot.GetComponent<Loot>().Initialize(lootConfig[index]);
    }
}
