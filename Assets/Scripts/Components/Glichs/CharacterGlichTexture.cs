using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterGlichTexture : IGlich
{
    [Range(1,10)]
    public float amountOfGlich = 1;
    public bool m_enabled = false;
    public Image m_image;
    private bool m_wait = false;
    public Sprite[] m_glichesSprite;
    Sprite m_originalSprite;

    public override void fix()
    {

    }

    public override void glich()
    {

    }
   
    // Start is called before the first frame update
    void Start()
    {
        m_originalSprite = m_image.sprite;
        StartCoroutine(startGlich());
    }

    // Update is called once per frame

    IEnumerator startGlich()
    {
        while (true)
        {
            if (m_enabled)
            {
                int index = 0;
                int times = UnityEngine.Random.Range(10 - (int)amountOfGlich, 10 - (int)amountOfGlich + 10 * ((int)amountOfGlich - 5));
                for (int i = 0; i < times; ++i)
                {
                    index = UnityEngine.Random.Range(0, m_glichesSprite.Length);
                    m_image.sprite = m_glichesSprite[index];
                    yield return new WaitForSeconds(UnityEngine.Random.Range(0.01f, 0.05f));
                }
                m_image.sprite = m_originalSprite;
                yield return new WaitForSeconds(UnityEngine.Random.Range(10 - amountOfGlich, (11 - amountOfGlich) + 0.5f*(10 - amountOfGlich)));
            }
            else
            {
                yield return null;
            }
           
        }
    }
}
