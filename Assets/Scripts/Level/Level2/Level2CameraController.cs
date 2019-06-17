using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2CameraController : MonoBehaviour
{
    void Start()
    {
        // 创建视野Sphere

        GameObject playerView = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        playerView.name = "playerView";
        playerView.layer = LayerMask.NameToLayer("WarFog");
        playerView.transform.parent = GameObject.Find("Player").transform;
        playerView.transform.localScale = new Vector3(28, 28, 1);
        playerView.AddComponent<lightFollow>();

        // 创建一大堆黑色区域

        GameObject room = ResourcesManager.getInstance().cameraRoomPrefab;

        //guestBedroon
        Vector3 position = new Vector3(14.6f, -2.665f, -5f);
        GameObject guestBedroon = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        guestBedroon.transform.localScale = new Vector3(8.512f, 9.31f, 1);

        //balcony
        position = new Vector3(21.674f, -2.665f, -5f);
        GameObject balcony = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        balcony.transform.localScale = new Vector3(5.623f, 9.31f, 1);

        //compartment
        position = new Vector3(9.162f, -2.642f, -5f);
        GameObject compartment = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        compartment.transform.localScale = new Vector3(2.416f, 9.357f, 1);

        //masterBedroom
        position = new Vector3(0.195f, -2.628f, -5f);
        GameObject masterBedroom = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        masterBedroom.transform.localScale = new Vector3(15.522f, 9.385f, 1);

        //storage
        position = new Vector3(-12.038f, -2.628f, -5f);
        GameObject storage = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        storage.transform.localScale = new Vector3(8.9515f, 9.385f, 1);

        //staircase
        position = new Vector3(11.621f, -12.641f, -5f);
        GameObject staircase = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        staircase.transform.localScale = new Vector3(7.3742f, 10.11f, 1);

        //livingRoom
        position = new Vector3(-1.087f, -12.595f, -5f);
        GameObject livingRoom = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        livingRoom.transform.localScale = new Vector3(17.92319f, 9.967667f, 1);

        //kitchen
        position = new Vector3(19.882f, -12.587f, -5f);
        GameObject kitchen = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        kitchen.transform.localScale = new Vector3(9.066195f, 9.951889f, 1);

        //corridor
        position = new Vector3(-13.304f, -12.623f, -5f);
        GameObject corridor = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        corridor.transform.localScale = new Vector3(6.501027f, 9.975888f, 1);

        //patio
        position = new Vector3(-20.808f, -7.678f, -5f);
        GameObject patio = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        patio.transform.localScale = new Vector3(8.504564f, 19.83416f, 1);

        //attic
        position = new Vector3(5.375f, 4.515f, -5f);
        GameObject attic = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        attic.transform.localScale = new Vector3(24.13971f, 4.5012f, 1);

        // 子相机初始化

        Camera subCamera = GameObject.Find("Main Camera/SubCam").GetComponent<Camera>();
        subCamera.enabled = true;

        RenderTexture t = new RenderTexture(Screen.width, Screen.height, 16);
        subCamera.targetTexture = t;
        Shader.SetGlobalTexture("_LOSMaskTexture", t);
    }

}
