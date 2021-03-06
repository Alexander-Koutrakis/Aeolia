using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneMaster : MonoBehaviour
{
    [SerializeField] private Dialogue startDialogue;
    private bool sceneVisited;
    [SerializeField] private string sceneName;
    [SerializeField] private Image playerAvatar;
    [SerializeField] private Sprite femaleSpriteAvatar;
   

    private void Start()
    {
        LoadScene();
        if (playerAvatar != null)
        {
            if (Gamemaster.PlayerGender == PlayerGender.Female)
            {
                playerAvatar.sprite = femaleSpriteAvatar;

            }

        }
        if (startDialogue != null)
        {
            if (!sceneVisited)
            {
                DialogueController.Instance.StartDialogue(startDialogue);
                sceneVisited = true;
            }
            else
            {
                DialogueController.Instance.StartRepeatSceneDialogue();
            }
        }

       
    }
    private void OnDisable()
    {
        SaveScene();
    }
    private void SaveScene()
    {
        SceneSave sceneSave = new SceneSave(sceneName, sceneVisited, ItemSaves());
        Gamemaster.SavedGame.SaveScene(sceneSave);
        Gamemaster.SavedGame.SaveLastScene(SceneManager.GetActiveScene().name);
        Gamemaster.SavedGame.SaveRecordings(RecordingsController.RecordedDialogues);
        Inventory.SaveInventory();
        SceneNavigation.SaveAvailableScenes();
        SaveSystem.Save();
    }

    private void LoadScene()
    {
        if (!Gamemaster.SavedGame.ContainsScene(sceneName))
        {
            return;
        }

        SceneSave sceneSave = Gamemaster.SavedGame.LoadSceneSave(sceneName);
        sceneVisited = sceneSave.SceneVisited;
        ItemContainer[] itemContainers = GetComponentsInChildren<ItemContainer>(true);
        for (int i = 0; i < itemContainers.Length; i++)
        {
            itemContainers[i].LoadItem(sceneSave.LoadItem(itemContainers[i].gameObject.name));
        }
    }

    private ItemSave[] ItemSaves()
    {
        ItemContainer[] itemContainers = GetComponentsInChildren<ItemContainer>(true);
        ItemSave[] itemSaves = new ItemSave[itemContainers.Length];
        for(int i = 0; i < itemContainers.Length; i++)
        {
            itemSaves[i] = itemContainers[i].SaveItem();
        }

        return itemSaves;
    }

    
}
