using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackScript : MonoBehaviour
{
    public Button basketButton;
    public Transform space;

    private bool onOff;

    public void OpenBackpack()
    {
        space.gameObject.SetActive(true);
        onOff = true;
    }

    public void CloseBackpack()
    {
        space.gameObject.SetActive(false);
        basketButton.GetComponent<BasketScript>().CloseBasket();
        onOff = false;
    }

    public void ToggleBackpack()
    {
        if (onOff)
            CloseBackpack();
        else
            OpenBackpack();
    }
}
