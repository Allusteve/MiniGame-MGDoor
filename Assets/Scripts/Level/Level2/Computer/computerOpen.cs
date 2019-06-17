using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class computerOpen : MonoBehaviour
{

    CanvasGroup diannao;
    public Image Computer;
  //  private int aa;
    void Start()
    {
        diannao = Computer.GetComponentInChildren<CanvasGroup>();
  //      aa = GameObject.Find("GameObject").GetComponent<NearTrigger>().aaa;
        
    }

    void Update()
    {
        if (GameObject.Find("Manager").GetComponent<Level2Controller>().computerOpen)
        { diannao.alpha = 1;
            diannao.blocksRaycasts = true;
            diannao.interactable = true;
        }

        if(GameObject.Find("Manager").GetComponent<Level2Controller>().computerOpen==false)
        {
            diannao.alpha = 0;
            diannao.blocksRaycasts = false;
            diannao.interactable = false;
        }
    }

    public void closeComputer()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().OnLocking();
        GameObject.Find("Manager").GetComponent<Level2Controller>().computerOpen = false;
        
    }
}
