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
        /// <summary> ������ �ε��� </summary>
        public int Index { get; private set; }

        private RectTransform _slotRect;
        public RectTransform SlotRect => _slotRect;

        /// <summary> ���� ������ �������� ���� </summary>
        public bool IsAccessible => _isAccessibleSlot && _isAccessibleItem;

        #endregion

        #region .
        private CollectionSystemUI _collectionUI;

        private bool _isAccessibleSlot = true; // ���� ���ٰ��� ����
        private bool _isAccessibleItem = true; // ���� ���ٰ��� ����

        #endregion

        private void Awake()
        {
            InitComponents();
        }

        // private �Լ�
        private void InitComponents()
        {
            _collectionUI = GetComponentInParent<CollectionSystemUI>();
            _slotRect = GetComponent<RectTransform>();
        }

        // ���Ǽ� �Լ�

        public void SetSlotIndex(int index) => Index = index;

    }

}
