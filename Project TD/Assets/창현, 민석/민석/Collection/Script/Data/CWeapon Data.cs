using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Minseok.CollectionSystem
{
    /// <summary> 도감 - 무기 도감 </summary>
    [CreateAssetMenu(fileName = "Collection_Weapon", menuName = "Collection System/Collection Data/Weaopn", order = 2)]
    public class CWeaponData : CollectionData
    {
        public override Collection CreateColletion()
        {
            return new CWeapon(this);
        }
    }
}

