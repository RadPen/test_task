using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LoadingSceneScript : MonoBehaviour
{
    public GameObject canvasObject;

    public GameObject player { get; private set; }
    public List<GameObject> enemyList { get; private set; }
    public List<GameObject> itemList { get; private set; }
    public ClipScript clip { get; private set; }

    private bool save = true;
    private CreatureSpawner spawner;
    private InventaryScript inventary;
    private const string saveFileName = "savedGame.json";

    [System.Serializable]
    public class GameData
    {
        public List<ParametersData> itemsData;
        public List<ParametersData> enemiesData;
        public ParametersData playerData;
        public List<ItemCell> itemCells;
        public int clipCount;
    }

    private void SaveGameData()
    {
        if (player == null)
            return;
        GameData gameData = new GameData
        {
            enemiesData = AddParameters(enemyList),
            itemsData = AddParameters(itemList),
            playerData = new ParametersData
            {
                type = Type.TypeCreature.Player,
                position = player.transform.position,
                health = player.GetComponent<HealthControlling>().health
            },
            itemCells = inventary.GetItems(),
            clipCount = clip.currentClip
        };      
        var json = JsonUtility.ToJson(gameData);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, saveFileName), json);
    }

    private void LoadGameData()
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            GameData gameData = JsonUtility.FromJson<GameData>(json);

            ParametersData PlayerData = gameData.playerData;
            player.transform.position = PlayerData.position;
            player.GetComponent<HealthControlling>().Initialize(PlayerData.health);
            SetParameters(gameData);
            inventary.SetItems(gameData.itemCells);
            clip.SetClip(gameData.clipCount);
        }
        else
            SpawnStartObjects();
    }

    private List<ParametersData> AddParameters(List<GameObject> objList)
    {
        List<ParametersData> creatureData = new List<ParametersData>();
        foreach (var obj in objList)
        {
            ParametersData data = new ParametersData
            {
                type = obj.GetComponent<CreatureType>().typeCreature,
                prefab = obj.GetComponent<CreatureType>().yourPrefab,
                position = obj.transform.position,
            };
            if (data.type != Type.TypeCreature.Item)
                data.health = obj.GetComponent<HealthControlling>().health;
            creatureData.Add(data);
        }
        return creatureData;
    }

    private void SetParameters(GameData gameData)
    {
        var enemiesData = gameData.enemiesData;
        var itemData = gameData.itemsData;
        var maxCount = Math.Max(enemiesData.Count, itemData.Count);

        for (var i = 0; i < maxCount; i++)
        {
            if (i < itemData.Count)
                spawner.CreateCreature(new Creature(itemData[i]));
            if (i >= enemiesData.Count)
                continue;
            var enemy = spawner.CreateCreature(new Creature(enemiesData[i]));
            enemy.GetComponent<HealthControlling>().Initialize(enemiesData[i].health);
        }
    }

    private void SpawnStartObjects()
    {
        spawner.CreateMultipleCreature(3, GetCameraView(),
            new Creature(new ParametersData
            {
                type = Type.TypeCreature.Monster
            }));
    }

    private Rect GetCameraView()
    {
        var mainCamera = Camera.main;
        var cameraHeight = mainCamera.orthographicSize * 2;
        var cameraWidth = cameraHeight * mainCamera.aspect;
        var cameraField = mainCamera.transform.position;

        var bottomLeft = new Vector2(cameraField.x - cameraWidth / 2, cameraField.y - cameraHeight / 2);
        var topRight = new Vector2(cameraField.x + cameraWidth / 2, cameraField.y + cameraHeight / 2);

        return new Rect(bottomLeft, topRight);
    }

    private void ReloadScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    private void OnEnable()
    {
        clip = FindObjectOfType<ClipScript>();
        spawner = FindObjectOfType<CreatureSpawner>();
        inventary = FindObjectOfType<InventaryScript>();
        StartCoroutine(WaitForAllObjects());
    }

    private void OnDisable()
    {
        if(save)
            SaveGameData();
    }

    private System.Collections.IEnumerator WaitForAllObjects()
    {
        yield return new WaitForEndOfFrame();
        InitialData();
    }

    private void InitialData()
    {
        clip.UpdateClip(120);
        enemyList = new List<GameObject>();
        itemList = new List<GameObject>();
        LoadGameData();
    }

    public void SaveGame()
    {
        SaveGameData();
    }

    public void DownloadGame()
    {
        save = false;
        ReloadScene();
    }

    public void RestartGame()
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        if (File.Exists(path))
            File.Delete(path);
        save = false;
        ReloadScene();
    }

    public void AddObjectScene(GameObject obj)
    {
        if (obj.CompareTag("Player"))
            player = obj;
        else if (obj.CompareTag("Enemy"))
            enemyList.Add(obj);
        else if (obj.CompareTag("Item"))
            itemList.Add(obj);
    }

    public void RemoveObjectScene(GameObject obj)
    {
        if (obj.CompareTag("Player"))
            player = null;
        else if (obj.CompareTag("Enemy"))
            enemyList.Remove(obj);
        else if (obj.CompareTag("Item"))
            itemList.Remove(obj);
    }
}