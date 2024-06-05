using Rito.InventorySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;
using static UnityEditor.Progress;


namespace Minseok.CollectionSystem
{
    public class CollectionSystem : MonoBehaviour
    {
        [SerializeField]
        private CollectionSystemUI _collectionSystemUI;    // ����� ���� UI

        /// <summary> ���� ��� </summary>
        [SerializeField]
        private Collection[] _collection;

        private int _maxindex = 50;


        private void Start()
        {
            _collection = new Collection[_maxindex];
            _collectionSystemUI.SetCollectionReference(this);
        }

        private void Awake()
        {
            UpdateAccessibleStatesAll();
        }

        /// <summary> �ε����� ���� ���� ���� �ִ��� �˻� </summary>
        private bool IsValidIndex(int index)
        {
            return index >= 0 && index < _maxindex;
        }

        /// <summary> �ش� ������ ������ ���� �ִ��� ���� </summary>
        public bool HasCollection(int index)
        {
            return IsValidIndex(index) && _collection[index] != null;
        }

        /// <summary> �ش� ������ ���� ���� ���� </summary>
        public CollectionData GetCollectionData(int index)
        {
            if (!IsValidIndex(index)) return null;
            if (_collection[index] == null) return null;

            return _collection[index].Data;
        }

        /// <summary> �ش� ������ ���� �̸� ���� </summary>
        public string GetItemName(int index)
        {
            if (!IsValidIndex(index)) return "";
            if (_collection[index] == null) return "";

            return _collection[index].Data.Name;
        }

        public void ConnectUI(CollectionSystemUI collectionSystemUI)
        {
            _collectionSystemUI = collectionSystemUI;
            _collectionSystemUI.SetCollectionReference(this);
        }

        public void UpdateAccessibleStatesAll()
        {
            _collectionSystemUI.SetAccessibleSlotRange(50);
        }
    }
}

