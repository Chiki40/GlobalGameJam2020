using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteOutline))]
public class ChangeMouse : MonoBehaviour
{
    public Texture2D _normalMouse;
    public Texture2D _specialMouse;
    public SpriteOutline _spriteOutline;

    private void Start()
    {
        Cursor.SetCursor(_normalMouse, Vector2.zero, CursorMode.Auto);
        _spriteOutline.enabled = false;
    }

    private void OnMouseEnter()
    {
        if(this.enabled)
        {
            Cursor.SetCursor(_specialMouse, Vector2.zero, CursorMode.Auto);
            _spriteOutline.enabled = true;
        }
    }

    public void OnMouseExit()
    {
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
    