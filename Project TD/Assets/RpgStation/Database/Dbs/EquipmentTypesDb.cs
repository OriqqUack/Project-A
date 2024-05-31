﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Station
{
    [CreateAssetMenu]
    public class EquipmentTypesDb : DictGenericDatabase<EquipmentTypesModel>
    {
        [Serializable] public class LocalDictionary : SerializableDictionary<string, EquipmentTypesModel> {}
        [SerializeField] private LocalDictionary _db = new LocalDictionary();
  
        public override IDictionary<string, EquipmentTypesModel> Db
        {
            get => _db;
            set => _db.CopyFrom (value);
        }


        public override string[] ListEntryNames()
        {
            return _db.Select(entry => entry.Value.Name.GetValue()).ToArray();
        }
        
        public override string ObjectName()
        {
            return "Equipment Types";
        }
    }
}
