using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLetter : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private Button closeButton;
    private void Start()
    {
        if (GameStateManager.Instance.IsObjectMarked(window))
        {
            window.SetActive(false);
        }
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(CloseLetter);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        window.SetActive(true);
    }
    private void CloseLetter()
    {
        window.SetActive(false);
        GameStateManager.Instance.MarkObjectAsInteracted(window, true);
    }
}
