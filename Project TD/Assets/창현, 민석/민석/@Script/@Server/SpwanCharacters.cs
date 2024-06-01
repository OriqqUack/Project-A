using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using System.Linq;
using Photon.Realtime;

public class SpwanCharacters : MonoBehaviour
{
    public GameObject character;
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            GameObject player = PhotonNetwork.Instantiate(character.name, spawnPoints[PhotonNetwork.CountOfPlayers - 1].position, Quaternion.identity);
            player.name = $"Player{Managers.Pool._playerCount}";
            Managers.Pool._playerCount++;
        }
        /*GameObject player = PhotonNetwork.Instantiate("PlayerPrefab", Vector3.zero, Quaternion.identity);
        player.name = "Player";*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
