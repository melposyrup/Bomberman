using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class TitleSceneManager : MonoBehaviour
{
	public KeyCode InputNextScene = KeyCode.Return;

	private void Update()
	{
		if (Input.GetKeyDown(InputNextScene))
		{
			SceneChange(1);
		}
	}

	/// <summary>
	/// TitleScene		0
	/// SelectionScene	1
	/// GameScene			2
	/// ResultScene		3
	/// WinnerScene		4
	/// </summary>
	public void SceneChange(int sceneNum)
	{
		SceneManager.LoadScene(sceneNum);
	}
}
