using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlichManager : MonoBehaviour
{
    public IGlich[] m_gliches;
    private int m_levelOfGlich = 0;
    public UnityEvent onMaximunLevelOfGlich;
    public UnityEvent onMinimunLevelOfGlich;

    // Start is called before the first frame update
    void Start()
    {
        m_levelOfGlich = m_gliches.Length / 2; // 50% de gliches
        for (int i = 0; i < m_gliches.Length; ++i)
        {
            if (i < m_levelOfGlich)
            {
                m_gliches[i].glich();
            }
            else
            {
                m_gliches[i].fix();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // returns true if the minimun level has been reached
    public bool ReduceLevelOfGlich()
    {
        Debug.Log("quitamos un glich");
        if (m_gliches.Length == 0)
        {
            return true;
        }
        --m_levelOfGlich;
        m_gliches[m_levelOfGlich].fix();
        if (m_levelOfGlich <= 0)
        {
            Debug.Log("Todos los glich arreglados");
            onMinimunLevelOfGlich.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }

    // returns true if the maximun level has been reached
    public bool AddLevelOfGlich()
    {
        Debug.Log("creamos un glich");
        if (m_gliches.Length == 0)
        {
            return true;
        }
        m_gliches[m_levelOfGlich].glich();
        ++m_levelOfGlich;
        if (m_levelOfGlich >= m_gliches.Length)
        {
            onMaximunLevelOfGlich.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }
}
