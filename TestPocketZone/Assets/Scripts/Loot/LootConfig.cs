using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootConfig", menuName = "ScriptableObjects/LootConfig", order = 1)]
public class LootConfig : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public int amount;
}
