using UnityEngine;
using UnityEngine.UI;

public class MGUGUIUtility
{
    public static Rect RectTransformToSpaceRect(RectTransform rect){
        Rect spaceRect = rect.rect;
        Vector3 spacePos = rect.position;
        spaceRect.x = spaceRect.x * rect.lossyScale.x + spacePos.x;
        spaceRect.y = spaceRect.y * rect.lossyScale.y + spacePos.y;
        spaceRect.width = spaceRect.width * rect.lossyScale.x;
        spaceRect.height = spaceRect.height * rect.lossyScale.y;
        return spaceRect;
    }

    public class Toast
    {
        public const int REMAIN_LONG = 1;
        public const int REMAIN_SHORT = 2;
        // 慎用
        public const int REMAIN_FOREVER = 3;

        public const int TOP_MSG = 1;
        public const int MIDDLE_MSG = 2;
        public const int BOTTOM_MSG = 3;

        public static void showToast(string richText, int toastType, int toastPos)
        {
            GameObject oldToast = GameObject.Find("Canvas/FloatMsg(Clone)");
            if (oldToast != null)
            {
                GameObject.Destroy(oldToast);
            }

            Text text = GameObject.Instantiate<Text>(ResourcesManager.getInstance().floatMsgPrefab);

            float remainTime;
            Color textColor;
            Vector3 initialPos;

            switch (toastType)
            {
                case REMAIN_LONG:
                    remainTime = 4.0f;
                    textColor = new Color(255, 150, 0);
                    break;
                case REMAIN_SHORT:
                    remainTime = 2.0f;
                    textColor = new Color(255, 150, 0);
                    break;
                case REMAIN_FOREVER:
                    remainTime = 120.0f;
                    textColor = new Color(255, 150, 0);
                    break;
                default:
                    Debug.Log("Toast Type Error!");
                    return;
            }

            switch (toastPos)
            {
                case TOP_MSG:
                    initialPos = new Vector3(0, 140, 0);
                    break;
                case MIDDLE_MSG:
                    initialPos = new Vector3(0, 0, 0);
                    break;
                case BOTTOM_MSG:
                    initialPos = new Vector3(0, -140, 0);
                    break;
                default:
                    Debug.Log("Toast Position Error!");
                    return;
            }

            text.GetComponent<FloatMsg>().Initialize(richText, remainTime, textColor, initialPos);
        }
    }
}
