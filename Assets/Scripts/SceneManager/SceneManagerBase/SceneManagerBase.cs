using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerBase : MonoBehaviour
{
	public enum EScene
	{
		TitleScene = 0,
		SelectionScene = 1,
		GameScene = 2,
		ResultScene = 3,
		WinnerScene = 4
	}

	/// <summary>
	/// TitleScene		0 
	/// SelectionScene	1
	/// GameScene			2
	/// ResultScene		3
	/// WinnerScene		4
	/// </summary>
	public void SceneChange(EScene sceneNum)
	{
		//EventManager.Instance.DestroySingleton();
		SceneManager.LoadScene((int)sceneNum);
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
		//EventManager.Instance.DestroySingleton();
		SceneManager.LoadScene(sceneNum);
	}

}
