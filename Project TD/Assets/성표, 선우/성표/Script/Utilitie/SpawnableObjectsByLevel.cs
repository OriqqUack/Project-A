using System.Collections.Generic;

[System.Serializable]
public class SpawnableObjectsByLevel<T>
{
    public MapLevelSO mapLevel;
    public List<SpawnableObjectRatio<T>> spawnableObjectRatioList;
}
