using UnityEngine;
using UnityEngine.EventSystems;

public class ForwardButtonManager : MonoBehaviour
{
    public static bool forwardButtonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        forwardButtonPressed = true;
        Debug.Log("Forward Button Pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        forwardButtonPressed = false;
    }
}
