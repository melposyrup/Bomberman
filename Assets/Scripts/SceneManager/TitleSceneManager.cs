using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : SceneManagerBase
{
	public KeyCode InputNextScene = KeyCode.Return;

	private void Update()
	{
		if (Input.GetKeyDown(InputNextScene))
		{
			SceneChange(1);
		}
	}

}
