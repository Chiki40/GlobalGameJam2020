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
            m_gliches[i].glich((float)m_levelOfGlich / (float)m_gliches.Length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // returns true if the minimun level has been reached
    public void ReduceLevelOfGlich()
    {
        Debug.Log("quitamos un glich");
        --m_levelOfGlich;
        if (m_levelOfGlich == 0) onMinimunLevelOfGlich.Invoke();
        for (int i = 0; i < m_gliches.Length; ++i)
        {
            m_gliches[i].glich((float)m_levelOfGlich / (float)m_gliches.Length);
        }
    }

    // returns true if the maximun level has been reached
    public void AddLevelOfGlich()
    {
        Debug.Log("creamos un glich");
        ++m_levelOfGlich;
        if (m_levelOfGlich >= m_gliches.Length) onMaximunLevelOfGlich.Invoke();
        for (int i = 0; i < m_gliches.Length; ++i)
        {
            float level = (float)m_levelOfGlich / (float)m_gliches.Length;
            m_gliches[i].glich(level);
        }
    }
}
