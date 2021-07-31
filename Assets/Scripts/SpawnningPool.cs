using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnningPool : MonoBehaviour
{
    public Transform spawnerParent;

    [SerializeField]
    public static Queue<Ghost> spawnQueue = new Queue<Ghost>();

    [Header("초기 Queue에 유령들을 만들고자 하는 수를 설정할 때 사용.")]
    [SerializeField]
    int spawnThrehold;

    public List<Ghost> ghosts = new List<Ghost>();

    [Header("유령 수, 소환 텀 등을 결정할 때 사용")]
    // 현재 존재하는 유령의 수, 최대 존재 가능 유령의 수
    public int curGhostNum;
    public int minGhostNum;
    public int maxGhostNum;

    //유령 생성 시간 및 유령 생성까지의 시간
    public float minSpawnTime, maxSpawnTime;
    public int ememrgencyCallNum;

    [Header("출현 위치 설정 파트")]
    public Vector2 screenCenter;

    public float horizontalSize, verticalSize;
    public float offset;

    void Start()
    {
        spawnQueue.Clear();

        for (int i = 0; i < spawnThrehold; i++)
        {
            CreateGhost();
            print("인큐되었다!");
        }
        
        StartCoroutine(Spawn());

        Vector3 screenToWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        horizontalSize = screenToWorldPos.x;
        verticalSize = screenToWorldPos.y;
        screenCenter = new Vector2(horizontalSize, verticalSize);

        Debug.Log(screenToWorldPos);
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            if (spawnQueue.Count <= 0)
                yield return new WaitUntil(() => spawnQueue.Count > 0);

            if (curGhostNum < maxGhostNum - 1)
            {
                if (curGhostNum < minGhostNum * 0.5f)
                    yield return new WaitForSeconds(Random.Range(minSpawnTime * 0.25f, maxSpawnTime * 0.25f));
                else if(curGhostNum < minGhostNum)
                    yield return new WaitForSeconds(Random.Range(minSpawnTime * 0.5f, maxSpawnTime * 0.5f));
                else
                    yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

                int randGhost = Random.Range(0, ghosts.Count);
                int randWall = Random.Range(0, 4);

                float randX = Random.Range(-horizontalSize, horizontalSize);
                float randY = Random.Range(-verticalSize, verticalSize);

                Ghost obj = spawnQueue.Dequeue();

                print("디큐되었다!");

                switch (randWall)
                {
                    case 0: //왼벽 -> 오른벽
                        obj.transform.position = new Vector2(-horizontalSize - offset, randY);
                        obj.SetDirection(SetDestinationToGhost(false, 0));
                        break;

                    case 1: //오른벽 -> 왼벽
                        obj.transform.position = new Vector2(horizontalSize + offset, randY);
                        obj.SetDirection(SetDestinationToGhost(false, 1));
                        break;

                    case 2: //윗벽 -> 아랫벽
                        obj.transform.position = new Vector2(randX, -verticalSize - offset);
                        obj.SetDirection(SetDestinationToGhost(true, 0));
                        break;

                    case 3: //아랫벽 -> 윗벽
                        obj.transform.position = new Vector2(randX, verticalSize + offset);
                        obj.SetDirection(SetDestinationToGhost(true, 1));
                        break;
                }

                obj.gameObject.SetActive(true);
                curGhostNum++;
            }
            yield return null;
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
                vec = new Vector3(num, verticalSize + offset);
            else
                vec = new Vector3(num, -verticalSize - offset);

        }
        else
        {
            num = Random.Range(-verticalSize, verticalSize);

            if (idx == 0) //오른쪽
                vec = new Vector3(horizontalSize + offset, num); //horizontalSize
            else
                vec = new Vector3(-horizontalSize - offset, num);
        }

        Debug.Log(vec);
        return vec;
    }

    public void AssembleEmergency()
    {
        if (curGhostNum < minGhostNum)
        {
            for(int i = 0; i < ememrgencyCallNum; i++)
            {
                CreateGhost();
            }
        }
    }

    public void CreateGhost()
    {
        int randGhost = Random.Range(0, ghosts.Count);

        GameObject ghostInst = GameObject.Instantiate(ghosts[randGhost].gameObject);

        ghostInst.transform.SetParent(spawnerParent);
        spawnQueue.Enqueue(ghostInst.GetComponent<Ghost>());
        ghostInst.SetActive(false);
    }
}
