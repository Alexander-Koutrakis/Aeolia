using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemaster : MonoBehaviour
{
    public static Gamemaster Instance;
    public static SavedGame SavedGame;
    public static PlayerGender PlayerGender;
    public static Sprite PlayerPortraitSprite;
    public static Sprite PlayerSprite;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
[System.Serializable]
public enum PlayerGender { Male,Female}
