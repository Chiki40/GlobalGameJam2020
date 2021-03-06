﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
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
