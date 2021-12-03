using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemaster : MonoBehaviour
{
    public static Gamemaster Instance;
    public static SavedGame SavedGame;
    public static PlayerGender PlayerGender;
    public static Sprite PlayerPortraitSprite;
    public static string CurrentScene;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        SaveSystem.Delete_Save();
        if (SaveSystem.LoadGame() != null)
        {
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
        CurrentScene = SavedGame.CurrentScene;
    }

}
[System.Serializable]
public enum PlayerGender { Male,Female}
