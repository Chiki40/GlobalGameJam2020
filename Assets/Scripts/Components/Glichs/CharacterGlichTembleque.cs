using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGlichTembleque : IGlich
{
    public float displacement = 10;
    public bool displamentInX = true;
    public bool displamentInY = false;
    public bool m_enabled = false;
    public Transform m_transform;
    [Range(1, 10)]
    public int amountOfGlich = 1;
    private Vector3 m_originalPosition;

    public void setEnable(bool enable)
    {
        m_enabled = enable;
    }

    public void setAmountOfGlich(int amount)
    {
        if (amount > 0 && amount <= 10)
            amountOfGlich = amount;
    }

    public override void fix()
    {

    }

    public override void glich()
    {

    }
   
    // Start is called before the first frame update
    void Start()
    {
        m_originalPosition = m_transform.transform.position;
        StartCoroutine(stopGlich());
    }

    IEnumerator stopGlich()
    {
        while (true)
        {
            if (m_enabled)
            {
                for (int i = 10 - amountOfGlich; i < amountOfGlich + 2*(amountOfGlich);++i)
                {
                    Vector3 pos = m_originalPosition;
                    if (displamentInX)
                        pos.x += UnityEngine.Random.Range(-displacement, displacement);
                    if (displamentInY)
                        pos.y += UnityEngine.Random.Range(-displacement, displacement);
                    m_transform.transform.position = pos;
                    yield return null;
                }
                m_transform.transform.position = m_originalPosition;
                yield return new WaitForSeconds((10 - amountOfGlich) * 0.5f);
            }
            yield return null;
        }   
    }
}
