using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class GlichComponent : IGlich
{
    public Sprite m_glichImage;
    public Sprite m_fixedImage;
    private SpriteRenderer m_spriteComponent;
    private Animator m_animator;
    [Range(1,10)]
    public float levelOfGlichToFix = 5;

    // Start is called before the first frame update
    void Start()
    {
        m_spriteComponent = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
        m_spriteComponent.sprite = m_glichImage;
        StartCoroutine(changeAnimator());
    }

    IEnumerator changeAnimator()
    {
        while (true)
        {

            if (m_spriteComponent.sprite == m_glichImage)
            {
                m_animator.speed = UnityEngine.Random.Range(0.0f, 3.0f);
            }
            yield return new WaitForSeconds(2.5f);
        }
        
    }

    public override void glich(float level)
    {
        m_spriteComponent.sprite = m_glichImage;
        bool fix = level < levelOfGlichToFix;
        m_animator.SetBool("fixed", fix);
    }
}
