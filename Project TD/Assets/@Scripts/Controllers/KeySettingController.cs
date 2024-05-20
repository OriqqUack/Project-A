using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class KeySettingController : MonoBehaviour
{
    private KeyCode mOriginKeyCode;
    [SerializeField] private string mKeyBindingName;

    //Ű ���� ��ư
    [SerializeField] private Image mKeyButtonImage; //���� �Ҵ�� Ű�� �� Ű�� ������ �� �ְ� �ϴ� ��ư�� �̹���
    private Coroutine mKeyButtonColorCor; //Ű ���� ��ư�� ���� ������ �����ϴ� �ڷ�ƾ�� ��� ����

    //��ư �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI mKeyButtonText; //��ư�� ���� �ڽ��� �ؽ�Ʈ�ʵ�

    private void OnEnable()
    {
        mOriginKeyCode = Managers.Key.GetKeyCode(mKeyBindingName);
        mKeyButtonText.text = ((char)mOriginKeyCode).ToString().ToUpper();
    }

    public void BTN_ModifyKey()
    {
        mKeyButtonText.text = "< >";

        StartCoroutine(CorAssignKey());
    }

    private IEnumerator CorAssignKey()
    {
        while (true)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(kcode))
                    {
                        // ������ �ڷ�ƾ ����        
                        if (mKeyButtonColorCor != null) { StopCoroutine(mKeyButtonColorCor); }

                        // Ű ������ �� �� �ִ°��?
                        if (Managers.Key.CheckKey(kcode, mOriginKeyCode))
                        {
                            // Ű ����
                            Managers.Key.AssignKey(kcode, mKeyBindingName);
                            mOriginKeyCode = kcode;

                            // Ű ���̺��� ����
                            mKeyButtonText.text = ((char)kcode).ToString().ToUpper();

                            // ������� ���� �Ϸ���� ����
                            mKeyButtonColorCor = StartCoroutine(CorChangeButtonColor(Color.green));
                        }
                        else
                        {
                            // Ű ���̺��� ����
                            mKeyButtonText.text = ((char)mOriginKeyCode).ToString().ToUpper();

                            // ���������� ���� �Ϸ���� ����
                            mKeyButtonColorCor = StartCoroutine(CorChangeButtonColor(Color.red));
                        }
                    }
                }

                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator CorChangeButtonColor(Color targetColor, float colorSpeed = 2.0f)
    {
        float progress = 0;

        //targetColor�� ����
        while (true)
        {
            mKeyButtonImage.color = Color.Lerp(mKeyButtonImage.color, targetColor, progress);
            progress += colorSpeed * Time.deltaTime;

            //progress�� 1�̸� > ���� �Ϸ�
            if (progress > 1)
            {
                progress = 0;

                //targetColor���� �ٽ� ���ƿ���
                while (true)
                {
                    mKeyButtonImage.color = Color.Lerp(mKeyButtonImage.color, Color.white, progress);
                    progress += colorSpeed * Time.deltaTime;

                    //���� ��ȯ �Ϸ�
                    if (progress > 1)
                    {
                        yield break;
                    }

                    yield return null;
                }
            }

            yield return null;
        }
    }
}