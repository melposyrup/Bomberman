using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMessageManager : MonoBehaviour
{
    public GameObject GOMessage;
    public GameObject READYMessage;
 
    void Start()
    {
        //// �I�u�W�F�N�g����Text�R���|�[�l���g���擾
        //_goText = GOMessage.GetComponent<Image>();
        //_readyText = READYMessage.GetComponent<Image>();
        READYMessage.SetActive(true);
        Invoke(nameof(ChangeMessage), 1.0f);
        Invoke(nameof(DeleteMessage), 2.0f);
    }

    // ���b�Z�[�W��READY����GO�ɕύX
    void ChangeMessage()
    {
        READYMessage.SetActive(false);
        GOMessage.SetActive(true);
    }

    // ���b�Z�[�W���폜
    void DeleteMessage()
    {
        Destroy(GOMessage);
        Destroy(READYMessage);
    }
}
