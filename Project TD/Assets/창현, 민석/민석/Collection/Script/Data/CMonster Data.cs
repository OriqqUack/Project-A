using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minseok.CollectionSystem
{
    /// <summary> ���� - ���� ���� </summary>
    [CreateAssetMenu(fileName = "Collection_Monster", menuName = "Collection System/Collection Data/Monster", order = 1)]
    public class CMonsterData : CollectionData
    {
        public override Collection CreateColletion()
        {
            return new CMonster(this);
        }
    }
}

