using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;

    private float moveLimit;
    private Vector2 inputVector;
    private bool active;

    void Start()
    {
        moveLimit = joystickBackground.sizeDelta.x / 2;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!active)
            return;
        var position = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out position);

        position.x = position.x / (joystickBackground.sizeDelta.x * 0.5f);
        position.y = position.y / (joystickBackground.sizeDelta.y * 0.5f);

        inputVector = new Vector2(position.x, position.y);
        if (inputVector.magnitude > 1)
            inputVector.Normalize();

        joystickHandle.anchoredPosition = inputVector * moveLimit;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!RectTransformUtility.RectangleContainsScreenPoint(joystickBackground, eventData.position, eventData.pressEventCamera))
        {
            active = false;
            return;
        }
        active = true;
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }

    public Vector2 GetInput()
    {
        return inputVector;
    }
}
