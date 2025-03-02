using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventaryScript : BackpackScript
{
    private Transform[] cells;

    void Start()
    {
        Assign�ells();
        CloseBackpack();
    }

    public void Assign�ells()
    {
        var amountCells = space.childCount;
        cells = new Transform[amountCells];
        for (int i = 0; i < amountCells; i++)
            cells[i] = space.GetChild(i);
    }

    public List<ItemCell> GetItems()
    {
        List<ItemCell> itemCells = new List<ItemCell>();
        foreach (Transform cell in cells)
        {
            itemCells.Add(cell.GetComponent<�ellScript>().item);
        }
        return itemCells;
    }

    public void SetItems(List<ItemCell> itemCells)
    {
        for (int i = 0; i < itemCells.Count; i++)
        {
            cells[i].GetComponent<�ellScript>().item = itemCells[i];
            cells[i].GetComponent<�ellScript>().ChangeDisplayItem();
        }
    }

    public GameObject Find�ellForItem(GameObject itemObj)
    {
        GameObject emptyCell = null;
        foreach (Transform cell in cells)
        {
            if (!cell.GetComponent<�ellScript>().isFree)
            {
                if (cell.GetComponent<�ellScript>().item.objectItem.name == itemObj.name)
                    return cell.gameObject;
            }
            else if (emptyCell == null)
                emptyCell = cell.gameObject;
        }
        return emptyCell;
    }
}
