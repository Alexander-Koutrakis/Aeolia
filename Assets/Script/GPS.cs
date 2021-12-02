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
    public static GPS Instance;


    private void Awake()
    {
        Instance = this;
        gridLayoutGroup = GetComponentInChildren<GridLayoutGroup>(true);
        ShowAvailableScenes();
        gameObject.SetActive(false);
    }

    private void Start()
    {
        //ShowAvailableScenes();
    }

    private void ShowAvailableScenes()
    {     
        for(int i = 0; i < SceneNavigation.AvailableScenes.Count; i++)
        {
            ShowScene(SceneNavigation.AvailableScenes[i]);
        }
    }

    public void ShowScene(SceneData sceneData)
    {
        Debug.Log("Show scene");
        GameObject newSceneGameobject = Instantiate(sceneButtonPrefab, gridLayoutGroup.transform);
        Button sceneButton = newSceneGameobject.GetComponent<Button>();
        Image sceneImage = newSceneGameobject.GetComponentsInChildren<Image>()[2];
        TMP_Text newSceneText = newSceneGameobject.GetComponentInChildren<TMP_Text>();

        SceneData shownScene = Instantiate(sceneData);
        shownScene = sceneData;
        sceneButton.onClick.AddListener(delegate { SceneLoader.Instance.LoadScene(shownScene.SceneLoaderName); });
        sceneImage.sprite = shownScene.SceneSprite;
        newSceneText.text = shownScene.SceneName;
    }

    public void LoadToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
