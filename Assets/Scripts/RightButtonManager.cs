using UnityEngine.EventSystems;
using UnityEngine;

public class RightButtonManager : MonoBehaviour
{
    public static bool rightButtonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        rightButtonPressed = true;
        Debug.Log("Right Button Pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rightButtonPressed = false;
    }
}
