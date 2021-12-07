using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PanelController : MonoBehaviour
{

   [SerializeField]private RectTransform buttonPanel;
   [SerializeField] private Button[] buttons;
   [SerializeField] private RectTransform[] rectTransforms;
    [SerializeField] private Button closeButton;
    [SerializeField] private TMP_Text titleText;
    private bool moving = false;
    public static PanelController Instance;

    public void Awake()
    {
        Instance = this;
    }

    private IEnumerator MovePanel(RectTransform panel, Vector2 targetLocation, float duration,bool hide)
    {
        moving = true;
        float t = 0f;
        while (t < 1f)
        {
            panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, targetLocation, t);
            t += Time.deltaTime / duration;
            yield return null;
        }

        panel.gameObject.SetActive(false);
        panel.anchoredPosition = targetLocation;
        moving = false;
    }
    private IEnumerator MovePanel(RectTransform panel,Vector2 targetLocation,float duration)
    {
        moving = true;
        float t = 0f;
        while (t<1f)
        {
            panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, targetLocation,t);
            t += Time.deltaTime / duration;
            yield return null;
        }

        panel.anchoredPosition = targetLocation;
        moving = false;
    }
    public void CenterPosition(RectTransform panel)
    {
        panel.gameObject.SetActive(true);
        StartCoroutine(MovePanel(panel, Vector2.zero, 0.5f));
    }
    public void RightHidePosition(RectTransform panel)
    {
        Vector2 hideposition = new Vector2(panel.rect.width, 0);
        StartCoroutine(MovePanel(panel, hideposition, 0.5f,true));
    }
    public void HidePanels()
    {
       for(int i = 0; i < rectTransforms.Length; i++)
        {
            RightHidePosition(rectTransforms[i]);
        }
        RightHidePosition(buttonPanel);
    }
    public void ShowPanels()
    {
        CenterPosition(buttonPanel);
    }
    public void NonInteractableButton(Button nonInteractableButton)
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] != nonInteractableButton)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }
    public void OpenPanel(RectTransform rectTransform)
    {
        for (int i = 0; i < rectTransforms.Length; i++)
        {
            if (rectTransforms[i] != rectTransform)
            {
                RightHidePosition(rectTransforms[i]);
            }
            else
            {
                CenterPosition(rectTransforms[i]);
            }
        }

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(delegate { ClosePanel(rectTransform); });
        closeButton.gameObject.SetActive(true);
        ShowTitle(rectTransform.name);
    }

    private void ClosePanel(RectTransform rectTransform)
    {
        RightHidePosition(rectTransform);
        closeButton.gameObject.SetActive(false);
        HideTitle();
        for (int i = 0; i < buttons.Length; i++)
        {          
          buttons[i].interactable = true;
        }

    }

    private void ShowTitle(string text)
    {
        titleText.gameObject.SetActive(true);
        titleText.text = text;
    }

    private void HideTitle()
    {
        titleText.gameObject.SetActive(false);
    }
}
