using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Minseok.Collection;

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
        if (FindCollection.FCAchievement0)
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

    public void Achievement1()
    {
        if (FindCollection.FCAchievement1)
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

    public void Achievement2()
    {
        if (FindCollection.FCAchievement2)
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

    public void Achievement3()
    {
        if (FindCollection.FCAchievement3)
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

    public void Achievement4()
    {
        if (FindCollection.FCAchievement4)
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

    public void Achievement5()
    {
        if (FindCollection.FCAchievement5)
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

    public void Achievement6()
    {
        if (FindCollection.FCAchievement6)
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

    public void Achievement7()
    {
        if (FindCollection.FCAchievement7)
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

    public void Achievement8()
    {
        if (FindCollection.FCAchievement8)
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

    public void Achievement9()
    {
        if (FindCollection.FCAchievement9)
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

    public void Achievement10()
    {
        if (FindCollection.FCAchievement10)
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

    public void Achievement11()
    {
        if (FindCollection.FCAchievement11)
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

    public void Achievement12()
    {
        if (FindCollection.FCAchievement12)
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

    public void Achievement13()
    {
        if (FindCollection.FCAchievement13)
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

    public void Achievement14()
    {
        if (FindCollection.FCAchievement14)
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

    public void Achievement15()
    {
        if (FindCollection.FCAchievement15)
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

    public void Achievement16()
    {
        if (FindCollection.FCAchievement16)
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

    public void Achievement17()
    {
        if (FindCollection.FCAchievement17)
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

    public void Achievement18()
    {
        if (FindCollection.FCAchievement18)
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

    public void Achievement19()
    {
        if (FindCollection.FCAchievement19)
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
