using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    private void Start()
    {
        startButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();

        startButton.onClick.AddListener(StartTheGame);
        exitButton.onClick.AddListener(EndTheGame);
    }
    private void StartTheGame() 
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.DeleteAll();
        if (Player.Instance != null)
            Destroy(Player.Instance.gameObject);
    }
    private void EndTheGame() 
    {
        Application.Quit();
        Debug.Log("The game quited!");
    }
}
