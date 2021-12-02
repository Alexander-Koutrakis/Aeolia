using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
public class Inventory:MonoBehaviour
{
    private static List<Item> inventoryItems=new List<Item>();
    public static List<Item> InventoryItems { get { return inventoryItems; } }
    private static Item itemHolding;
    public static Item ItemHolding { get { return itemHolding; } }
    [SerializeField] private GameObject itemButtonPrefab;
    private GridLayoutGroup gridLayoutGroup;
    public static Inventory Instance;
    [SerializeField] private Image itemHoldingImageBackground;

    public void Awake()
    {
        Instance = this;
        gridLayoutGroup = GetComponentInChildren<GridLayoutGroup>(true);
        gameObject.SetActive(false);
    }
  
    private void Start()
    {
        ShowItems();
    }

    public void TakeItem(Item item)
    {
        if (!inventoryItems.Contains(item))
        {
            inventoryItems.Add(item);
            if (item.ItemType == ItemType.KeyItem)
            {
                ShowItem(item);
                NotificationController.Instance.Notify(Notification.NewItem);
            }
            else
            {
                NotificationController.Instance.Notify(Notification.NewContainer);
            }

            
        }
    }

    public void HoldItem(Item item)
    {
        itemHolding = item;
        itemHoldingImageBackground.gameObject.SetActive(true);
        Image holdingItemImage = itemHoldingImageBackground.GetComponentsInChildren<Image>(true)[1];
        holdingItemImage.sprite = item.Sprite;
    }

   public void RemoveItem(Item item)
    {
        inventoryItems.Remove(item);
    }
    
    public static void SaveInventory()
    {
        Gamemaster.SavedGame.SaveInventory(inventoryItems);
    }

    public static void LoadInventory()
    {
        string[] itemNames = Gamemaster.SavedGame.LoadItems();
        inventoryItems.Clear();
        for(int i = 0; i < itemNames.Length; i++)
        {
            Item savedItem = Resources.Load<Item>("/Items/" + itemNames[i]);
            inventoryItems.Add(savedItem);
        }
    }

    public void ShowItem(Item item)
    {
        GameObject itemButtonClone = Instantiate(itemButtonPrefab, gridLayoutGroup.transform);
        Button itemButton = itemButtonClone.GetComponent<Button>();
        Image buttonImage = itemButtonClone.GetComponentsInChildren<Image>()[2];
        TMP_Text buttonText = itemButtonClone.GetComponentInChildren<TMP_Text>();

        buttonImage.sprite = item.Sprite;
        buttonImage.preserveAspect = true;
        buttonText.text = item.Name;
        Item buttonItem = item;
        itemButton.onClick.AddListener(delegate { HoldItem(buttonItem); });
        itemButton.onClick.AddListener(delegate { HoldItemButtonSprite(itemButtonClone); });
        itemButton.onClick.AddListener(delegate { DeselectItems(itemButton); });
        itemButtonClone.name = item.Name;
    }

    public void ShowItems()
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].ItemType == ItemType.KeyItem)
            {
                Debug.Log(inventoryItems[i].ItemType);
                ShowItem(inventoryItems[i]);
            }
        }
    }

    private void HoldItemButtonSprite(GameObject prefabObject)
    {
        Image buttonImage = prefabObject.GetComponentsInChildren<Image>()[0];
        buttonImage.color = new Color32(255, 255, 255, 255);
    }

    private void NonHoldItemButtonSprite(GameObject prefabObject)
    {
        Image buttonImage = prefabObject.GetComponentsInChildren<Image>()[0];
        buttonImage.color = new Color32(255, 255, 255, 0);
    }

    private void DeselectItems(Button buttonPressed)
    {
        Button[] itemButtons = gridLayoutGroup.GetComponentsInChildren<Button>();
        for(int i = 0; i < itemButtons.Length; i++)
        {
            if (itemButtons[i] != buttonPressed)
            {
                NonHoldItemButtonSprite(itemButtons[i].gameObject);
            }
        }
    }
}
