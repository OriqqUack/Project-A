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
        /// <summary> ����� �κ��丮 </summary>
        private CollectionSystem _collectionSystem;

        private GraphicRaycaster _gr;
        private PointerEventData _ped;
        private List<RaycastResult> _rrList;

        [Space]
        [SerializeField] private bool _showTooltip = true;
        [SerializeField] private bool _showHighlight = true;

        [Header("Connected Objects")]
        [SerializeField] private ItemTooltipUI _collectionTooltip;   // ���� ������ ������ ���� UI




        /// <summary> ���� ���� ��� (�������� ���� ȣ��) </summary>
        public void SetCollectionReference(CollectionSystem collectionSystem)
        {
            _collectionSystem = collectionSystem;
        }

        /// <summary> ���� ������ ���� ���� ���� </summary>
        public void SetAccessibleSlotRange(int accessibleSlotCount)
        {
            
        }
    }
}

