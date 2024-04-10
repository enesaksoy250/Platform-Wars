using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTheDoor : MonoBehaviour
{


    [SerializeField] float pointY;
    bool isLoop;
    bool isStart;
    bool slowlyOpen;

    void Start()
    {
        slowlyOpen = true;
        isStart = false;
        isLoop = true;
    }


    void Update()
    {


        if (isStart && isLoop && slowlyOpen)
        {


            transform.position -= new Vector3(0, 0.01f, 0);
            StartCoroutine(SlowlyOpen());

            if (transform.position.y <= pointY)
            {

                isLoop = false;

            }


        }
    }

    public void CloseDoor()
    {

        isStart = true;

    }

    IEnumerator SlowlyOpen()
    {

        slowlyOpen = false;
        yield return new WaitForSeconds(.01f);
        slowlyOpen = true;

    }
}
