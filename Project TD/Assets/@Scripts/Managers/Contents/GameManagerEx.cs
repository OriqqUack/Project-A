using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class GameManagerEx
{
    GameObject _rocket;
    GameObject _player = GameObject.Find("rocket");
    //Dictionary<int, GameObject> _players = new Dictionary<int, GameObject>();
    HashSet<GameObject> _monsters = new HashSet<GameObject>(); // 여러마리가 소환되어야하니 HashSet을 써줌

    public Action<int> OnSpawnEvent;

    public GameObject GetRocket() { return _rocket; }
    public GameObject GetPlayer() { return _player; }

    public GameObject PlayerSpawn(Define.Players playerType, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (playerType)
        {
            case Define.Players.Normal:
                _player = go;
                break;
            case Define.Players.Knight:
                _player = go;
                break;
            case Define.Players.Gunner:
                _player = go;
                break;
            case Define.Players.Miner:
                _player = go;
                break;
            case Define.Players.Engineer:
                _player = go;
                break;
            case Define.Players.Researcher:
                _player = go;
                break;
            case Define.Players.Medic:
                _player = go;
                break;
        }

        return go;
    }
    public GameObject MonsterSpawn(Define.Monsters monsterType, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate($"Monsters/{path}", parent);


        switch (monsterType)
        {
            case Define.Monsters.Monster1:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.Monsters.Monster2:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.Monsters.Monster3:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.Monsters.Monster4:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.Monsters.Monster5:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.Monsters.Monster6:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.Monsters.Monster7:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.Monsters.Monster8:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.Monsters.Monster9:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.Monsters.Monster10:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
        }

        return go;
    }

    //public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    //{
    //    GameObject go = Managers.Resource.Instantiate(path, parent);

    //    switch (type)
    //    {
    //        case Define.WorldObject.Monster:
    //            _monsters.Add(go);
    //            if (OnSpawnEvent != null)
    //                OnSpawnEvent.Invoke(1);
    //            break;
    //        case Define.WorldObject.Player:
    //            _player = go;
    //            break;
    //    }

    //    return go;
    //}

    public Define.Players GetPlayerType(GameObject go)
    {
        PlayerController pc = go.GetComponent<PlayerController>();
        if (pc == null)
            return Define.Players.Unknown;

        return pc.PlayerType;
    }

    public Define.Monsters GetMonsterType(GameObject go)
    {
        MonsterController mc = go.GetComponent<MonsterController>();
        if (mc == null)
            return Define.Monsters.Unknown;

        return mc.MonsterType;
    }

    public void PlayerDespawn(GameObject go) // 일단 두개로 분할해봄 나중에 한개로 합치는 방향을 생각해 봐야할듯
    {
        Define.Players type = GetPlayerType(go);

        if (type == Define.Players.Unknown)
        {
            Debug.Log("디스폰 실패");
        }
        else
        {
            if (_player == go)
                _player = null;
            Managers.Resource.Destroy(go);
        }
    //    switch (type)
    //    {
    //        case Define.Players.Monster:
    //            {
    //                if (_monsters.Contains(go))
    //                {
    //                    _monsters.Remove(go);
    //                    if (OnSpawnEvent != null)
				//			OnSpawnEvent.Invoke(-1);
				//	}   
    //            }
    //            break;
    //        case Define.WorldObject.Player:
    //            {
				//	if (_player == go)
				//		_player = null;
				//}
    //            break;
    //    }
    }

    public void MonsterDespawn(GameObject go) // 몬스터 디스폰
    {
        Define.Monsters type = GetMonsterType(go);

        switch (type)
        {
            case Define.Monsters.Monster1:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.Monsters.Monster2:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.Monsters.Monster3:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.Monsters.Monster4:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.Monsters.Monster5:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.Monsters.Monster6:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.Monsters.Monster7:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.Monsters.Monster8:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.Monsters.Monster9:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.Monsters.Monster10:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
        }

        Managers.Resource.Destroy(go);
    }
}
