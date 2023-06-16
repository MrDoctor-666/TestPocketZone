using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] int enemyToSpawn = 3;
    [SerializeField] Vector3Int playerSpawn = new Vector3Int(0, 0, 0);
    
    private List<Vector3> tileWorldLocations;
    private Vector3 offset = new Vector3(0.5f, 0.5f, 0);

    private void Start()
    {
        tileWorldLocations = new List<Vector3>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = tilemap.CellToWorld(localPlace);
            if (tilemap.HasTile(localPlace) && !localPlace.Equals(playerSpawn))
            {
                tileWorldLocations.Add(place);
            }
        }

        Instantiate(playerPrefab, playerSpawn + offset, Quaternion.identity);

        for (int i = 0; i < enemyToSpawn; i++)
        {
            int indexPlace = Random.Range(0, tileWorldLocations.Count);
            int indexPrefab = Random.Range(0, enemyPrefabs.Count);

            Instantiate(enemyPrefabs[indexPrefab], tileWorldLocations[indexPlace] + offset, Quaternion.identity);

            tileWorldLocations.RemoveAt(indexPlace);
        }

    }
}
