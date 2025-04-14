using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateManager
{
    public static void MarkItemAsPicked(string id) 
    {
        PlayerPrefs.SetInt("item_" + id, 1);
        PlayerPrefs.Save();
    }
    public static bool IsItemPicked(string id) 
    {
        return PlayerPrefs.GetInt("item_" + id, 0) == 1;
    }
}
