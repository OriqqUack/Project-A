using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BuldingManager : MonoBehaviour
{
    public Vector3 Size { get; private set; }

    public void SetSize(Vector3 newSize)
    {
        Size = newSize;
        Debug.Log("�ǹ��� ũ�Ⱑ �����Ǿ����ϴ�. ũ��: " + Size);
    }

    // �ǹ��� ũ�⸦ �������� �޼���
    public Vector3 GetSize()
    {
        return Size;
    }


}
