using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Minseok.CollectionSystem
{
    /// <summary> ���� - ���� ���� </summary>
    [CreateAssetMenu(fileName = "Collection_Weapon", menuName = "Collection System/Collection Data/Weaopn", order = 2)]
    public class CWeaponData : CollectionData
    {
        public override Collection CreateColletion()
        {
            return new CWeapon(this);
        }
    }
}

