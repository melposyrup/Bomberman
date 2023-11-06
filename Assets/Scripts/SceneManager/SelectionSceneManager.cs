using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSceneManager : SceneManagerBase
{
	public KeyCode InputLeftArrow= KeyCode.LeftArrow;
	public KeyCode InputRightArrow = KeyCode.RightArrow;
	public KeyCode InputSelect = KeyCode.Return;

	// ���݂̑I�����
	private int _selectScene = 0;
	// ���ݑI��ł���ꏊ
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
			// �v���C���[����I��(�������邩�킩��Ȃ�)
			case 0:
				_selectScene++;
					break;
			//player model selection
			// �v���C���[�L������I��
			case 1:
				SelectPlayerModel();
				break;
			// change scene
			// �o�g���V�[���ɕύX
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
