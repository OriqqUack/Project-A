using Rito.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Minseok.CollectionSystem
{
    public class CollectionSystemUI : MonoBehaviour
    {
        /// <summary> 연결된 인벤토리 </summary>
        private CollectionSystem _collectionSystem;

        private GraphicRaycaster _gr;
        private PointerEventData _ped;
        private List<RaycastResult> _rrList;

        [Space]
        [SerializeField] private bool _showTooltip = true;
        [SerializeField] private bool _showHighlight = true;

        [Header("Connected Objects")]
        [SerializeField] private ItemTooltipUI _collectionTooltip;   // 도감 정보를 보여줄 툴팁 UI




        /// <summary> 도감 참조 등록 (도감에서 직접 호출) </summary>
        public void SetCollectionReference(CollectionSystem collectionSystem)
        {
            _collectionSystem = collectionSystem;
        }

        /// <summary> 접근 가능한 슬롯 범위 설정 </summary>
        public void SetAccessibleSlotRange(int accessibleSlotCount)
        {
            
        }
    }
}

