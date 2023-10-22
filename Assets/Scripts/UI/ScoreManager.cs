using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // プレイヤースコアのオブジェクト取得用
    public GameObject Score;
    Text _scoreText;

    int _playerscore1P;
    int _playerscore2P;


    void Start()
    {
        // オブジェクトからTextコンポーネントを取得
        _scoreText = Score.GetComponent<Text>();
        // プレイヤー関連のスクリプトから勝利数を取得

        // テキストの表示を更新する
        _scoreText.text = _playerscore1P.ToString("0") + "           " + _playerscore2P.ToString("0");
    }
}
