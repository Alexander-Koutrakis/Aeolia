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
    private void Awake()
    {
        Instance = this;
        gridLayoutGroup = GetComponentInChildren<GridLayoutGroup>(true);
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
            }         
        }
    }

    public void HoldItem(Item item)
    {
        itemHolding = item;
        PanelController.Instance.HidePanels();
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
}
