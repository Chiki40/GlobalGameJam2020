using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class ClickableComponent : MonoBehaviour
{
    public UnityEvent OnMouseDownEvent;
    public UnityEvent OnMouseUpEvent;
    private bool _dragged = false;

    private void Awake()
    {
        _dragged = false;
    }

    public void OnMouseDown()
    {
        if (!_dragged)
        {
            _dragged = true;
            OnMouseDownEvent.Invoke();
        }
    }

    public void OnMouseUp()
    {
        if (_dragged)
        {
            OnMouseUpEvent.Invoke();
            _dragged = false;
        }
    }
}
