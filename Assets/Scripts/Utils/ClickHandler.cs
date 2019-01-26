using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnClick;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke();
    }
}