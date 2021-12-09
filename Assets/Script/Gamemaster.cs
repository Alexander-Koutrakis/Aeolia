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
        if (SaveSystem.SaveExists())
        {
            SavedGame = SaveSystem.LoadGame();
            CurrentScene = SavedGame.LastScene;
            PlayerGender = PlayerGenderFromString(SavedGame.PlayerGender);
            Inventory.LoadInventory();
            SceneNavigation.LoadAvailableScenes();
            SetPortraitSprite();
            Debug.Log(CurrentScene);
        }       
    }

  

    public static void NewGame()
    {

        SaveSystem.Delete_Save();
        Inventory.NewGame();
        SceneNavigation.NewGame();
        SaveSystem.Save();
        SavedGame.SavePlayerGender(PlayerGender);
    }

    private PlayerGender PlayerGenderFromString(string playergender)
    {
        if (playergender == "Male")
        {
            return PlayerGender.Male;
        }
        else
        {
            return PlayerGender.Female;
        }
    }

    private void SetPortraitSprite()
    {
        if (PlayerGender == PlayerGender.Male)
        {
            Gamemaster.PlayerGender = PlayerGender.Male;
            Gamemaster.PlayerPortraitSprite = Resources.Load<Sprite>("MalePortrait");
        }
        else
        {
            Gamemaster.PlayerGender = PlayerGender.Female;
            Gamemaster.PlayerPortraitSprite = Resources.Load<Sprite>("FemalePortrait");
        }
    }
}
[System.Serializable]
public enum PlayerGender { Male,Female}
