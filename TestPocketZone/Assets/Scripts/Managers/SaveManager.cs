using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName = "GameSave";

    private GameData gameData;
    private List<IDataSaved> dataPersistenceObjects;
    private SaveData dataHandler;

    public void Initialize()
    {
        dataHandler = new SaveData(Application.persistentDataPath, fileName);
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        // load any saved data from a file using the data handler
        gameData = dataHandler.Load();

        // if no data can be loaded, initialize to a new game
        if (gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
            return;
        }

        // push the loaded data to all other scripts that need it
        foreach (IDataSaved dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        gameData = new GameData();
        // pass the data to other scripts so they can update it
        foreach (IDataSaved dataPersistenceObj in dataPersistenceObjects)
        {
            if ((Object)dataPersistenceObj != null)
                dataPersistenceObj.SaveData(gameData);
        }

        // save that data to a file using the data handler
        dataHandler.Save(gameData);
    }

    public void DeleteData()
    {
        dataHandler.DeleteData();
    }

    private void OnApplicationQuit()
    {
        if (!Root.saveOnExit) DeleteData();
        else SaveGame();
    }

    private List<IDataSaved> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataSaved> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataSaved>();

        return new List<IDataSaved>(dataPersistenceObjects);
    }
}
