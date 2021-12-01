using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
   [SerializeField] private GameObject[] panels;
    [SerializeField] private Sprite femaleIntro;
    public void OpenPanel(GameObject panel)
    {
        for(int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }

        panel.SetActive(true);
    }

    public void SetPlayerGender(string playerGender)
    {
        if (playerGender == "Male")
        {
            Gamemaster.PlayerGender = PlayerGender.Male;
            Gamemaster.PlayerPortraitSprite = Resources.Load<Sprite>("MalePortrait");
            Gamemaster.PlayerSprite = Resources.Load<Sprite>("MaleAvatar");
        }
        else
        {
            Gamemaster.PlayerGender = PlayerGender.Female;
            Gamemaster.PlayerPortraitSprite = Resources.Load<Sprite>("FemalePortrait");
            Gamemaster.PlayerSprite = Resources.Load<Sprite>("FemaleAvatar");
        }

        for(int i = 0; i < panels.Length; i++)
        {
            if (panels[i].name == "Intro")
            {
                if (playerGender == "Female")
                    panels[i].GetComponent<Image>().sprite = femaleIntro;

                break;
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Aivali");
    }
}
