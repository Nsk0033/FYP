using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDelayActive : MonoBehaviour
{
    public GameObject[] m_activeObj;
    public float m_delayTime;
    float m_time;
	public GameObject[] m_spawnObj;
	public Transform SpawnLocation;
	
	public bool hasActivated;

    private void Start()
    {
        m_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasActivated && Time.time > m_time + m_delayTime)
        {
            for (int i = 0; i < m_activeObj.Length; i++)
            {
                if (m_activeObj[i] != null)
                    m_activeObj[i].SetActive(true);
            }

            for (int i = 0; i < m_spawnObj.Length; i++)
            {
                if (m_spawnObj[i] != null)
                    Instantiate(m_spawnObj[i], SpawnLocation.position, Quaternion.identity);
            }
			hasActivated = true;
        }
    }
	
	private void OnDisable()
	{
		 hasActivated = false;
	}
}
