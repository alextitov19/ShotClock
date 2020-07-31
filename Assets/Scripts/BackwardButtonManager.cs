using UnityEngine;
using UnityEngine.EventSystems;
public class BackwardButtonManager : MonoBehaviour
{
    public static bool backwardButtonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        backwardButtonPressed = true;
        Debug.Log("Backward Button Pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        backwardButtonPressed = false;
    }
}
