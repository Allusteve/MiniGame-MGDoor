
public class MessageEvent : MGEvent
{
    private string MessageText;

    public static void ShowMessage(MGEvent mgEvent)
    {
        MessageEvent msgEvent = (MessageEvent)mgEvent;
        MGUGUIUtility.Toast.showToast(msgEvent.MessageText, MGUGUIUtility.Toast.REMAIN_LONG, MGUGUIUtility.Toast.TOP_MSG);
    }

    public MessageEvent(string msgText, float startTime, float remainTime, int loopNum)
        : base(startTime, remainTime, loopNum, ShowMessage, null)
    {
        MessageText = msgText;
    }
}
