using UnityEngine;
using UnityEngine.EventSystems;

public class FireButtonManager : MonoBehaviour
{
    public static bool fireButtonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        fireButtonPressed = true;
        Debug.Log("Fire Button Pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        fireButtonPressed = false;
    }
}
