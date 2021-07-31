using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GhostCaterpalt
{
    public List<Ghost> ghosts;
    public List<float> numPerGhost;
}

public class GhostThrower : MonoBehaviour
{
    public GhostCaterpalt ghostCaterpalt;
    SpawnningPool spawnningPool;

    // Start is called before the first frame update
    void Start()
    {
        spawnningPool = GameObject.FindObjectOfType<SpawnningPool>();

        TimeChecker.TimeOn += ThrowToSpwanList;
    }

    public void ThrowToSpwanList()
    {
        if (ghostCaterpalt.ghosts.Count <= 0) return;

        for (int x = 0; x < ghostCaterpalt.ghosts.Count; x++)
        {
            for (int y = 0; y < ghostCaterpalt.numPerGhost[x]; y++)
            {
                Ghost g = ghostCaterpalt.ghosts[x];
                g.SetGhostProperties(g.proto);

                spawnningPool.ghosts.Add(g);
            }
        }
    }
}
