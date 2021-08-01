using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageGhostImprovement
{
    public List<float> elementImprovementNum;
    public List<Ghost> ghosts;
}

[CreateAssetMenu(menuName = "ScriptableObjects /New GhostPercentageTable")]
public class MonsterRevealTable : ScriptableObject
{
    [Header("아임 소 노멀: 지그재그, 점멸 등")]
    public StageGhostImprovement normalGhostSpeedTable;
    [Header("아임 소 패스트: 대쉬 등")]
    public StageGhostImprovement fastGhostSpeedTable;

    [Space(10)]

    [Header("최소 유령 제한")]
    public List<float> minGhostNumTable;
    [Header("최대 유령 제한")]
    public List<float> maxGhostNumTable;
    [Header("최소 스폰 시간")]
    public List<float> minSpawnTime;
    [Header("최소 스폰 시간")]
    public List<float> maxSpawnTime;
}
