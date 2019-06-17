using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginTextPrintEvent : MGEvent
{
    private string PrintText;
    private Image blackCurtain;

    private string ClipName;
    private float ClipTime;

    public static void ShowPrinter(MGEvent mgEvent)
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().OnLocking();

        BeginTextPrintEvent printEvent = (BeginTextPrintEvent)mgEvent;
        printEvent.blackCurtain = GameObject.Instantiate<Image>(ResourcesManager.getInstance().blackCurtainPrefab,
            GameObject.Find("Canvas").transform);

        TextPrinter printer = printEvent.blackCurtain.GetComponent<TextPrinter>();
        printer.words = printEvent.PrintText;
        printer.ClipName = printEvent.ClipName;
        printer.ClipTime = printEvent.ClipTime;
    }

    public static void DeletePrinter(MGEvent mgEvent)
    {
        BeginTextPrintEvent printEvent = (BeginTextPrintEvent)mgEvent;

        if (printEvent.blackCurtain!=null)
        {
            printEvent.blackCurtain.GetComponent<TextPrinter>().destroy();
            GameObject.Find("Player").GetComponent<PlayerMovement>().OnLocking();
        }
    }

    public BeginTextPrintEvent(string printText,float playTime,string clipName,float clipTime)
        : base(MGEventManager.getInstance().currTime, playTime, 1, ShowPrinter, DeletePrinter)
    {
        PrintText = printText;
        ClipName = clipName;
        ClipTime = clipTime;
    }
}
