using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // �v���C���[�X�R�A�̃I�u�W�F�N�g�擾�p
    public GameObject Score;
    Text _scoreText;

    int _playerscore1P;
    int _playerscore2P;


    void Start()
    {
        // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
        _scoreText = Score.GetComponent<Text>();
        // �v���C���[�֘A�̃X�N���v�g���珟�������擾

        // �e�L�X�g�̕\�����X�V����
        _scoreText.text = _playerscore1P.ToString("0") + "           " + _playerscore2P.ToString("0");
    }
}
