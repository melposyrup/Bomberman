using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionSceneManager : MonoBehaviour
{
	public KeyCode InputLeftArrow= KeyCode.LeftArrow;
	public KeyCode InputRightArrow = KeyCode.RightArrow;
	public KeyCode InputSelect = KeyCode.Return;

	private void Update()
	{
		//player model selection

		// all the players are selected, go to game scene
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
