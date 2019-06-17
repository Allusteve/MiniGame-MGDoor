using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 关卡通用UI控制器
public class UIControllder : MonoBehaviour
{
    private Image pausePage;
    private Image settingPage;
    private Image mask;
    private LevelController levelController;


    CanvasGroup cgPausePage;
    CanvasGroup cgSettingPage;
    CanvasGroup cgMask;

    void Start()
    {
        pausePage = GameObject.Find("Canvas/PausePage").GetComponent<Image>();
        settingPage = GameObject.Find("Canvas/SettingPage").GetComponent<Image>();
        mask = GameObject.Find("Canvas/Mask").GetComponent<Image>();
        levelController = GameObject.Find("Manager").GetComponent<LevelController>();
        

        cgPausePage = pausePage.GetComponentInChildren<CanvasGroup>();
        cgSettingPage = settingPage.GetComponentInChildren<CanvasGroup>();
        cgMask = mask.GetComponentInChildren<CanvasGroup>();
        
    }

    public void ShowPausePage()
    {
        if (levelController.cursorType == CURSOR_TYPE.NORMAL_CURSOR)
        {
            levelController.Pause();

            cgMask.alpha = 1;
            cgMask.interactable = true;
            cgMask.blocksRaycasts = true;

            cgPausePage.alpha = 1;
            cgPausePage.interactable = true;
            cgPausePage.blocksRaycasts = true;

        }
    }

    public void HidePausePage()
    {
        levelController.Continue();
        //pausePage.rectTransform.Translate(Vector3.down * 450f, Space.Self);
        //mask.rectTransform.Translate(Vector3.up * 600f, Space.Self);

        cgPausePage.alpha = 0;
        cgPausePage.interactable = false;
        cgPausePage.blocksRaycasts = false;

        cgMask.alpha = 0;
        cgMask.interactable = false;
        cgMask.blocksRaycasts = false;

    }

    public void ShowSettingPage()
    {
        //pausePage.rectTransform.Translate(Vector3.down * 450f, Space.Self);
        //settingPage.rectTransform.Translate(Vector3.left * 850f, Space.Self);

        cgPausePage.alpha = 0;
        cgPausePage.interactable = false;
        cgPausePage.blocksRaycasts = false;

        cgSettingPage.alpha = 1;
        cgSettingPage.interactable = true;
        cgSettingPage.blocksRaycasts = true;
    }

    public void HideSettingPage()
    {
        cgSettingPage.alpha = 0;
        cgSettingPage.interactable = false;
        cgSettingPage.blocksRaycasts = false;

        cgPausePage.alpha = 1;
        cgPausePage.interactable = true;
        cgPausePage.blocksRaycasts = true;
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(levelController.CurrentLevel);
    }

    public void ShowGameOverPage()
    {
        levelController.Pause();

        cgMask.alpha = 1;
        cgMask.interactable = true;
        cgMask.blocksRaycasts = true;

        pausePage.transform.Find("ContinueBtnImage").gameObject.SetActive(false);
        pausePage.transform.Find("Text").gameObject.SetActive(false);

        cgPausePage.alpha = 1;
        cgPausePage.interactable = true;
        cgPausePage.blocksRaycasts = true;
    }

    public void showTishi()
    {
        string tip = "AD键左右移动，空格键跳跃，Ctrl键下蹲，鼠标点击放大镜调查";
        MGUGUIUtility.Toast.showToast(tip, MGUGUIUtility.Toast.REMAIN_LONG,MGUGUIUtility.Toast.TOP_MSG);
    }
}
