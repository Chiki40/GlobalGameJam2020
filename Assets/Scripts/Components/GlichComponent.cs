using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class GlichComponent : MonoBehaviour
{
    public Sprite m_glichImage;
    public Sprite m_fixedImage;
    private SpriteRenderer m_spriteComponent;
    private Animator m_animator;

    public void fixImage()
    {
        m_spriteComponent.sprite = m_fixedImage;
        m_animator.SetBool("fixed", true);
    }
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
                m_animator.speed = Random.Range(0.0f, 3.0f);
            }
            yield return new WaitForSeconds(2.5f);
        }
        
    }
}
