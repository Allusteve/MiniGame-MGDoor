using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 主菜单UI控制
public class MainMenu: MonoBehaviour
{
    CanvasGroup masks;
    CanvasGroup settings;
    CanvasGroup levels;
    CanvasGroup loading;

    

    public Image theLevel;
    public Image theSet;
    public Image zhezhao;
    public Text Loading;

   

    private int upORdown1 = 0;
    private int upORdown2 = 0;

    void Start()
    {
        masks = zhezhao.GetComponentInChildren<CanvasGroup>();
        settings = theSet.GetComponentInChildren<CanvasGroup>();
        levels = theLevel.GetComponentInChildren<CanvasGroup>();
        loading=Loading.GetComponentInChildren<CanvasGroup>();

        //guan1=Guan1.GetComponentInChildren<CanvasGroup>();
        //guan2 = Guan2.GetComponentInChildren<CanvasGroup>();
    }

    public void GuanqiaClick()
    {

        if (upORdown1 == 0)
        {
            //theLevel.rectTransform.Translate(Vector3.up * 450f, Space.Self);
            levels.alpha = 1;
            levels.blocksRaycasts = true;
            levels.interactable = true;
            upORdown1 = 1;
        }
        //关卡选择界面出现
    }

    public void GuanqiaClose()
    {
        //theLevel.rectTransform.Translate(Vector3.down * 450f, Space.Self);
        levels.alpha = 0;
        levels.blocksRaycasts = false;
        levels.interactable = false;
        upORdown1 = 0;
        //关卡选择界面消失
    }

    public void gotoGame()
    {
        
        SceneManager.LoadScene("Level_1");//要切换到的场景名

        takeLoading();


        //进入游戏
    }

    public void gotoGame2()
    {
        
        SceneManager.LoadScene("Level_2");//要切换到的场景名
        takeLoading();

        //进入游戏
    }

    public void SetOnClickUP()
    {

        if (upORdown1 == 0 )
        {
            //theSet.rectTransform.Translate(Vector3.up * 450f, Space.Self);
            settings.alpha = 1;
            settings.blocksRaycasts = true;
            settings.interactable = true;
            upORdown2 = 1;
        }
        //设置界面出现
    }

    public void Setclose()
    {
        //theSet.rectTransform.Translate(Vector3.down * 450f, Space.Self);
        settings.alpha = 0;
        settings.blocksRaycasts = false;
        settings.interactable = false;
        upORdown2 = 0;
        //设置界面消失
    }

    public void zhezhaoControl()
    {
        if(upORdown1==1||upORdown2==1)
        {
            masks.alpha = 1;
            masks.blocksRaycasts = true;
            masks.interactable = true;
        }
        else
        {
            masks.alpha = 0;
            masks.blocksRaycasts = false;
            masks.interactable = false;
            upORdown2 = 0;
        }
    }

    public void takeLoading()
    {
        loading.alpha = 1;
        loading.blocksRaycasts = false;
        loading.interactable = false;
    }

    public void exitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
