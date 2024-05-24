using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using System.Linq;
using Photon.Realtime;
using UnityEngine.TextCore.Text;

public class ServerPlayerManager : MonoBehaviour
{
    public PhotonView _pv;
    public GameObject _character;
    public Transform[] _spawnPoints;

    private void Awake()
    {
        _pv = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (_pv.IsMine)
        //photon view가 local player에 의해 소유된 경우 PV.IsMine이 true
        {
            CreateController();
        }
    }

    // Instantiate our player controller
    void CreateController()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Instantiate(_character.name, _spawnPoints[PhotonNetwork.CountOfPlayers - 1].position, _spawnPoints[PhotonNetwork.CountOfPlayers - 1].rotation, 0, new object[] { _pv.ViewID });
        }

        // PhotonNetwork.Instantiate(string prefabNames, Vector3 position, Quternion rotation, byte group = 0, object[] data = null) 
        // group: 0 is the group of prefab 
        // data : actual parameters that pass into the prefab we instantiate -> send the view id into the instantiation method
    }

    public void Die()
    {
        PhotonNetwork.Destroy(_character);
        // 플레이어가 죽을 경우 씬에 있는 playerController 파괴

        CreateController(); // 리스폰
    }
}
