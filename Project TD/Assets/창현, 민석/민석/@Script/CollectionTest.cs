using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionTest : MonoBehaviour
{
    // ���Ͱ� ī�޶� ���̸� ȣ��Ǵ� �޼���
    private void OnBecameVisible()
    {
        ReactToVisibility(true);
    }

    // ���Ͱ� ī�޶� ������ ������ ȣ��Ǵ� �޼���
    private void OnBecameInvisible()
    {
        ReactToVisibility(false);
    }

    // ������ ����/�� ���ӿ� ���� �����ϴ� �޼���
    private void ReactToVisibility(bool isVisible)
    {
        if (isVisible)
        {
            // ���Ͱ� ī�޶� ���� ���� ����
            Define._Monster0 = true;
            Debug.Log("���Ͱ� ī�޶� ���Դϴ�!");
            // ���⿡ ������ ���� �ڵ带 �߰��ϼ���.
            // ��: ���� ��� Ȱ��ȭ, �Ҹ� ��� ��
        }
    }
}
