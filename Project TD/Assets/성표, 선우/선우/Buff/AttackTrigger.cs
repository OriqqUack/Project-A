using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackTrigger : MonoBehaviour
{

    //private void OnTriggerEnter(Collider other)
    //{
    //    Buff monsterBuff = other.GetComponent<Buff>();
    //    if (monsterBuff != null)
    //    {
    //        monsterBuff.ApplyBuff(new AttackBuff(10f, monsterBuff.gameObject, 20));
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        MonsterStat monsterStat = other.GetComponent<MonsterStat>();
        monsterStat.Attack += 10;

    }

    private void OnTriggerExit(Collider other)
    {
        MonsterStat monsterStat = other.GetComponent<MonsterStat>();
        monsterStat.Attack -= 10;
    }
}