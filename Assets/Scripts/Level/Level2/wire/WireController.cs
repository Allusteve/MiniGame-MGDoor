using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WireController : MonoBehaviour
{
    bool flow;
    float remainTime = 0.0f;

    const float disapearTime = 1.0f;

    Sprite sprite;

    PlayerMovement playerMovement;

    void Start()
    {
        Transform ts = GameObject.Find("Canvas/PowerFlow").GetComponent<Image>().transform;
        Debug.Log(ts.Find("heng").GetComponent<RectTransform>().position + "AA");
        Debug.Log(ts.Find("tu").GetComponent<RectTransform>().position + "BB");
        Debug.Log(GameObject.Find("Canvas/PowerFlow").GetComponent<RectTransform>().position + "CC");
        flow = false;
        sprite = Resources.Load<Sprite>("Images/Wire/success");
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        SetWire.InitWire();
    }

    void Update()
    {
        if (!flow&& SetWire.FlowSuccess())
        {
            flow = true;
            transform.Find("WireState").GetComponent<Image>().sprite = sprite;
        }
        else if (flow)
        {
            remainTime += Time.deltaTime;
            if (remainTime >= disapearTime)
            {
                DestroyOwn();
            }
        }
    }

    void DestroyOwn()
    {
        Level2Controller level2Controller = GameObject.Find("Manager").GetComponent<Level2Controller>();
        level2Controller.electricOn = true;
        level2Controller.isPlayingWireGame = false;

        playerMovement.OnLocking();
        Destroy(gameObject);
    }
}
