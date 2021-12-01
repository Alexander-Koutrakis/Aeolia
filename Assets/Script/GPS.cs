using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GPS : MonoBehaviour
{
    [SerializeField] private GameObject sceneButtonPrefab;
    private GridLayoutGroup gridLayoutGroup;

    private void Awake()
    {
        gridLayoutGroup = GetComponentInChildren<GridLayoutGroup>(true);
    }

    private void Start()
    {
        ShowAvailableScenes();
    }

    private void ShowAvailableScenes()
    {
        
        for(int i = 0; i < SceneNavigation.AvailableScenes.Count; i++)
        {
            GameObject newSceneGameobject = Instantiate(sceneButtonPrefab, gridLayoutGroup.transform);
            Button sceneButton = newSceneGameobject.GetComponent<Button>();
            Image sceneImage = newSceneGameobject.GetComponentsInChildren<Image>()[1];
            TMP_Text newSceneText= newSceneGameobject.GetComponentInChildren<TMP_Text>();

            SceneData shownScene = new SceneData();
            shownScene = SceneNavigation.AvailableScenes[i];
            sceneButton.onClick.AddListener(delegate { SceneLoader.Instance.LoadScene(shownScene.SceneLoaderName); });
            sceneImage.sprite = shownScene.SceneSprite;
            newSceneText.text = shownScene.SceneName;
        }
    }

    public void LoadToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
