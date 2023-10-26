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
		//// オブジェクトからTextコンポーネントを取得
		//_goText = GOMessage.GetComponent<Image>();
		//_readyText = READYMessage.GetComponent<Image>();
		READYMessage.SetActive(true);
		Invoke(nameof(ChangeMessage), 1.0f);
		Invoke(nameof(DeleteMessage), 2.0f);
	}

	// メッセージをREADYからGOに変更
	void ChangeMessage()
	{
		READYMessage.SetActive(false);
		GOMessage.SetActive(true);
	}

	// メッセージを削除
	void DeleteMessage()
	{
		Destroy(GOMessage);
		Destroy(READYMessage);
	}
}
