using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1CameraController : MonoBehaviour
{
    void Start()
    {
        // 创建视野Sphere

        GameObject playerView = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        playerView.name = "playerView";
        playerView.layer = LayerMask.NameToLayer("WarFog");
        playerView.transform.parent = GameObject.Find("Player").transform;
        playerView.transform.localScale = new Vector3(29, 29, 1);
        playerView.AddComponent<lightFollow>();

        // 创建一大堆黑色区域

        GameObject room = ResourcesManager.getInstance().cameraRoomPrefab;

        //cinema
        Vector3 position = new Vector3(0.7f, 0.55f, -5f);
        GameObject cinema = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        cinema.transform.localScale = new Vector3(14.65f, 6.5f, 1);

        //toilet
        position = new Vector3(14.1f, 0.55f, -5f);
        GameObject toilet = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        toilet.transform.localScale = new Vector3(8.2f, 6.5f, 1);

        //hall
        position = new Vector3(31.4f, 0.55f, -5f);
        GameObject hall = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        hall.transform.localScale = new Vector3(26.5f, 6.5f, 1);

        //dailyShop
        position = new Vector3(25.711f, -7.51f, -5f);
        GameObject dailyShop = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        dailyShop.transform.localScale = new Vector3(11.87f, 8.4f, 1);

        //smartPhoneShop
        position = new Vector3(13.679f, -7.51f, -5f);
        GameObject smartPhoneShop = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        smartPhoneShop.transform.localScale = new Vector3(12.175f, 8.4f, 1);

        //uniqlo
        position = new Vector3(0.465f, -7.51f, -5f);
        GameObject uniqlo = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        uniqlo.transform.localScale = new Vector3(14.20715f, 8.4f, 1);

        //mcdonald
        position = new Vector3(26.254f, -16.65f, -5f);
        GameObject mcdonald = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        mcdonald.transform.localScale = new Vector3(12.13241f, 8.4f, 1);

        //fruitShop
        position = new Vector3(14.446f, -16.65f, -5f);
        GameObject fruitShop = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        fruitShop.transform.localScale = new Vector3(11.44643f, 8.4f, 1);

        //pipe
        position = new Vector3(14.56f, 5.15f, -5f);
        GameObject pipe = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        pipe.transform.localScale = new Vector3(22.6f, 2.68f, 1);

        //secondStairs
        position = new Vector3(38.324f, -7.51f, -5f);
        GameObject secondStairs = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        secondStairs.transform.localScale = new Vector3(13.367f, 8.4f, 1);

        //firstStairs
        position = new Vector3(38.64f, -16.65f, -5f);
        GameObject firstStairs = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        firstStairs.transform.localScale = new Vector3(12.66395f, 8.4f, 1);

        //doorWay
        position = new Vector3(2.19f, -16.65f, -5f);
        GameObject doorWay = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        doorWay.transform.localScale = new Vector3(13.02824f, 8.4f, 1);

        //pipeWall
        position = new Vector3(19.264f, 12.25f, -5f);
        GameObject pipeWall = GameObject.Instantiate(room, position, Quaternion.identity) as GameObject;
        pipeWall.transform.localScale = new Vector3(53.6475f, 10.37778f, 1);

        // 子相机初始化

        Camera subCamera = GameObject.Find("Main Camera/SubCam").GetComponent<Camera>();
        subCamera.enabled = true;

        RenderTexture t = new RenderTexture(Screen.width, Screen.height, 16);
        subCamera.targetTexture = t;
        Shader.SetGlobalTexture("_LOSMaskTexture", t);
    }

    void Update()
    {
        
    }
}
