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
        /// <summary> 아이템 수용 한도 </summary>
        public int Capacity { get; private set; }

        [SerializeField]
        private CollectionSystemUI _collectionSystemUI;    // 연결된 도감 UI

        /// <summary> 업데이트 할 인덱스 목록 </summary>
        private readonly HashSet<int> _indexSetForUpdate = new HashSet<int>();

        /// <summary> 도감 목록 </summary>
        [SerializeField]
        private Collection[] _collection;

        private int _maxindex = 50;


        private void Start()
        {
            _collectionSystemUI.SetCollectionReference(this);
        }

        /// <summary> 인덱스가 수용 범위 내에 있는지 검사 </summary>
        private bool IsValidIndex(int index)
        {
            return index >= 0 && index < _maxindex;
        }

        /// <summary> 앞에서부터 비어있는 슬롯 인덱스 탐색 </summary>
        private int FindEmptySlotIndex(int startIndex = 0)
        {
            for (int i = startIndex; i < Capacity; i++)
                if (_collection[i] == null)
                    return i;
            return -1;
        }

        /// <summary> 해당 슬롯이 도감을 갖고 있는지 여부 </summary>
        public bool HasCollection(int index)
        {
            return IsValidIndex(index) && _collection[index] != null;
        }

        /// <summary> 해당 슬롯의 도감 정보 리턴 </summary>
        public CollectionData GetCollectionData(int index)
        {
            if (!IsValidIndex(index)) return null;
            if (_collection[index] == null) return null;

            return _collection[index].Data;
        }

        /// <summary> 해당 슬롯의 아이템 정보 리턴 </summary>
        public CollectionData GetItemData(int index)
        {
            if (!IsValidIndex(index)) return null;
            if (_collection[index] == null) return null;

            return _collection[index].Data;
        }

        /// <summary> 해당 슬롯의 도감 이름 리턴 </summary>
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

