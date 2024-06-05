using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minseok.CollectionSystem
{
    /// <summary> 도감 - 업적 도감 </summary>
    [CreateAssetMenu(fileName = "Collection_Achievement", menuName = "Collection System/Collection Data/Achievement", order = 0)]
    public class CAchievementData : CollectionData
    {
        public override Collection CreateColletion()
        {
            return new CAchievement(this);
        }
    }

}
