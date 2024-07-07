using System.Collections;
using System.Collections.Generic;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minseok.Collection
{
    // 업적 도감에 제목과 내용을 적으면 됨
    public class Collection_Achievement : MonoBehaviour
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

        public void Achievement0()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
            {
                if (value.Index[0])
                {
                    _title.text = "시작";
                    _content.text = "테블릿을 열었습니다~~";
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

        public void Achievement1()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
            {
                if (value.Index[1])
                {
                    // TODO
                    _title.text = "출발";
                    _content.text = "P키를 눌러보기";
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

        public void Achievement2()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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

        public void Achievement3()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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

        public void Achievement4()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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

        public void Achievement5()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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

        public void Achievement6()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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

        public void Achievement7()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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

        public void Achievement8()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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

        public void Achievement9()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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

        public void Achievement10()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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

        public void Achievement11()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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

        public void Achievement12()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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

        public void Achievement13()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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

        public void Achievement14()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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

        public void Achievement15()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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

        public void Achievement16()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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

        public void Achievement17()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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

        public void Achievement18()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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

        public void Achievement19()
        {
            if (Managers.Data.collectionDic.TryGetValue("Achievement", out CollectionData.Collection value))
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
