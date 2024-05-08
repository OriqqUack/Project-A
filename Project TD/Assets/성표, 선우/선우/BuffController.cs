using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BuffController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Buff monsterBuff = other.GetComponent<Buff>();
        if (monsterBuff != null)
        {
            monsterBuff.ApplyBuff(new AttackBuff(10f, monsterBuff.gameObject, 20));
        }
    }

}