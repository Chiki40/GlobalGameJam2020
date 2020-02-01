using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteOutline))]
public class ChangeMouse : MonoBehaviour
{
    public Texture2D _normalMouse;
    public Texture2D _specialMouse;
    public SpriteOutline _spriteOutline;
    public UnityEvent MouseEnter;
    public UnityEvent MouseExit;

    private void Start()
    {
        Cursor.SetCursor(_normalMouse, Vector2.zero, CursorMode.Auto);
        _spriteOutline.enabled = false;
    }

    private void OnMouseEnter()
    {
        MouseEnter.Invoke();
        if (this.enabled)
        {
            Cursor.SetCursor(_specialMouse, Vector2.zero, CursorMode.Auto);
            _spriteOutline.enabled = true;
        }
    }

    public void OnMouseExit()
    {
        MouseExit.Invoke();
        if (this.enabled)
        {
            Cursor.SetCursor(_normalMouse, Vector2.zero, CursorMode.Auto);
            if (_spriteOutline)
            {
                _spriteOutline.enabled = false;
            }
        }
    }
}
    