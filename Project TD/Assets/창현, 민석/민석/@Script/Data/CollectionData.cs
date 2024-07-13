using System;
using System.IO;
using Minseok.Collection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Minseok.Collection
{
    public class CollectionData : MonoBehaviour
    {
        [Serializable]
        public class Collection
        {
            public bool[] Index = new bool[20];
        }

        [Serializable]
        public class CollectionList
        {
            public Dictionary<string, Collection> _collection;
        }

        private void Awake()
        {
            Collection Achievement = new Collection();
            Collection Monster = new Collection();
            Collection Weapon = new Collection();

            for (int i = 0; i < (int)Define.Collection.Achievement; i++)
            {
                Achievement.Index[i] = false;
                Monster.Index[i] = false;
                Weapon.Index[i] = false;
            }

            Managers.Data.collectionDic["Achievement"] = Achievement;
            Managers.Data.collectionDic["Monster"] = Monster;
            Managers.Data.collectionDic["Weapon"] = Weapon;

            CollectionList collection = new CollectionList();
            collection._collection = Managers.Data.collectionDic;

            LoadCollection();
        }

        public void SaveCollection()
        {
            //ToJson �κ�
            string jsonData = DictionaryJsonUtility.ToJson(Managers.Data.collectionDic, true);

            string path = Application.dataPath + "/Data";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.WriteAllText(path + "/CollectionData.txt", jsonData);
            Debug.Log("�����ϱ� ����!!");
        }

        public void LoadCollection()
        {
            string path = Application.dataPath + "/Data";
            //FromJson �κ�
            string fromJsonData = File.ReadAllText(path + "/CollectionData.txt");


            //CollectionList CollectionFromJson = new CollectionList();
            Managers.Data.collectionDic = DictionaryJsonUtility.FromJson<string, Collection>(fromJsonData);
            print(Managers.Data.collectionDic);
            Debug.Log("�ҷ����� ����!!");
        }

    }
}

