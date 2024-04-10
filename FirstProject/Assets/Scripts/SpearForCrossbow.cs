using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearForCrossbow : MonoBehaviour
{

    [SerializeField] float spearFast;
    [SerializeField] float power;
    CrossbowSettings crossbowSettingsScript;
    BoxCollider2D boxCollider2D;
    PlayerHealth playerHealth;
    float direction;

    void Start()
    {
        crossbowSettingsScript = FindObjectOfType<CrossbowSettings>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }


    void Update()
    {

        if (crossbowSettingsScript.isAlive)
        {

            direction = -(crossbowSettingsScript.gameObject.transform.localScale.x);
            transform.Translate(spearFast * direction * Time.deltaTime, 0, 0);

        }



        if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Main")))
        {


            Destroy(gameObject);

        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {

            playerHealth.ChangeHealth(power);
            Destroy(gameObject);

        }

    }


}
