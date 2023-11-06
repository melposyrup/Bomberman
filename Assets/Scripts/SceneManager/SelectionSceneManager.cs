using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSceneManager : SceneManagerBase
{
	public KeyCode InputLeftArrow= KeyCode.LeftArrow;
	public KeyCode InputRightArrow = KeyCode.RightArrow;
	public KeyCode InputSelect = KeyCode.Return;

	// 現在の選択画面
	private int _selectScene = 0;
	// 現在選んでいる場所
	public int SelectNum = 0;

	[SerializeField] private FadingImage _fadingImage;

	private void Start()
	{
		if (_fadingImage) { _fadingImage.StartFadingOut(); }
	}

	private void Update()
	{
		switch (_selectScene)
		{
			//select number of players
			// プレイヤー数を選択(実装するかわからない)
			case 0:
				_selectScene++;
					break;
			//player model selection
			// プレイヤーキャラを選択
			case 1:
				SelectPlayerModel();
				break;
			// change scene
			// バトルシーンに変更
			case 2:
				// if all the players are selected, go to game scene
				{
					SceneChange(SceneManagerBase.EScene.GameScene);
				}
				break;
		}
	}

	// select players
	void SelectPlayerModel()
    {		
		// Button Input
		if (Input.GetKeyDown(InputLeftArrow))
		{
			SelectNum--;
		}
		else if (Input.GetKeyDown(InputRightArrow))
		{
			SelectNum++;
		}
		else if (Input.GetKeyDown(InputSelect))
		{
			_selectScene++;
		}

		
	}

}
