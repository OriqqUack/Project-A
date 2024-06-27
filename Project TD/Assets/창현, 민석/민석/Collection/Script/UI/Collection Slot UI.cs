using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


namespace Minseok.CollectionSystem
{
    public class CollectionSlotUI : MonoBehaviour
    {

        #region .
        /// <summary> 슬롯의 인덱스 </summary>
        public int Index { get; private set; }

        private RectTransform _slotRect;
        public RectTransform SlotRect => _slotRect;

        /// <summary> 접근 가능한 슬롯인지 여부 </summary>
        public bool IsAccessible => _isAccessibleSlot && _isAccessibleItem;

        #endregion

        #region .
        private CollectionSystemUI _collectionUI;

        private bool _isAccessibleSlot = true; // 슬롯 접근가능 여부
        private bool _isAccessibleItem = true; // 도감 접근가능 여부

        #endregion

        private void Awake()
        {
            InitComponents();
        }

        // private 함수
        private void InitComponents()
        {
            _collectionUI = GetComponentInParent<CollectionSystemUI>();
            _slotRect = GetComponent<RectTransform>();
        }

        // 편의성 함수

        public void SetSlotIndex(int index) => Index = index;

    }

}
