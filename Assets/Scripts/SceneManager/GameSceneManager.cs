using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
