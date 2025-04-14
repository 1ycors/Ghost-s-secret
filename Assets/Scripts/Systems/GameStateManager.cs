using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetProgress();
            Debug.Log("Прогресс сброшен!");
        }
    }
    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll(); 
    }
    public void MarkItemAsPicked(string id) 
    {
        PlayerPrefs.SetInt("item_" + id, 1);
        PlayerPrefs.Save();
    }
    public bool IsItemPicked(string id) 
    {
        return PlayerPrefs.GetInt("item_" + id, 0) == 1;
    }
}
