using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minseok.CollectionSystem
{
    /// <summary> ���� - ���� ���� </summary>
    [CreateAssetMenu(fileName = "Collection_Achievement", menuName = "Collection System/Collection Data/Achievement", order = 0)]
    public class CAchievementData : CollectionData
    {
        public override Collection CreateColletion()
        {
            return new CAchievement(this);
        }
    }

}
