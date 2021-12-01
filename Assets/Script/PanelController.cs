using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PanelController : MonoBehaviour
{
    [SerializeField]private RectTransform inventoryPanel;
    [SerializeField] private RectTransform gpsPanel;
    [SerializeField]private RectTransform buttonPanel;
    [SerializeField] private Button openInventoryButton;
    [SerializeField] private Button gPSButton;
    private bool moving = false;
    public static PanelController Instance;

    private void Awake()
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
    public void OpenInventory()
    {
        if (moving)
        {
            return;
        }      
            CenterPosition(inventoryPanel);
            RightHidePosition(gpsPanel);
        openInventoryButton.interactable = false;
        gPSButton.interactable = true;
    }
    public void CloseInventory()
    {
        if (moving)
        {
            return;
        }
        RightHidePosition(inventoryPanel);

        openInventoryButton.interactable = true;   
    }
    public void OpenGPS()
    {
        if (moving)
        {
            return;
        }
        CenterPosition(gpsPanel);
        RightHidePosition(inventoryPanel);
        openInventoryButton.interactable = true;
        gPSButton.interactable = false;
    }
   
    public void CloseGPS()
    {
        if (moving)
        {
            return;
        }
        RightHidePosition(gpsPanel);
        gPSButton.interactable = true;
    }
    public void HidePanels()
    {
        RightHidePosition(inventoryPanel);
        RightHidePosition(gpsPanel);
        RightHidePosition(buttonPanel);
    }
    public void ShowPanels()
    {
        CenterPosition(buttonPanel);
    }

  
}
