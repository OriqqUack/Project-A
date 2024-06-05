using Rito.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Minseok.CollectionSystem
{
    public abstract class Collection
    {
        public CollectionData Data { get; private set; }
        public Collection(CollectionData data) => Data = data;
    }
}

