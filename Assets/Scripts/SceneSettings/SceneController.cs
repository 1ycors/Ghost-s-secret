using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    private string targetSpawnPointName;
    public void LoadNewScene(string sceneName, string spawnPointName)
    {
        targetSpawnPointName = spawnPointName;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName); //загружаем сцену
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (var point in spawnPoints)
        {
            if (point.name == targetSpawnPointName)
            {
                Player.Instance.transform.position = point.transform.position;
                break;
            }
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
