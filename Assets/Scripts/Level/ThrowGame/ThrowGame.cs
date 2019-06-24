using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrowGame : MonoBehaviour
{
    // Start is called before the first frame update

    private bool m_Success;
    private bool m_Ground;

    public UnityEvent OnSuccess;
    public UnityEvent OnRestart;

    void Awake()
    {
        m_Success = false;
        m_Ground = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        m_Success = true;
        OnSuccess.Invoke();
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!m_Success)
            OnRestart.Invoke();

    }

    // Update is called once per frame
  

    public bool isSuccess()
    {
        return m_Success;
    }

    public bool isGround()
    {
        return m_Ground;
    }
}
