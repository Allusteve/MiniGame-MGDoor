
public enum MG_EVENT_ID
{
    // 物品获取事件
    GET_ITEM,
    // 播放消息事件
    SHOW_MESSAGE,
    // 关卡1通关-砸门事件
    LEVEL_1_BREAKING_DOOR,
    // 物体出现事件
    OBJECT_APEAR,
    // 关卡1用绳子下1楼事件
    LEVEL_1_DOWN,
    // 关卡1爬上通风管道事件
    LEVEL_1_CLIMB_TO_PIPE,
    // 关卡2上下楼事件
    LEVEL_2_UPSTAIR,
    LEVEL_2_DOWNSTAIR,
    LEVEL_2_UP_ATTIC,
    LEVEL_2_DOWN_ATTIC,
    // 关卡2喂狗事件
    LEVEL_2_FEED_DOG,
    // 关卡2开电脑事件
    LEVEL_2_COMPUTER,
    // 关卡2通关-开门事件
    LEVEL_2_OPEN_THE_DOOR,
    // 关卡2接电线事件
    LEVEL_2_CONNECT_ELECTRIC
}

public class MGEvent
{
    public float startTime;
    public float remainTime;
    public int loopNum;

    public bool isExecuted;

    public delegate void RespondCallback(MGEvent mgEvent);

    private RespondCallback startedCallback;
    private RespondCallback finishedCallback;

    public MGEvent(float startTime,float remainTime,int loopNum,RespondCallback startedCallback,RespondCallback finishedCallback)
    {
        this.startTime = startTime;
        this.remainTime = remainTime;
        this.loopNum = loopNum;
        this.startedCallback = startedCallback;
        this.finishedCallback = finishedCallback;
        isExecuted = false;
    }

    public void Start()
    {
        startedCallback?.Invoke(this);
    }

    public void Finish()
    {
        finishedCallback?.Invoke(this);
    }
}
