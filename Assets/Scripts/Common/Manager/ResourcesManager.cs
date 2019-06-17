﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CURSOR_TYPE
{
    NORMAL_CURSOR,
    DRY_TOWEL_CURSOR,
    HAMMER_CURSOR,
    LADDER_CURSOR,
    ROPE_CURSOR,
    FROST_PORK_CURSOR,
    CAT_FOOD_CURSOR,
    HEAT_PORK_CURSOR,
    KEY_CURSOR,
    FIRE_EXTINGUISHER_CURSOR
}

public class ResourcesManager
{
    // 道具与背包系统
    public Image emptyGridPrefab;
    public Image itemPrefab;

    // UI
    public Text floatMsgPrefab;
    public Image blackCurtainPrefab;

    // 庄鸿基相机模块使用
    public GameObject cameraRoomPrefab;

    // 图标
    public Dictionary<CURSOR_TYPE, Texture2D> cursorTextureList;

    private static ResourcesManager instance;

    private ResourcesManager()
    {

    }

    public static ResourcesManager getInstance()
    {
        if (instance == null)
            instance = new ResourcesManager();
        return instance;
    }

    public void Initialize()
    {
        emptyGridPrefab = Resources.Load<Image>("Prefabs/Inventory/EmptyGrid");
        itemPrefab = Resources.Load<Image>("Prefabs/Inventory/Item");

        floatMsgPrefab = Resources.Load<Text>("Prefabs/Common/FloatMsg");
        blackCurtainPrefab = Resources.Load<Image>("Prefabs/Common/BlackCurtain");

        cameraRoomPrefab = Resources.Load<GameObject>("Prefabs/Camera/Room");

        cursorTextureList = new Dictionary<CURSOR_TYPE, Texture2D>();
        cursorTextureList.Add(CURSOR_TYPE.NORMAL_CURSOR, Resources.Load<Texture2D>("Textures/NormalCursor"));
        cursorTextureList.Add(CURSOR_TYPE.DRY_TOWEL_CURSOR, Resources.Load<Texture2D>("Textures/DryTowelCursor"));
        cursorTextureList.Add(CURSOR_TYPE.HAMMER_CURSOR, Resources.Load<Texture2D>("Textures/HammerCursor"));
        cursorTextureList.Add(CURSOR_TYPE.LADDER_CURSOR, Resources.Load<Texture2D>("Textures/LadderCursor"));
        cursorTextureList.Add(CURSOR_TYPE.ROPE_CURSOR, Resources.Load<Texture2D>("Textures/RopeCursor"));
        cursorTextureList.Add(CURSOR_TYPE.FROST_PORK_CURSOR, Resources.Load<Texture2D>("Textures/FrostPorkCursor"));
        cursorTextureList.Add(CURSOR_TYPE.CAT_FOOD_CURSOR, Resources.Load<Texture2D>("Textures/CatFoodCursor"));
        cursorTextureList.Add(CURSOR_TYPE.HEAT_PORK_CURSOR, Resources.Load<Texture2D>("Textures/HeatPorkCursor"));
        cursorTextureList.Add(CURSOR_TYPE.KEY_CURSOR, Resources.Load<Texture2D>("Textures/KeyCursor"));
        cursorTextureList.Add(CURSOR_TYPE.FIRE_EXTINGUISHER_CURSOR, Resources.Load<Texture2D>("Textures/FireExtinguisherCursor"));
    }
}
