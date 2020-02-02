using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class GlichComponent : IGlich
{
    public Sprite m_glichImage;
    public Sprite m_fixedImage;
    private SpriteRenderer m_spriteComponent;
    [Range(1,10)]
    public float levelOfGlichToFix = 5;

    // Start is called before the first frame update
    void Start()
    {
        m_spriteComponent = GetComponent<SpriteRenderer>();
        m_spriteComponent.sprite = m_glichImage;
    }

    public override void glich(float level)
    {
        m_spriteComponent.sprite = m_glichImage;
        bool fix = level < levelOfGlichToFix;
        if (fix)
            m_spriteComponent.sprite = m_fixedImage;
        else
            m_spriteComponent.sprite = m_glichImage;
    }
}
