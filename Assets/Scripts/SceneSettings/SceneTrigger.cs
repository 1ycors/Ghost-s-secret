using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public string sceneName;
    public string spawnPointName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SceneController.Instance.LoadNewScene(sceneName, spawnPointName);
    }
}
