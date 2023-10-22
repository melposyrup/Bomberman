using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager: MonoBehaviour
{
    // �^�C�}�[�̃I�u�W�F�N�g�擾�p
    public GameObject Timer;
    Text _timerText;

    // ����
    int _minutes = 2;
    float _seconds = 0;

    // ������
    void Start()
    {
        // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
        _timerText = Timer.GetComponent<Text>();
    }

    // �X�V
    void Update()
    {
        // 0�ȏ�Ȃ�b�����}�C�i�X����
        if (_seconds > 0)
        {
            _seconds -= Time.deltaTime;
        }
        // 0�ȉ��ɂȂ�Ε������}�C�i�X���ă��Z�b�g
        else
        {
            _seconds = 60;
            _minutes--;
        }

        // �e�L�X�g�̕\�����X�V����
        _timerText.text = _minutes.ToString("0") + ":" + ((int)_seconds).ToString("00");
    }
}
