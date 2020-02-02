using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleGlichUIComponent : IGlich
{
    public Sprite[] m_glichImage;
    public Sprite m_fixedImage;
    public Image m_spriteComponent;
    [Range(0, 10)]
    public float amountOfGlich;
    private float levelOfGLich;

    void Update()
    {
        int size = m_glichImage.Length;
        int index = (int)(size * levelOfGLich / 10);
        if (index >= m_glichImage.Length) index = m_glichImage.Length - 1;
        m_spriteComponent.sprite = m_glichImage[index];
    }
    // Start is called before the first frame update
    void Start()
    {
        int size = m_glichImage.Length;
        int index = (int)(size * levelOfGLich / 10);
        m_spriteComponent.sprite = m_glichImage[index];
    }

    public override void glich(float level)
    {
        levelOfGLich = level * 10;
        if (levelOfGLich < 1)
            m_spriteComponent.sprite = m_fixedImage;
        else
        {
            int size = m_glichImage.Length;
            int index = (int)(size * levelOfGLich / 10);
            if (index >= m_glichImage.Length) index = m_glichImage.Length - 1;
            m_spriteComponent.sprite = m_glichImage[index];
        }
    }
}
