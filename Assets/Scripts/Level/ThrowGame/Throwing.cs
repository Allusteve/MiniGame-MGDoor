using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    private bool iscliked = false;
    public Transform Leftpos;
    public Transform Rightpos;
    public float MaxDistance = 3;
    private SpringJoint2D m_Springjoint;
    private Rigidbody2D m_rigiBody;
    private bool isGround = false;

    public LineRenderer LeftLine;
    public LineRenderer RightLine;

    private void OnMouseDown()
    {
        iscliked = true;
        m_rigiBody.isKinematic = true;
        m_Springjoint.enabled = true;
        m_rigiBody.constraints = ~RigidbodyConstraints2D.FreezePosition;
    }


    private void OnMouseUp()
    {
        iscliked = false;
        m_rigiBody.isKinematic = false;
        Invoke("Fly", 0.1f);
        LeftLine.enabled = false;
        RightLine.enabled = false;
    }



    private void Awake()
    {
        transform.position = GameObject.Find("StartPoint").transform.position;
        m_Springjoint = GetComponent<SpringJoint2D>();
        m_rigiBody = GetComponent<Rigidbody2D>();
        m_rigiBody.constraints = RigidbodyConstraints2D.FreezePosition;
        LeftLine.startWidth = 0.20f;
        LeftLine.endWidth = 0.20f;

        RightLine.startWidth = 0.20f;
        RightLine.endWidth = 0.20f;

        m_Springjoint.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(iscliked)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);

            //绳子位置限定
            if(Vector3.Distance(transform.position, Leftpos.position)>MaxDistance)
            {
                Vector3 pos = (transform.position - Leftpos.position).normalized;
                pos *= MaxDistance;
                transform.position = pos + Leftpos.position;
            }

            DrawLine();

        }

       
        
    }

    void FixedUpdate()
    {
        
    }

    void Fly()
    {
        m_Springjoint.enabled = false;
    }

    void DrawLine()
    {
        LeftLine.enabled = true;
        RightLine.enabled = true;

        RightLine.SetPosition(0, Rightpos.position);
        RightLine.SetPosition(1, transform.position);

        LeftLine.SetPosition(0, Leftpos.position);
        LeftLine.SetPosition(1, transform.position);
    }

    public void OnSuccess()
    {
        Invoke("Success", 1f);
    }

    public void OnRestart()
    {
        Invoke("Restart", 1f);
    }

    private void Success()
    {
        Debug.Log("Success");

    }

    private void Restart()
    {
        Debug.Log("Restart");
        m_rigiBody.constraints = RigidbodyConstraints2D.FreezePosition;
        transform.position= GameObject.Find("StartPoint").transform.position;

    }
}
