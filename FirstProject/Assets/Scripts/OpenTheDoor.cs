using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTheDoor : MonoBehaviour
{

    [SerializeField] string direction;
    [SerializeField] float pointX;
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


        if (isStart)
        {

            if (direction == "x" && isLoop && slowlyOpen)
            {

                transform.position -= new Vector3(0.01f, 0, 0);
                StartCoroutine(SlowlyOpen());

                if (transform.position.x <= pointX)
                {

                    isLoop = false;
                }

            }

            else if (direction == "y" && isLoop && slowlyOpen)
            {

                transform.position += new Vector3(0, 0.01f, 0);
                StartCoroutine(SlowlyOpen());

                if (transform.position.y >= pointY)
                {

                    isLoop = false;

                }

            }
        }
    }

    public void OpenDoor()
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
