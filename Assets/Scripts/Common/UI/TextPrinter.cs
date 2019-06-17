using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextPrinter : MonoBehaviour,IPointerClickHandler
{
    private Text uiText;
    //储存中间值
    public string words;
    //每个字符的显示速度
    private float timer;
    //限制条件，是否可以进行文本的输出
    private bool isPrint = false;
    private float perCharSpeed = 6f;

    private int wordCount = 0;

    private bool isSkipping;

    public string ClipName;
    public float ClipTime;

    void Start()
    {
        isSkipping = false;

        uiText = transform.Find("Text").GetComponent<Text>();

        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if (!isSkipping)
        {
            wordCount = (int)(perCharSpeed * timer);
            if (wordCount > words.Length)
            {
                wordCount = words.Length;
                GetComponent<AudioSource>().Stop();
            }

            uiText.text = words.Substring(0, wordCount);
            timer += Time.deltaTime;
        }
    }

    public void destroy()
    {
        MusicEvent bgmEvent = new MusicEvent(ClipName, 0.08f, MGEventManager.getInstance().currTime, ClipTime, -1);
        MGEventManager.getInstance().AddEvent(bgmEvent);

        Destroy(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isSkipping)
        {
            isSkipping = true;

            GetComponent<AudioSource>().Stop();

            // 出现全部文字
            uiText.text = words;
        }
        else
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().OnLocking();
            this.destroy();
        }
    }

    private void OnMouseDown()
    {
        
    }
}