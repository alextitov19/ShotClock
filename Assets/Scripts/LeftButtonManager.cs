using UnityEngine;
using UnityEngine.EventSystems;

public class LeftButtonManager : MonoBehaviour
{
    public static bool leftButtonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        leftButtonPressed = true;
        Debug.Log("Left Button Pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        leftButtonPressed = false;
    }
}
