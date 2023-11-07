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
	// 選択中の人
	private int _selectPlayer = 0;
	// プレイヤーカラー
	public int SelectColor1P = 0;
	public int SelectColor2P = 0;

	[SerializeField] private FadingImage _fadingImage;

	private void Start()
	{
		if (_fadingImage) { _fadingImage.StartFadingOut(); }
	}

	private void Update()
	{
		switch (_selectScene)
		{
			//player model selection
			// プレイヤーキャラを選択
			case 0:
				SelectPlayerModel();
				break;
			// change scene
			// バトルシーンに変更
			case 1:
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
		switch (_selectPlayer)
		{
			//player model selection
			// 1P
			case 0:
				// プレイヤー１のキャラ選択
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
					SelectColor1P = SelectNum;
					// 選択場所をリセット
					if (SelectColor1P == 0)
					{
						SelectNum = 1;
					}
					else
					{
						SelectNum = 0;
					}
					_selectPlayer++;

				}
				else if(SelectNum < 0)
				{
					SelectNum = 3;
				}
				else if (SelectNum > 3)
				{
					SelectNum = 0;
				}
				break;
			// 2P
			case 1:
				// プレイヤー２のキャラ選択
				if (Input.GetKeyDown(InputLeftArrow))
				{
					SelectNum--;
					if (SelectNum == SelectColor1P) 
                    {
						SelectNum--;
                    }
				}
				else if (Input.GetKeyDown(InputRightArrow))
				{
					SelectNum++;
					if (SelectNum == SelectColor1P)
					{
						SelectNum++;
					}
				}
				else if (Input.GetKeyDown(InputSelect))
				{
					SelectColor2P = SelectNum;
					// シーンを移動
					_selectScene++;
				}
				else if (SelectNum < 0)
				{
					if(SelectColor1P == 3)
					{
						SelectNum = 2;
					}
					else
                    {
						SelectNum = 3;
					}
				}
				else if (SelectNum > 3)
				{
					if (SelectColor1P == 0)
					{
						SelectNum = 1;
					}
					else
					{
						SelectNum = 0;
					}
				}
				break;

		}
	}
}
