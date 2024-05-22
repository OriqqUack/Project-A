using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSP
{
    [DisallowMultipleComponent]
    public class GameResources : MonoBehaviour
    {
        private static GameResources instance;

        public static GameResources Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<GameResources>("GameResources");
                }
                return instance;
            }
        }

        #region Header Map Type List
        [Space(10)]
        [Header("¸Ê ¸®½ºÆ®")]
        #endregion Header Map Type List
        public MapTypeListSO mapTypeList;

    }
}
