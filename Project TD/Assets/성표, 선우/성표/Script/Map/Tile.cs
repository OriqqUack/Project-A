using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region REQUIRE COMPONENT
[RequireComponent(typeof(MaterializeEffect))]
#endregion REQUIRE COMPONENT

[DisallowMultipleComponent]
public class Tile : MonoBehaviour
{
    public TileDetailSO tileDetails;
    private MaterializeEffect materializeEffect;
    //GameObject -> ENEMYDETAIL로 수정필요
    public List<SpawnableObjectsByLevel<GameObject>> spawnableObjectsByLevelList;
    public Vector3[] spawnPositionArray;

    private void Awake()
    {
        materializeEffect = GetComponent<MaterializeEffect>();
        materializeEffect.InitalizeMaterial();
    }

    private void OnEnable()
    {
        TileInitialization();
    }


    public void TileInitialization()
    {
        this.tileDetails = GameManager.Instance.GetTile(GameManager.Instance.currentTileIndex).tileDetails;
        StartCoroutine(MaterializeTile(tileDetails));
    }

    private IEnumerator MaterializeTile(TileDetailSO tileDetail)
    {
        TileEnable(false);

        if (tileDetails == null) Debug.Log("null");
        yield return StartCoroutine(materializeEffect.MaterializeRoutine(tileDetail.materializeShader, tileDetail.tileMaterializeColor, tileDetail.materializeTime, tileDetail.dissolveName));

        TileEnable(true);
    }

    private void TileEnable(bool isEnable)
    {

    }

}





