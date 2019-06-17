using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WetTowelController : MonoBehaviour
{
    private GameObject blueBarObject;
    private Slider blueBarSlider;

    private LevelController levelController;
    private Level1Controller level1Controller;

    private float MaxValue = 100.0f;
    private float ChangeSpeed = 2.0f;

    private float helpBloodValue = 0.2f;

    void Start()
    {
        levelController = GameObject.Find("Manager").GetComponent<LevelController>();
        level1Controller = GameObject.Find("Manager").GetComponent<Level1Controller>();

        level1Controller.PlayerSwitchWetTowel();
        blueBarObject = GameObject.Find("Canvas/PlayerInfo/BlueBar");
        blueBarObject.SetActive(true);
        blueBarSlider = blueBarObject.GetComponent<Slider>();

        blueBarSlider.maxValue = MaxValue;
        blueBarSlider.value = MaxValue;
    }

    void Update()
    {
        if (blueBarSlider.value <= 0.0f)
        {
            GameObject.Find("Manager").GetComponent<Level1Controller>().PlayerSwitchWetTowel();
            blueBarObject.SetActive(false);

            GameObject.Find("Manager").GetComponent<Inventory>().AddItem(ITEM_ID.DRY_TOWEL);

            Destroy(this);
        }

        if (!levelController.isPause)
        {
            blueBarSlider.value -= ChangeSpeed * Time.deltaTime;
            levelController.PlayerBlood += helpBloodValue * Time.deltaTime;
        }
    }
}
