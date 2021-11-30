using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemaster : MonoBehaviour
{
    public static Gamemaster Instance;
    public static SavedGame SavedGame;

    private void Awake()
    {
        Instance = this;
        SaveSystem.Delete_Save();
        if (SaveSystem.LoadGame() != null)
        {
            Debug.Log("load game");
            SavedGame = SaveSystem.LoadGame();

        }
        else
        {
            SaveSystem.Save();
        }
    }

    private void Start()
    {
        Inventory.LoadInventory();
        SceneNavigation.LoadAvailableScenes();
    }

}
