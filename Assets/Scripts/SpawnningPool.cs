using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnningPool : MonoBehaviour
{
    public Queue spawnQueue = new Queue(20);

    public WallSizeController wallSizeController;
    public List<GameObject> ghosts = new List<GameObject>();

    // 현재 존재하는 유령의 수, 최대 존재 가능 유령의 수
    public int curGhostNum, maxGhostNum;

    //유령 생성 시간 및 유령 생성까지의 시간
    public float spawnTime, needSpawnTime;

    // 현재 맵에 존재하는 유령의 수.
    public float spawnOffsetX, spawnOffsetY;

    void Start()
    {
        wallSizeController = GetComponent<WallSizeController>();

        spawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (curGhostNum >= maxGhostNum) return;

        needSpawnTime += Time.deltaTime;

        int randGhost = Random.Range(0, ghosts.Count);
        int randWall = Random.Range(0, 4);

        float randX = Random.Range(-spawnOffsetX, spawnOffsetX);
        float randY = Random.Range(-spawnOffsetY, spawnOffsetY);

        switch (randWall)
        {
            case 0: // 오른쪽
                GameObject.Instantiate(ghosts[randGhost], new Vector2(wallSizeController.rightWall.transform.position.x + randX, wallSizeController.rightWall.transform.position.y + randY), Quaternion.identity);
                break;

            case 1: // 왼쪽
                GameObject.Instantiate(ghosts[randGhost], new Vector2(wallSizeController.leftWall.transform.position.x + randX, wallSizeController.leftWall.transform.position.y + randY), Quaternion.identity);
                break;

            case 2: // 위쪽
                GameObject.Instantiate(ghosts[randGhost], new Vector2(wallSizeController.topWall.transform.position.x + randX, wallSizeController.topWall.transform.position.y + randY), Quaternion.identity);
                break;

            case 3: // 아래쪽
                GameObject.Instantiate(ghosts[randGhost], new Vector2(wallSizeController.downWall.transform.position.x + randX, wallSizeController.downWall.transform.position.y + randY), Quaternion.identity);
                break;
        }
        curGhostNum++;
        needSpawnTime = 0;
    }
}
