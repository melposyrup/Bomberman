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
	/// <para> 0 TitleScene		</para>
	/// <para> 1 SelectionScene	</para>
	/// <para> 2 GameScene		</para>
	/// <para> 3 ResultScene		</para>
	/// <para> 4 WinnerScene	</para>
	/// </summary>
	public void SceneChange(EScene sceneNum)
	{
		SceneManager.LoadScene((int)sceneNum);
	}

	/// <summary>
	/// <para> 0 TitleScene		</para>
	/// <para> 1 SelectionScene	</para>
	/// <para> 2 GameScene		</para>
	/// <para> 3 ResultScene		</para>
	/// <para> 4 WinnerScene	</para>
	/// </summary>
	public void SceneChange(int sceneNum)
	{
		SceneManager.LoadScene(sceneNum);
	}

}
