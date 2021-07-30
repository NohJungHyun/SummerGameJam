using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnningPool : MonoBehaviour
{
    public GameObject ghostParent;

    public Queue<GameObject> spawnQueue = new Queue<GameObject>(20);

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

        for (int i = 0; i < 20; i++)
        {
            int randGhost = Random.Range(0, ghosts.Count);

            GameObject ghostInst = GameObject.Instantiate(ghosts[randGhost]);
            ghostInst.transform.SetParent(ghostParent.transform);
            ghostInst.SetActive(false);

            spawnQueue.Enqueue(ghostInst);
        }

        spawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (curGhostNum >= maxGhostNum) return;

        needSpawnTime += Time.deltaTime;

        // int randGhost = Random.Range(0, ghosts.Count);
        int randWall = Random.Range(0, 4);

        float randX = Random.Range(-spawnOffsetX, spawnOffsetX);
        float randY = Random.Range(-spawnOffsetY, spawnOffsetY);

        GameObject ghostObj = spawnQueue.Dequeue();

        switch (randWall)
        {
            case 0:
                ghostObj.transform.position = new Vector2(wallSizeController.rightWall.transform.position.x, wallSizeController.rightWall.transform.position.y + randY);
                break;
            case 1:
                ghostObj.transform.position = new Vector2(wallSizeController.leftWall.transform.position.x, wallSizeController.leftWall.transform.position.y + randY);
                break;
            case 2:
                ghostObj.transform.position = new Vector2(wallSizeController.topWall.transform.position.x + randX, wallSizeController.topWall.transform.position.y);
            break;
            case 3:
                ghostObj.transform.position = new Vector2(wallSizeController.downWall.transform.position.x + randX, wallSizeController.downWall.transform.position.y);
            break;
        }

        ghostObj.SetActive(true);

        curGhostNum++;
        needSpawnTime = 0;
    }
}
