using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Vector3Int playerSpawn = new Vector3Int(0, 0, 0);
    
    private List<Vector3> tileWorldLocations;
    private Vector3 offset = new Vector3(0.5f, 0.5f, 0);

    private void Start()
    {
        tileWorldLocations = new List<Vector3>();

        SpawnPlayer();
        SpawnEnemies();

        Root.SaveManager.Initialize();
    }

    private void SpawnPlayer()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = tilemap.CellToWorld(localPlace);
            if (tilemap.HasTile(localPlace) && !localPlace.Equals(playerSpawn))
            {
                tileWorldLocations.Add(place);
            }
        }

        var player = Instantiate(playerPrefab, playerSpawn + offset, Quaternion.identity);
        Root.PlayerReference = player.GetComponent<PlayerController>();
        player.GetComponent<PlayerController>().Initialize();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            int indexPlace = Random.Range(0, tileWorldLocations.Count);

            var enemy = Instantiate(enemyPrefabs[i], tileWorldLocations[indexPlace] + offset, Quaternion.identity);
            enemy.GetComponent<EnemyController>().Initialize(i);

            tileWorldLocations.RemoveAt(indexPlace);
        }

    }
}
