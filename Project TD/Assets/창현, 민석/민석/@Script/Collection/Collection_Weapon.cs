using Minseok.Collection;
using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Minseok.Collection
{
    // 무기 도감에 제목과 내용을 적으면 됨
    public class Collection_Weapon : MonoBehaviour
    {
        [SerializeField]
        public GameObject _tooltip;

        [SerializeField]
        public TextMeshProUGUI _title;

        [SerializeField]
        public TextMeshProUGUI _content;

        private void Update()
        {
            if (Input.GetKey(KeyCode.D))
                _tooltip.SetActive(false);
        }

        public void Weapon0()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[0])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon1()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[1])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon2()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[2])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon3()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[3])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon4()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[4])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon5()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[5])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon6()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[6])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon7()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[7])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon8()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[8])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon9()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[9])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon10()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[10])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon11()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[11])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon12()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[12])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon13()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[13])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon14()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[14])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon15()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[15])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon16()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[16])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon17()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[17])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon18()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[18])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }

        public void Weapon19()
        {
            if (Managers.Data.collectionDic.TryGetValue("Weapon", out CollectionData.Collection value))
            {
                if (value.Index[19])
                {
                    // TODO
                    _tooltip.SetActive(true);
                }
                else
                {
                    _title.text = "???";
                    _content.text = "???";
                    _tooltip.SetActive(true);
                }
            }
        }
    }
}

