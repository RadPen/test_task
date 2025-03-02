using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketScript : MonoBehaviour
{
    private GameObject cellObject;
    private bool onOff;

    public void CloseBasket()
    {
        onOff = true;
        ToggleBasket();
        cellObject = null;
    }

    public void OpenBasket()
    {
        onOff = false;
        ToggleBasket();
    }

    public void ToggleBasket()
    {
        onOff = !onOff;
        this.gameObject.SetActive(onOff);
    }

    public bool ÑompareCell(GameObject cell)
    {
        if (!cellObject || !cell)
            return false;
        return cellObject.name == cell.name;
    }

    public void GetCell(GameObject cell)
    {
        if (cell)
            cellObject = cell;
        OpenBasket();
    }

    public void DeliteItem()
    {
        cellObject.GetComponent<ÑellScript>().DecreaseItemCell();
        CloseBasket();
        cellObject = null;
    }
}
