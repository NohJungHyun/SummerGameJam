using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnningPool : MonoBehaviour
{
    [SerializeField]
    public static Queue<Ghost> spawnQueue = new Queue<Ghost>(20);

    public WallSizeController wallSizeController;
    public List<Ghost> ghosts = new List<Ghost>();

    // 현재 존재하는 유령의 수, 최대 존재 가능 유령의 수
    public int curGhostNum, maxGhostNum;

    //유령 생성 시간 및 유령 생성까지의 시간
    public float spawnTime, needSpawnTime;

    // 현재 맵에 존재하는 유령의 수.
    public float spawnOffsetX, spawnOffsetY;

    public Vector2 screenCenter;
    public float horizontalSize, verticalSize;

    void Start()
    {
        wallSizeController = GetComponent<WallSizeController>();

        for (int i = 0; i < 3; i++)
        {
            int randGhost = Random.Range(0, ghosts.Count);

            GameObject ghostInst = GameObject.Instantiate(ghosts[randGhost].gameObject);

            spawnQueue.Enqueue(ghostInst.GetComponent<Ghost>());
            ghostInst.SetActive(false);
            print("인큐되었다!");
        }

        spawnTime = 0;
        StartCoroutine(Spawn());

        Vector3 screenToWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        horizontalSize = screenToWorldPos.x;
        verticalSize = screenToWorldPos.y;
        screenCenter = new Vector2(horizontalSize, verticalSize);
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));

            if (curGhostNum < maxGhostNum)
            {
                int randGhost = Random.Range(0, ghosts.Count);
                int randWall = Random.Range(0, 4);

                float randX = Random.Range(-horizontalSize, horizontalSize);
                float randY = Random.Range(-verticalSize, verticalSize);

                Ghost obj = spawnQueue.Dequeue();
                print("디큐되었다!");

                // 왼쪽 -> 오른쪽 실험
                obj.transform.position = new Vector2(-horizontalSize, randY);
                obj.SetDirection(SetDestinationToGhost(false, 0));
                obj.gameObject.SetActive(true);

                curGhostNum++;

                // float randX = Random.Range(-wallSizeController.rightWall.bounds.size.x, wallSizeController.rightWall.bounds.size.x);
                // float randY = Random.Range(-wallSizeController.rightWall.bounds.size.y, wallSizeController.rightWall.bounds.size.y);

                // switch (randWall)
                // {
                //     case 0: // 오른쪽
                //         // GameObject.Instantiate(ghosts[randGhost], new Vector2(wallSizeController.rightWall.transform.position.x, wallSizeController.rightWall.transform.position.y + randY), Quaternion.identity);
                //         obj.transform.position = new Vector2(wallSizeController.rightWall.transform.position.x, wallSizeController.rightWall.transform.position.y + randY);
                //         obj.SetDirection(SetDestinationToGhost(wallSizeController.leftWall, wallSizeController.rightWall.bounds.size.y * 0.5f, false));
                //         break;

                //     case 1: // 왼쪽
                //         // GameObject.Instantiate(ghosts[randGhost], new Vector2(wallSizeController.leftWall.transform.position.x, wallSizeController.leftWall.transform.position.y + randY), Quaternion.identity);
                //         obj.transform.position = new Vector2(wallSizeController.leftWall.transform.position.x, wallSizeController.leftWall.transform.position.y + randY);
                //         ghosts[randGhost].SetDirection(SetDestinationToGhost(wallSizeController.rightWall, wallSizeController.leftWall.bounds.size.y * 0.5f, false));
                //         break;

                //     case 2: // 위쪽
                //         //GameObject.Instantiate(ghosts[randGhost], new Vector2(wallSizeController.topWall.transform.position.x + randX, wallSizeController.topWall.transform.position.y), Quaternion.identity);
                //         obj.transform.position = new Vector2(wallSizeController.topWall.transform.position.x + randX, wallSizeController.topWall.transform.position.y);
                //         ghosts[randGhost].SetDirection(SetDestinationToGhost(wallSizeController.downWall, wallSizeController.topWall.bounds.size.x * 0.5f, true));
                //         break;

                //     case 3: // 아래쪽
                //         //GameObject.Instantiate(ghosts[randGhost], new Vector2(wallSizeController.downWall.transform.position.x + randX, wallSizeController.downWall.transform.position.y), Quaternion.identity);
                //         obj.transform.position = new Vector2(wallSizeController.downWall.transform.position.x + randX, wallSizeController.downWall.transform.position.y);
                //         ghosts[randGhost].SetDirection(SetDestinationToGhost(wallSizeController.topWall, wallSizeController.downWall.bounds.size.x * 0.5f, true));
                //         break;
                // }
            }
        }
    }

    Vector3 SetDestinationToGhost(bool isHorizontal, int idx)
    {
        float num = 0;
        Vector2 vec = Vector2.one;

        // 수평인가? 즉, top, bottom인가?
        if (isHorizontal)
        {
            num = Random.Range(-horizontalSize, horizontalSize) * 0.5f;

            if (idx == 0) //위
                vec = new Vector3(num, verticalSize);
            else
                vec = new Vector3(num, -verticalSize);

        }
        else
        {
            num = Random.Range(-verticalSize, verticalSize);

            if (idx == 0) //오른쪽
                vec = new Vector3(wallSizeController.rightWall.transform.position.x + 1f, num); //horizontalSize
            else
                vec = new Vector3(-horizontalSize, num);
        }

        Debug.Log(vec);
        return vec;
    }
}
