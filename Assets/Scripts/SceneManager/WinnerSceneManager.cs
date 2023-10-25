using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerSceneManager : MonoBehaviour
{
	public KeyCode InputNewGame = KeyCode.A;
	public KeyCode InputBackToSelection = KeyCode.B;

	

	private void Update()
	{
		if (Input.GetKeyDown(InputNewGame))
		{
			SceneChange(2);
		}
		else if (Input.GetKeyDown(InputBackToSelection))
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
