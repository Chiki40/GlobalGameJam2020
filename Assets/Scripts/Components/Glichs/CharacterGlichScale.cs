﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGlichScale : IGlich
{
    public bool m_enabled = false;
    public Transform m_transform;
    public bool scaleInX = false;
    public int maxScaleInX = 10;
    public bool scaleInY = false;
    public int maxScaleInY = 10;
    private bool m_wait = false;

    public override void glich(float level)
    {
        maxScaleInX = maxScaleInY = (int)level*10;
        if (level > 0)
            m_enabled = true;
        else
            m_enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(scaleGlichX());
        StartCoroutine(scaleGlichY());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator scaleGlichY()
    {
        while (true)
        {
            if (m_enabled && scaleInY)
            {
                if (m_transform.transform.localScale.y > maxScaleInY*2)
                {
                    Vector3 scale = m_transform.transform.localScale;
                    scale.y = 1;
                    m_transform.transform.localScale = scale;
                    yield return new WaitForSeconds(UnityEngine.Random.Range(3, 5));

                }
                float speed = UnityEngine.Random.Range(0.0f, 1.0f);
                m_transform.transform.localScale += new Vector3(0, speed);
                yield return null;
            }
            yield return null;
        }
    }
    IEnumerator scaleGlichX()
    {
        while (true)
        {
            if (m_enabled && scaleInX)
            {
                if (m_transform.transform.localScale.x > maxScaleInX*2)
                {
                    yield return new WaitForSeconds(UnityEngine.Random.Range(3,5));
                    Vector3 scale = m_transform.transform.localScale;
                    scale.x = 1;
                    m_transform.transform.localScale = scale;
                    yield return new WaitForSeconds(UnityEngine.Random.Range(3, 5));

                }
                float speed = UnityEngine.Random.Range(0.0f, 1.0f);
                m_transform.transform.localScale += new Vector3(speed,0);
                yield return null;
            }
            yield return null;
        }
    }
}
