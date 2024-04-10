using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChickenController : MonoBehaviour
{

    Animator chickenAnimator;
    bool isLoop = true;
    bool isMove;

    void Start()
    {

        chickenAnimator = GetComponent<Animator>();
        StartCoroutine(nameof(ChickenMove));

    }

    private void Update()
    {

        float localScaleX = transform.localScale.x;

        if (isMove)
        {

            if (gameObject.name == "Chick")
                localScaleX = -localScaleX;

            transform.Translate(new Vector2(.5f * localScaleX * Time.deltaTime, 0));

            if (transform.position.x < 47)
            {
                if (gameObject.name == "Chick")
                    transform.localScale = new Vector3(-1, 1, 1);

                else
                    transform.localScale = new Vector3(1, 1, 1);

            }

            if (transform.position.x > 55)
            {
                if (gameObject.name == "Chick")
                    transform.localScale = new Vector3(1, 1, 1);

                else
                    transform.localScale = new Vector3(-1, 1, 1);

            }

        }


    }


    IEnumerator ChickenMove()
    {

        while (isLoop)
        {
            isLoop = false;

            chickenAnimator.SetBool("Peck", true);

            int a = Random.Range(1, 7);

            yield return new WaitForSeconds(a);

            chickenAnimator.SetBool("Peck", false);

            int b = Random.Range(1, 7);

            yield return new WaitForSeconds(b);

            chickenAnimator.SetBool("Run", true);

            isMove = true;

            int c = Random.Range(1, 5);

            yield return new WaitForSeconds(c);

            chickenAnimator.SetBool("Run", false);

            isMove = false;

            isLoop = true;

        }
    }
}
