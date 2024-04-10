using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointManager : MonoBehaviour
{
    public List<Vector2> checkpointPositions = new List<Vector2>();
    public static CheckPointManager Instance;
    StartPosition startPosition;

    private void Awake()
    {

        if (Instance == null)
        {
            print("ilk defa");
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            print("ikinci defa");
            Destroy(gameObject);
        }


    }

    private void Start()
    {

     
        startPosition = FindObjectOfType<StartPosition>();       

    }

   

    public void SetCheckpoint(Vector2 checkpointPos)
    {
        checkpointPositions.Add(checkpointPos);
    }

    public Vector2 GetLastCheckpointPos()
    {
        if (checkpointPositions.Count > 0)
        {
            return checkpointPositions[checkpointPositions.Count - 1];
        }

        else
        {
         
            startPosition=FindObjectOfType<StartPosition>();
            return startPosition.startPosition;

        }
    }

    public void DeleteCheckPointPos()
    {

        checkpointPositions.Clear();
       

    }
}
