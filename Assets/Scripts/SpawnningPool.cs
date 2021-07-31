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
    public static int curGhostNum;
    public int minGhostNum;
    public int maxGhostNum;

    //유령 생성 시간 및 유령 생성까지의 시간
    public float minSpawnTime, maxSpawnTime;
    public int ememrgencyCallNum;

    [Header("출현 위치 설정 파트")]
    public Vector2 screenCenter;

    public float horizontalSize, verticalSize;
    public float offset;

    public static List<Vector2> Node = new List<Vector2>();
    List<int>[] V = new List<int>[8];

    void Start()
    {
        curGhostNum = 0;
        spawnQueue.Clear();

        for (int i = 0; i < spawnThrehold; i++)
            CreateGhost();

        StartCoroutine(Spawn());

        Vector3 screenToWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        horizontalSize = screenToWorldPos.x;
        verticalSize = screenToWorldPos.y;
        screenCenter = new Vector2(horizontalSize, verticalSize);

        MakeGraph();
    }

    private void MakeGraph()
    {
        Node.Clear();
        for (int i = 1; i >= -1; i-=2)
        {
            Node.Add(new Vector2(horizontalSize * +0.5f, verticalSize * +1.2f) * i);
            Node.Add(new Vector2(horizontalSize * +1.2f, verticalSize * +0.5f) * i);
            Node.Add(new Vector2(horizontalSize * +1.2f, verticalSize * -0.5f) * i);
            Node.Add(new Vector2(horizontalSize * +0.5f, verticalSize * -1.2f) * i);
        }

        for (int i = 0; i < 8; i++)
            V[i] = new List<int>();

        V[0].Add(3);
        V[0].Add(4);
        V[0].Add(5);

        V[1].Add(5);
        V[1].Add(6);

        V[2].Add(5);
        V[2].Add(6);

        V[3].Add(0);
        V[3].Add(6);
        V[3].Add(7);

        V[4].Add(0);
        V[4].Add(1);
        V[4].Add(7);

        V[5].Add(1);
        V[5].Add(2);

        V[6].Add(1);
        V[6].Add(2);

        V[7].Add(2);
        V[7].Add(3);
        V[7].Add(4);
    }

    List<int> GetSpawnPos()
    {
        List<int> temp = new List<int>();
        bool[] pos = new bool[8];
        foreach (Ghost g in spawnQueue)
            if (g.spawnPos != -1)
                pos[g.spawnPos] = true;

        for (int i = 0; i < 8; i++)
            if (!pos[i] && i != 0 && i != 7)
                temp.Add(i);

        return temp;
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            if (spawnQueue.Count <= 0)
                yield return new WaitUntil(() => spawnQueue.Count > 0);

            List<int> spawnPos = GetSpawnPos();

            if (curGhostNum < maxGhostNum - 1 && spawnPos.Count > 0)
            {
                if (curGhostNum < minGhostNum * 0.5f)
                    yield return new WaitForSeconds(Random.Range(minSpawnTime * 0.25f, maxSpawnTime * 0.25f));
                else if(curGhostNum < minGhostNum)
                    yield return new WaitForSeconds(Random.Range(minSpawnTime * 0.5f, maxSpawnTime * 0.5f));
                else
                    yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

                //int randGhost = Random.Range(0, ghosts.Count);
                //int randWall = Random.Range(0, 4);

                //float randX = Random.Range(-horizontalSize, horizontalSize);
                //float randY = Random.Range(-verticalSize, verticalSize);

                Ghost obj = spawnQueue.Dequeue();

                int startNode = spawnPos[Random.Range(0, spawnPos.Count)];
                
                int endNode = V[startNode][Random.Range(0, V[startNode].Count)];

                obj.spawnPos = startNode;
                obj.targetPos = endNode;
                obj.transform.position = Node[startNode];
                obj.SetDirection(Node[endNode]);

                //switch (randWall)
                //{
                //    case 0: //왼벽 -> 오른벽
                //        obj.transform.position = new Vector2(-horizontalSize - offset, randY);
                //        obj.SetDirection(SetDestinationToGhost(false, 0));
                //        break;

                //    case 1: //오른벽 -> 왼벽
                //        obj.transform.position = new Vector2(horizontalSize + offset, randY);
                //        obj.SetDirection(SetDestinationToGhost(false, 1));
                //        break;

                //    case 2: //윗벽 -> 아랫벽
                //        obj.transform.position = new Vector2(randX, -verticalSize - offset);
                //        obj.SetDirection(SetDestinationToGhost(true, 0));
                //        break;

                //    case 3: //아랫벽 -> 윗벽
                //        obj.transform.position = new Vector2(randX, verticalSize + offset);
                //        obj.SetDirection(SetDestinationToGhost(true, 1));
                //        break;
                //}

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
