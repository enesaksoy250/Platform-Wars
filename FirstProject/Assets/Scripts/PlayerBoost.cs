using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBoost : MonoBehaviour
{

    [SerializeField] Slider boostSlider;
    PlayerHealth playerHealthScript;
    bool isLoop = true;
    float boost = 100;


    void Start()
    {
        playerHealthScript = FindObjectOfType<PlayerHealth>();

    }


    void Update()
    {

        if (playerHealthScript.GetPlayerHealth() < 100)
        {

            Invoke(nameof(BoostHealth), 5f);

        }


    }


    void BoostHealth()
    {

        if (playerHealthScript.GetPlayerHealth() < 100 && boost > 1 && isLoop)
        {

            boost -= 2;
            boostSlider.value -= 0.02f;
            playerHealthScript.ChangeHealthForHealthPotion(1);
            StartCoroutine(Wait());

        }

    }

    IEnumerator Wait()
    {

        isLoop = false;

        yield return new WaitForSeconds(.2f);

        isLoop = true;

    }

    public void SetBoost(float boost1)
    {

        boost += boost1;
        boostSlider.value = boost / 100;

        if (boost > 100)
        {

            boost = 100;
            boostSlider.value = boost / 100;

        }

    }
}
