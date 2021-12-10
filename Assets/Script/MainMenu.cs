using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private Sprite femaleIntro;
    [SerializeField] private Sprite maleIntro;
    [SerializeField] private Button continueButton;
    private void Start()
    {
        if(!SaveSystem.SaveExists())
        {
            continueButton.interactable = false;
        }

    }
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
        }
        else
        {
            Gamemaster.PlayerGender = PlayerGender.Female;
            Gamemaster.PlayerPortraitSprite = Resources.Load<Sprite>("FemalePortrait");
        }

        for(int i = 0; i < panels.Length; i++)
        {
            if (panels[i].name == "Intro")
            {
                if (playerGender == "Female")
                {
                    panels[i].GetComponent<Image>().sprite = femaleIntro;
                }
                else if(playerGender=="Male")
                {
                    panels[i].GetComponent<Image>().sprite = maleIntro;
                }
                break;
            }
        }
    }

    

    public void ContinueGame()
    {
        SceneManager.LoadScene(Gamemaster.CurrentScene);
    }
    
}
