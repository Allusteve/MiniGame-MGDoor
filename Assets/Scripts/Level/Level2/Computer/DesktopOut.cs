using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesktopOut : MonoBehaviour
{
    CanvasGroup canvasGroup1;
    CanvasGroup canvasGroup2;
    CanvasGroup canvasGroup3;
    CanvasGroup canvasGroup4;


    //界面1
    public Image dark;

    //界面2
    
    public Image turnOn;
    public Text Name;
    public Text mima;


    //界面3
    public Button OpenDocument;
    public Image desktop;
    public Image wenjianneibu;
    public Image rijijiemian;

    //界面4
    public Image webMini;

    Level2Controller level2Controller;


    void Start()
    {
        canvasGroup1 = turnOn.GetComponentInChildren<CanvasGroup>();
        canvasGroup2 = desktop.GetComponentInChildren<CanvasGroup>();
        canvasGroup3 = webMini.GetComponentInChildren<CanvasGroup>();
        canvasGroup4 = rijijiemian.GetComponentInChildren<CanvasGroup>();

        level2Controller = GameObject.Find("Manager").GetComponent<Level2Controller>();
    }

    public void Kaiji()
    {
         dark.enabled = false;
    }

    public void login()
    {
        if(mima.text.Equals(level2Controller.password))
        {
            canvasGroup1.alpha = 0;
            canvasGroup1.interactable = false;
            canvasGroup1.blocksRaycasts = false;  
        }

    }

    public void openDocument()
    {
        canvasGroup2.alpha = 0;
        canvasGroup2.interactable = false;
        canvasGroup2.blocksRaycasts = false;
    }

    public void close()
    {
        canvasGroup2.alpha = 1;
        canvasGroup2.interactable = true;
        canvasGroup2.blocksRaycasts = true;
    }

    public void webOpen()
    {
        canvasGroup3.alpha = 1;
        canvasGroup3.interactable = true;
        canvasGroup3.blocksRaycasts = true;
    }

    public void webClose()
    {
        canvasGroup3.alpha = 0;
        canvasGroup3.interactable = false;
        canvasGroup3.blocksRaycasts = false;
    }

    public void DiaryOpen()
    {
        GameObject.Find("Manager").GetComponent<Level2Controller>().knowTheKey = true;
        canvasGroup4.alpha=1;
        canvasGroup4.interactable=true;
        canvasGroup4.blocksRaycasts=true;
    }

    public void DiaryClose()
    {
        canvasGroup4.alpha=0;
        canvasGroup4.interactable=false;
        canvasGroup4.blocksRaycasts=false;
    }
}
