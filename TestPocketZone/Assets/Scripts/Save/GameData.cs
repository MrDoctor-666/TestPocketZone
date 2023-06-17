using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //player
    public float currentHP;
    public Vector3 currentPosition;

    //enemies
    public SerializableDictionary<int, Vector3> enemyIndexToPositions;
    public SerializableDictionary<int, float> enemyIndexToHP;

    //inventory
    public SerializableDictionary<string, int> inventoryNameToAmount;

    //loot
    public SerializableDictionary<string, Vector3> lootNameToPlace;

    public GameData()
    {
        enemyIndexToHP = new SerializableDictionary<int, float>();
        enemyIndexToPositions = new SerializableDictionary<int, Vector3>();
        inventoryNameToAmount = new SerializableDictionary<string, int>();
        lootNameToPlace = new SerializableDictionary<string, Vector3>();
    }
}


