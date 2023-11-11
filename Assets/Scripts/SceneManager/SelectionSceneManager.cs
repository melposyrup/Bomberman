using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーカラー
public static class GlobalVariables
{
	// プレイヤーカラー
	public static int SelectColor1P = 0;
	public static int SelectColor2P = 0;
}

public class SelectionSceneManager : SceneManagerBase
{
	// 現在の選択画面
	private int _selectScene = 0;
	// 現在選んでいる場所
	public int SelectNum = 0;
	// 選択中の人
	private int _selectPlayer = 0;

	[SerializeField] private FadingImage _fadingImage;

	private void Start()
	{
		if (_fadingImage) { _fadingImage.StartFadingOut(); }
		SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Selections);
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
					GameSettings.Instance.InitializeAll();
					GameSettings.Instance.SetMaterial(1, GlobalVariables.SelectColor1P);
					GameSettings.Instance.SetMaterial(2, GlobalVariables.SelectColor2P);
	
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
				if (Input.GetButtonDown("LeftArrow"))
				{
					SoundManager.Instance.PlaySE(SESoundData.SE.SelectKey);
					SelectNum--;
				}
				else if (Input.GetButtonDown("RightArrow"))
				{
					SoundManager.Instance.PlaySE(SESoundData.SE.SelectKey);
					SelectNum++;
				}
				else if (Input.GetButtonDown("InputNextScene"))
				{
					SoundManager.Instance.PlaySE(SESoundData.SE.EnterKey);
					GlobalVariables.SelectColor1P = SelectNum;
					// 選択場所をリセット
					if (GlobalVariables.SelectColor1P == 0)
					{
						SelectNum = 1;
					}
					else
					{
						SelectNum = 0;
					}
					_selectPlayer++;
				}
				else if (SelectNum < 0)
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
				if (Input.GetButtonDown("LeftArrow"))
				{
					SoundManager.Instance.PlaySE(SESoundData.SE.SelectKey);
					SelectNum--;
					if (SelectNum == GlobalVariables.SelectColor1P)
					{
						SelectNum--;
					}
				}
				else if (Input.GetButtonDown("RightArrow"))
				{
					SoundManager.Instance.PlaySE(SESoundData.SE.SelectKey);
					SelectNum++;
					if (SelectNum == GlobalVariables.SelectColor1P)
					{
						SelectNum++;
					}
				}
				else if (Input.GetButtonDown("InputNextScene"))
				{
					SoundManager.Instance.PlaySE(SESoundData.SE.EnterKey);
					GlobalVariables.SelectColor2P = SelectNum;
					// シーンを移動
					_selectScene++;
				}
				else if (SelectNum < 0)
				{
					if (GlobalVariables.SelectColor1P == 3)
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
					if (GlobalVariables.SelectColor1P == 0)
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
