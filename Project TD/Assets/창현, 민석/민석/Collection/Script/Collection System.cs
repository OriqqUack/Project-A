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
        /// <summary> ������ ���� �ѵ� </summary>
        public int Capacity { get; private set; }

        [SerializeField]
        private CollectionSystemUI _collectionSystemUI;    // ����� ���� UI

        /// <summary> ������Ʈ �� �ε��� ��� </summary>
        private readonly HashSet<int> _indexSetForUpdate = new HashSet<int>();

        /// <summary> ���� ��� </summary>
        [SerializeField]
        private Collection[] _collection;

        private int _maxindex = 50;


        private void Start()
        {
            _collectionSystemUI.SetCollectionReference(this);
        }

        /// <summary> �ε����� ���� ���� ���� �ִ��� �˻� </summary>
        private bool IsValidIndex(int index)
        {
            return index >= 0 && index < _maxindex;
        }

        /// <summary> �տ������� ����ִ� ���� �ε��� Ž�� </summary>
        private int FindEmptySlotIndex(int startIndex = 0)
        {
            for (int i = startIndex; i < Capacity; i++)
                if (_collection[i] == null)
                    return i;
            return -1;
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

        /// <summary> �ش� ������ ������ ���� ���� </summary>
        public CollectionData GetItemData(int index)
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
    }
}

