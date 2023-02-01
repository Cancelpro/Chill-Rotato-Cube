using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class CubeControl : MonoBehaviour
{

    public int speed = 300;
    bool isMoving = false;
    public int cubeX = 0;
    public int cubeZ = 0;
    int oldCubeX = 0;
    int oldCubeZ = 0;
    GameObject level;
    public bool hasWon = false;
    float time = 0;
    [SerializeField] GameObject canvy;
    [SerializeField] GameObject Cube2;
    bool hasPlayedFanFare = false;
    public Cube.SideResults winningColor = Cube.SideResults.na;
    private void Start()
    {
        level = GameObject.Find("Level");
    }

    private void Update()
    {
        if (hasWon)
        {
            transform.Translate(new Vector3(0, 10, 0) * 1 * Time.deltaTime, Space.World);
            transform.Rotate(0, 500 * Time.deltaTime, 0, Space.World);
            if (!hasPlayedFanFare)
            {
                AudioHandler.instance.PlayFanFare();
                hasPlayedFanFare=true;
            }
            if(Time.time - time > 0.5f)
            {
                canvy.SetActive(true);
                
            }
            
        }
        if (isMoving) return;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            cubeX += 1;
            StartCoroutine(Roll(Vector3.right));
        }else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            cubeX -= 1;
            StartCoroutine(Roll(Vector3.left));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            cubeZ += 1;
            StartCoroutine(Roll(Vector3.forward));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            cubeZ -= 1;
            StartCoroutine(Roll(Vector3.back));
        }

    }

    IEnumerator Roll(Vector3 direction)
    {

        if (CheckMove())
        {
            isMoving = true;
            float remainAngle = 90;
            Vector3 rotationCenter = transform.position + direction / 2 + Vector3.down / 2;
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);
            while(remainAngle > 0)
            {
                float rotationAngle = Mathf.Min(Time.deltaTime * speed, remainAngle);

                transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
                remainAngle -= rotationAngle;
                yield return null;

            }
            isMoving = false;
            transform.position = new Vector3(cubeX, 0, cubeZ);

        }
        if(Cube2 != null) {
            if (transform.GetComponent<Cube>().selectedResult == winningColor && Cube2.GetComponent<CubeControl>().winningColor == Cube2.GetComponent<Cube>().selectedResult)
            {
                hasWon = true;
                time = Time.time;
            }
        }
        else
        {
            if (transform.GetComponent<Cube>().selectedResult == winningColor)
            {
                hasWon = true ;
                time = Time.time;
            }

        }
        yield return new WaitForEndOfFrame();
    }


    public bool CheckMove()
    {
        if (Cube2 != null)
        {
            for (int i = 0; i < level.transform.childCount; i++)
            {
                if (new Vector3(cubeX, -1, cubeZ) == level.transform.GetChild(i).transform.position && new Vector3(cubeX, -1, cubeZ) != new Vector3(Cube2.GetComponent<CubeControl>().cubeX, -1, Cube2.GetComponent<CubeControl>().cubeZ))
                {
                    oldCubeX = cubeX;
                    oldCubeZ = cubeZ;

                    if (level.transform.GetChild(i).GetComponent<EndGame>())
                    {
                        winningColor = level.transform.GetChild(i).GetComponent<EndGame>().color;
                    }
                    else
                    {
                        winningColor = Cube.SideResults.na;
                    }

                    return true;


                }
            }
        }
        else
        {

            for (int i = 0; i < level.transform.childCount; i++)
            {
                if (new Vector3(cubeX, -1, cubeZ) == level.transform.GetChild(i).transform.position)
                {
                    oldCubeX = cubeX;
                    oldCubeZ = cubeZ;

                    if (level.transform.GetChild(i).GetComponent<EndGame>())
                    {
                        winningColor = level.transform.GetChild(i).GetComponent<EndGame>().color;
                    }
                    else
                    {
                        winningColor = Cube.SideResults.na;
                    }

                    return true;


                }

            }
        }
        
        cubeX = oldCubeX;
        cubeZ = oldCubeZ;
        return false;

    }
}
