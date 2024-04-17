using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    // Start is called before the first frame update
    void Start()
    {
        _level = 1;
        _hp = 100;
        _maxHp = 100;
        _attack = 10;
        _defense = 5;
        _moveSpeed = 5.0f;
        _dashingPower = 10.0f;
        _dashingTime = 0.2f;
        _dashingCooldown = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
