using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlichComponent : MonoBehaviour
{
    public SpriteRenderer m_spriteComponent;
    public Sprite m_glichImage;
    public Sprite m_fixedImage;
    public Animator m_animator;

    public void fixImage()
    {
        m_spriteComponent.sprite = m_fixedImage;
        m_animator.StopPlayback();
    }
    // Start is called before the first frame update
    void Start()
    {
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
