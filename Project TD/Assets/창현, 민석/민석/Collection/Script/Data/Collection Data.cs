using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minseok.CollectionSystem
{
    public abstract class CollectionData : ScriptableObject
    {
        public int ID => _id;
        public string Name => _name;
        public string Tooltip => _tooltip;
        public Sprite IconSprite => _iconSprite;

        [SerializeField] private int _id;
        [SerializeField] private string _name;    // 도감 이름
        [Multiline]
        [SerializeField] private string _tooltip; // 도감 설명
        [SerializeField] private Sprite _iconSprite; // 도감 아이콘

        /// <summary> 타입에 맞는 새로운 아이템 생성 </summary>
        public abstract Collection CreateColletion();

    }
}

