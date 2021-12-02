using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneNavigation
{
    private static List<SceneData> availableScenes = new List<SceneData>();
    public static List<SceneData> AvailableScenes { get { return availableScenes; } }

    public static void AddAvailableScene(SceneData newScene)
    {
        if (!availableScenes.Contains(newScene))
        {
            availableScenes.Add(newScene);
            NotificationController.Instance.Notify(Notification.NewLocation);
        }
    }

    public static void SaveAvailableScenes()
    {
        string[] sceneNames = new string[availableScenes.Count];
        for(int i = 0; i < availableScenes.Count; i++)
        {
            sceneNames[i] = availableScenes[i].SceneName;
        }
        Gamemaster.SavedGame.SaveAvailableScenes(sceneNames);
    }

    public static void LoadAvailableScenes()
    {
        string[] sceneNames= Gamemaster.SavedGame.LoadAvailableScenes();
        availableScenes.Clear();
        for (int i = 0; i < sceneNames.Length; i++)
        {
            SceneData availableScene= Resources.Load<SceneData>("/SceneData/" + sceneNames[i]);
            availableScenes.Add(availableScene);
        }
    }


}
