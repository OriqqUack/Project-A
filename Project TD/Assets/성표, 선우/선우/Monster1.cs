using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : MonoBehaviour
{
    [SerializeField]
    MonsterStat _stat;


    public Define.Monsters MonsterType { get; private set; } = Define.Monsters.Monster1;
    void Start()
    {
        _stat = gameObject.GetComponent<MonsterStat>();
        
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    
}
