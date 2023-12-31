using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
	// タイマーのオブジェクト取得用
	//public GameObject Timer;
	Text _timerText;

	// 時間
	int _minutes = 2;
	float _seconds = 0;

	[SerializeField] private bool _isTimerActive = false;

	// 初期化
	void Start()
	{
		// オブジェクトからTextコンポーネントを取得
		_timerText = this.GetComponent<Text>();
	}

	// 更新
	void Update()
	{
		if (_isTimerActive)
		{
			// 0以上なら秒数をマイナスする
			if (_seconds > 0)
			{
				_seconds -= Time.deltaTime;
			}
			// 0以下になれば分数をマイナスしてリセット
			else
			{
				_seconds = 60;
				_minutes--;
			}
		}
		// テキストの表示を更新する
		_timerText.text = _minutes.ToString("0") + ":" + ((int)_seconds).ToString("00");
	}

	// Function to start the timer
	public void StartTimer()
	{
		_isTimerActive = true;
		//Debug.Log("_isTimerActive = true;");

	}

	// Function to stop the timer
	public void StopTimer()
	{
		_isTimerActive = false;
	}

	public int GetMinutes()
	{
		return _minutes;
	}

	public float GetSeconds()
	{
		return _seconds;
	}

	public bool TimeIsUp()
	{
		return _minutes <= 0 && _seconds <= 0;
	}

	public void SetTimerToZero()
	{
		_minutes = 0;
		_seconds = 0;
	}


}
