using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class СellScript : MonoBehaviour, IPointerClickHandler
{
    public bool isFree;
    public ItemCell item;

    private Text textCell;

    private void OnEnable()
    {
        textCell = GetComponent<Text>();
        ChangeDisplayItem();
    }

    void BasketActive()
    {
        var basketButton = transform.parent.GetComponent<InventaryScript>().basketButton;
        if (basketButton.GetComponent<BasketScript>().СompareCell(this.gameObject))
            basketButton.GetComponent<BasketScript>().CloseBasket();
        else
            basketButton.GetComponent<BasketScript>().GetCell(this.gameObject);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (item.countItems > 0)
            BasketActive();
    }

    public void AddItemCell(GameObject itemObj)
    {
        if (item.objectItem == null)
            CreateItemCell(itemObj);
        item.countItems += 1;
        ChangDisplayQuantity();
        isFree = false;
    }

    public void AddItemOfName(string nameObj)
    {
        if(nameObj != "")
            CreateItemCell(Resources.Load<GameObject>("Prefab/Item/" + nameObj));
    }

    private void CreateItemCell(GameObject itemObj)
    {
        if (!itemObj)
            return;
        var itemImage = itemObj.GetComponent<SpriteRenderer>();
        if (itemImage != null)
        {
            GameObject cellObj = new GameObject(itemObj.name);
            var cellImage = cellObj.AddComponent<SpriteRenderer>();
            cellImage.sprite = itemImage.sprite;
            cellImage.sortingLayerName = "UX";
            item.objectItem = cellObj;
            item.nameItems = itemObj.name;
            PlaceInCell();
        }
    }

    public void DecreaseItemCell()
    {
        if (item.objectItem != null)
        {
            item.countItems--;
        }
        if (item.countItems < 1)
        {
            ClearCell();
        }
        ChangDisplayQuantity();
    }

    private void PlaceInCell()
    {
        item.objectItem.transform.SetParent(this.transform);
        item.objectItem.transform.position = this.transform.position;
        item.objectItem.transform.localScale = new Vector3(120f, 120f, 1f);
    }

    private void ClearCell()
    {
        item = new ItemCell
        {
            countItems = 0
        };
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
        isFree = true;
    }

    public void ChangeDisplayItem()
    {
        if (item.objectItem == null)
        {
            if (item.nameItems != "")
                AddItemOfName(item.nameItems);
            else
                ClearCell();
        }
        else
            CreateItemCell(item.objectItem);
        ChangDisplayQuantity();
    }

    public void ChangDisplayQuantity()
    {
        if (item.countItems > 1)
            textCell.text = item.countItems.ToString();
         else
            textCell.text = "";
    }
}
