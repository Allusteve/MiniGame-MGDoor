using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetWire : MonoBehaviour
{
    private static Transform WirePage;
    static Vector3 firstP;
    static Vector3 secondP;
    static float side;
    private static Vector3[] gridCoordinate = new Vector3[]
{
        new Vector3(firstP.x, firstP.y, firstP.z),        new Vector3(firstP.x + side , firstP.y, firstP.z),
        new Vector3(firstP.x + side * 2 , firstP.y, firstP.z),        new Vector3(firstP.x + side * 3 , firstP.y, firstP.z),

        new Vector3(firstP.x, firstP.y - side , firstP.z),        new Vector3(firstP.x + side , firstP.y - side , firstP.z),
        new Vector3(firstP.x + side * 2 , firstP.y - side , firstP.z),        new Vector3(firstP.x + side * 3 , firstP.y - side , firstP.z),

        new Vector3(firstP.x, firstP.y - side * 2 , firstP.z),        new Vector3(firstP.x + side , firstP.y - side * 2 , firstP.z),
        new Vector3(firstP.x + side * 2 , firstP.y - side * 2 , firstP.z),        new Vector3(firstP.x + side * 3 , firstP.y - side * 2 , firstP.z),

        new Vector3(firstP.x, firstP.y - side * 3 , firstP.z),        new Vector3(firstP.x + side , firstP.y - side * 3 , firstP.z),
        new Vector3(firstP.x + side * 2 , firstP.y - side * 3 , firstP.z),        new Vector3(firstP.x + side * 3 , firstP.y - side * 3 , firstP.z),  
};

    private static bool[] isSet = new bool[]
    {
            true, false, false, false,
            true, true, true, true,
            false, true, false, true,
            false, false, true, false
    };

    public static int NearestCoodIndex(Vector3 currentP)
    {
        float distance = 1000;
        int nearestIndex = 0;
        for (int i = 0; i < 16; i++)
        {
            if (Vector3.Distance(currentP, gridCoordinate[i]) < distance)
            {
                distance = Vector3.Distance(currentP, gridCoordinate[i]);
                nearestIndex = i;
            }
        }
        return nearestIndex;
    }



    public static Vector3 NearestCood(Vector3 currentP)
    {
        float distance = 1000;
        int nearestIndex = 0;
        for (int i = 0; i < 16; i++)
        {
            if (Vector3.Distance(currentP, gridCoordinate[i]) < distance)
            {
                if (!isSet[i])
                {
                    distance = Vector3.Distance(currentP, gridCoordinate[i]);
                    nearestIndex = i;
                }
            }
        }
        return gridCoordinate[nearestIndex];
    }

    public static void IsSet( Vector3 afterCorrect)
    {
        isSet[NearestCoodIndex(afterCorrect)] = true;
    }

    public static void IsNotSet(Vector3 beforeCorrect)
    {
        isSet[NearestCoodIndex(beforeCorrect)] = false;
    }

    public static bool FlowSuccess()
    {
        if (ExistWire())
        {
            if (WirePage.Find("tu").GetComponent<RectTransform>().position == gridCoordinate[0] && WirePage.Find("youshang").GetComponent<RectTransform>().position == gridCoordinate[9]
               && WirePage.Find("heng").GetComponent<RectTransform>().position == gridCoordinate[10] && WirePage.Find("zuoshang").GetComponent<RectTransform>().position == gridCoordinate[11]
                && WirePage.Find("shuyou").GetComponent<RectTransform>().position == gridCoordinate[7])
            {
                if ((WirePage.Find("shuzuo").GetComponent<RectTransform>().position == gridCoordinate[1] && WirePage.Find("shuzuo2").GetComponent<RectTransform>().position == gridCoordinate[5])
                    || (WirePage.Find("shuzuo").GetComponent<RectTransform>().position == gridCoordinate[5] && WirePage.Find("shuzuo2").GetComponent<RectTransform>().position == gridCoordinate[1]))
                { return true; }
            }
            if (WirePage.Find("tu").GetComponent<RectTransform>().position == gridCoordinate[4] && WirePage.Find("youshang").GetComponent<RectTransform>().position == gridCoordinate[9]
               && WirePage.Find("heng").GetComponent<RectTransform>().position == gridCoordinate[10] && WirePage.Find("zuoshang").GetComponent<RectTransform>().position == gridCoordinate[11]
                && WirePage.Find("shuyou").GetComponent<RectTransform>().position == gridCoordinate[7])
            {
                if ((WirePage.Find("shuzuo").GetComponent<RectTransform>().position == gridCoordinate[0] && WirePage.Find("shuzuo2").GetComponent<RectTransform>().position == gridCoordinate[5])
                    || (WirePage.Find("shuzuo").GetComponent<RectTransform>().position == gridCoordinate[5] && WirePage.Find("shuzuo2").GetComponent<RectTransform>().position == gridCoordinate[0]))
                { return true; }
            }

            if (WirePage.Find("tu").GetComponent<RectTransform>().position == gridCoordinate[9] && WirePage.Find("youshang").GetComponent<RectTransform>().position == gridCoordinate[4]
                && WirePage.Find("heng").GetComponent<RectTransform>().position == gridCoordinate[10] && WirePage.Find("zuoshang").GetComponent<RectTransform>().position == gridCoordinate[11]
                 && WirePage.Find("shuyou").GetComponent<RectTransform>().position == gridCoordinate[7])
            {
                if ((WirePage.Find("shuzuo").GetComponent<RectTransform>().position == gridCoordinate[0] && WirePage.Find("shuzuo2").GetComponent<RectTransform>().position == gridCoordinate[5])
                    || (WirePage.Find("shuzuo").GetComponent<RectTransform>().position == gridCoordinate[5] && WirePage.Find("shuzuo2").GetComponent<RectTransform>().position == gridCoordinate[0]))
                { return true; }
            }

            if (WirePage.Find("tu").GetComponent<RectTransform>().position == gridCoordinate[10] && WirePage.Find("youshang").GetComponent<RectTransform>().position == gridCoordinate[8]
               && WirePage.Find("heng").GetComponent<RectTransform>().position == gridCoordinate[9] && WirePage.Find("zuoshang").GetComponent<RectTransform>().position == gridCoordinate[11]
                && WirePage.Find("shuyou").GetComponent<RectTransform>().position == gridCoordinate[7])
            {
                if ((WirePage.Find("shuzuo").GetComponent<RectTransform>().position == gridCoordinate[0] && WirePage.Find("shuzuo2").GetComponent<RectTransform>().position == gridCoordinate[4])
                    || (WirePage.Find("shuzuo").GetComponent<RectTransform>().position == gridCoordinate[4] && WirePage.Find("shuzuo2").GetComponent<RectTransform>().position == gridCoordinate[0]))
                { return true; }
            }

            if (WirePage.Find("tu").GetComponent<RectTransform>().position == gridCoordinate[9] && WirePage.Find("youshang").GetComponent<RectTransform>().position == gridCoordinate[8]
               && WirePage.Find("heng").GetComponent<RectTransform>().position == gridCoordinate[10] && WirePage.Find("zuoshang").GetComponent<RectTransform>().position == gridCoordinate[11]
                && WirePage.Find("shuyou").GetComponent<RectTransform>().position == gridCoordinate[7])
            {
                if ((WirePage.Find("shuzuo").GetComponent<RectTransform>().position == gridCoordinate[0] && WirePage.Find("shuzuo2").GetComponent<RectTransform>().position == gridCoordinate[4])
                    || (WirePage.Find("shuzuo").GetComponent<RectTransform>().position == gridCoordinate[4] && WirePage.Find("shuzuo2").GetComponent<RectTransform>().position == gridCoordinate[0]))
                { return true; }
            }

            if (WirePage.Find("tu").GetComponent<RectTransform>().position == gridCoordinate[10] && WirePage.Find("youshang").GetComponent<RectTransform>().position == gridCoordinate[9]
                && WirePage.Find("heng").GetComponent<RectTransform>().position == gridCoordinate[0] && WirePage.Find("zuoshang").GetComponent<RectTransform>().position == gridCoordinate[11]
                && WirePage.Find("shuyou").GetComponent<RectTransform>().position == gridCoordinate[7])
            {
                if ((WirePage.Find("shuzuo").GetComponent<RectTransform>().position == gridCoordinate[1] && WirePage.Find("shuzuo2").GetComponent<RectTransform>().position == gridCoordinate[5])
                    || (WirePage.Find("shuzuo").GetComponent<RectTransform>().position == gridCoordinate[5] && WirePage.Find("shuzuo2").GetComponent<RectTransform>().position == gridCoordinate[1]))
                { return true; }
            }

            if (WirePage.Find("tu").GetComponent<RectTransform>().position == gridCoordinate[7] && WirePage.Find("youshang").GetComponent<RectTransform>().position == gridCoordinate[4]
                && WirePage.Find("heng").GetComponent<RectTransform>().position == gridCoordinate[2] && WirePage.Find("zuoshang").GetComponent<RectTransform>().position == gridCoordinate[5]
                && WirePage.Find("shuyou").GetComponent<RectTransform>().position == gridCoordinate[1])
            {
                if ((WirePage.Find("shuzuo").GetComponent<RectTransform>().position == gridCoordinate[0] && WirePage.Find("shuzuo2").GetComponent<RectTransform>().position == gridCoordinate[3])
                    || (WirePage.Find("shuzuo").GetComponent<RectTransform>().position == gridCoordinate[3] && WirePage.Find("shuzuo2").GetComponent<RectTransform>().position == gridCoordinate[0]))
                { return true; }
            }

            if (WirePage.Find("tu").GetComponent<RectTransform>().position == gridCoordinate[4] && WirePage.Find("youshang").GetComponent<RectTransform>().position == gridCoordinate[7]
                && WirePage.Find("heng").GetComponent<RectTransform>().position == gridCoordinate[2] && WirePage.Find("zuoshang").GetComponent<RectTransform>().position == gridCoordinate[5]
                && WirePage.Find("shuyou").GetComponent<RectTransform>().position == gridCoordinate[1])
            {
                if ((WirePage.Find("shuzuo").GetComponent<RectTransform>().position == gridCoordinate[0] && WirePage.Find("shuzuo2").GetComponent<RectTransform>().position == gridCoordinate[3])
                    || (WirePage.Find("shuzuo").GetComponent<RectTransform>().position == gridCoordinate[3] && WirePage.Find("shuzuo2").GetComponent<RectTransform>().position == gridCoordinate[0]))
                { return true; }
            }
        }
        return false;
    }

    public static void InitWire()
    {
        WirePage = GameObject.Find("Canvas/PowerFlow").GetComponent<Image>().transform;
        firstP = WirePage.Find("heng").GetComponent<RectTransform>().position;
        secondP = WirePage.Find("tu").GetComponent<RectTransform>().position;
        side = secondP.x - firstP.x;
        isSet = new bool[]
        {
            true, false, false, false,
            true, true, true, true,
            false, true, false, true,
            false, false, true, false
        };
        gridCoordinate = new Vector3[]
{
        new Vector3(firstP.x, firstP.y, firstP.z),        new Vector3(firstP.x + side , firstP.y, firstP.z),
        new Vector3(firstP.x + side * 2 , firstP.y, firstP.z),        new Vector3(firstP.x + side * 3 , firstP.y, firstP.z),

        new Vector3(firstP.x, firstP.y - side , firstP.z),        new Vector3(firstP.x + side , firstP.y - side , firstP.z),
        new Vector3(firstP.x + side * 2 , firstP.y - side , firstP.z),        new Vector3(firstP.x + side * 3 , firstP.y - side , firstP.z),

        new Vector3(firstP.x, firstP.y - side * 2 , firstP.z),        new Vector3(firstP.x + side , firstP.y - side * 2 , firstP.z),
        new Vector3(firstP.x + side * 2 , firstP.y - side * 2 , firstP.z),        new Vector3(firstP.x + side * 3 , firstP.y - side * 2 , firstP.z),

        new Vector3(firstP.x, firstP.y - side * 3 , firstP.z),        new Vector3(firstP.x + side , firstP.y - side * 3 , firstP.z),
        new Vector3(firstP.x + side * 2 , firstP.y - side * 3 , firstP.z),        new Vector3(firstP.x + side * 3 , firstP.y - side * 3 , firstP.z),
};
        WirePage.Find("tu").GetComponent<RectTransform>().position = gridCoordinate[9];
        WirePage.Find("heng").GetComponent<RectTransform>().position = gridCoordinate[0];
        WirePage.Find("zuoshang").GetComponent<RectTransform>().position = gridCoordinate[4];
        WirePage.Find("shuyou").GetComponent<RectTransform>().position = gridCoordinate[5];
        WirePage.Find("youshang").GetComponent<RectTransform>().position = gridCoordinate[7];
        WirePage.Find("shuzuo").GetComponent<RectTransform>().position = gridCoordinate[11];
        WirePage.Find("shuzuo2").GetComponent<RectTransform>().position = gridCoordinate[14];
        WirePage.Find("WireState").GetComponent<RectTransform>().position = gridCoordinate[6];
    }

    public static void ResetWire()
    {
        GameObject powerFlow = GameObject.Find("Canvas/PowerFlow");

        //powerFlow.GetComponent<RectTransform>().position = new Vector3(861.6f, 384.6f, 0.0f);

        CanvasGroup canvasGroup = powerFlow.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        InitWire();
    }

    public static bool ExistWire()
    {
        return (!(GameObject.Find("Canvas/PowerFlow") == null));
    }
}
