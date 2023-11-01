using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartMessageManager : MonoBehaviour
{
	public GameObject GOMessage;
	public GameObject READYMessage;

	public void StartMessage()
	{
		StartCoroutine(ShowStartMessages());
	}

	IEnumerator ShowStartMessages()
	{
		yield return new WaitForSeconds(1.0f);

		READYMessage.SetActive(true);
		GOMessage.SetActive(false);

		yield return new WaitForSeconds(1.0f);

		// メッセージをREADYからGOに変更
		READYMessage.SetActive(false);
		GOMessage.SetActive(true);

		yield return new WaitForSeconds(1.0f);


		READYMessage.SetActive(false);
		GOMessage.SetActive(false);
	}
}
