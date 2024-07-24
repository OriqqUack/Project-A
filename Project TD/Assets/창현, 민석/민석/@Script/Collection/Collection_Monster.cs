using Minseok.Collection;
using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Minseok.Collection
{
    // 몬스터 도감에 제목과 내용을 적으면 됨
    public class Collection_Monster : MonoBehaviour
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

        public void Monster0()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
            {
                if (value.Index[0])
                {
                    // TODO
                    _title.text = "테스트";
                    _content.text = "테스트0";
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

        public void Monster1()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster2()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster3()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster4()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster5()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster6()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster7()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster8()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster9()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster10()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster11()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster12()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster13()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster14()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster15()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster16()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster17()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster18()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

        public void Monster19()
        {
            if (Managers.Data.collectionDic.TryGetValue("Monster", out CollectionData.Collection value))
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

