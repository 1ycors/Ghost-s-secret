using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private string spawnPointName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SceneController.Instance.LoadNewScene(sceneName, spawnPointName);
    }
}
