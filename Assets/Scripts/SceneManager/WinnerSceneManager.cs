using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerSceneManager : SceneManagerBase
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


}
