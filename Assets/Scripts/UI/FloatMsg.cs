using UnityEngine;
using UnityEngine.UI;

public class FloatMsg : MonoBehaviour
{
    private float floatTime;
    private float remainTime;
    private Vector3 moveStep;

    RectTransform rectTransform;

    void Start()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
            rectTransform.SetParent(GameObject.Find("Canvas").transform);
        }

        floatTime = 0.0f;
    }

    void Update()
    {
        if (floatTime < remainTime)
        {
            rectTransform.Translate(moveStep * Time.deltaTime);
            floatTime += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(string showText,float remainTime, Color color,Vector3 initialPos)
    {
        Text text = GetComponent<Text>();
        text.text = showText;
        text.color = color;

        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
            rectTransform.SetParent(GameObject.Find("Canvas").transform);
        }

        rectTransform.localPosition = initialPos;

        this.remainTime = remainTime;
        moveStep = new Vector3(0, 30, 0) / remainTime;
    }
}
