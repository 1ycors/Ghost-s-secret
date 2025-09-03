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
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }
    private void EndTheGame() 
    {
        Application.Quit();
        Debug.Log("The game quited!");
    }
}
