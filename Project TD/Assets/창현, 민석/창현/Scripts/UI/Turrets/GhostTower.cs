using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTower : MonoBehaviour
{
    public MeshRenderer[] _meshRender;

    public Material _valid;
    public Material _inValid;
    void Start()
    {
        _meshRender = GetComponentsInChildren<MeshRenderer>();
        _valid = Managers.Resource.Load<Material>("Prefabs/TD UI/PossibleGhost");
        _inValid = Managers.Resource.Load<Material>("Prefabs/TD UI/ImpossibleGhost");

    }


}
