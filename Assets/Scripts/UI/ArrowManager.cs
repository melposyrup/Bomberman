using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
	public GameObject SelMng;
	SelectionSceneManager _selNum;

	// Start is called before the first frame update
	void Start()
    {
		SelMng = GameObject.Find("SelectionManager");
		_selNum = SelMng.GetComponent<SelectionSceneManager>();
	}

    // Update is called once per frame
    void Update()
    {
		// Move Arrow
		if (_selNum.SelectNum == 0)
		{
			transform.localPosition = new Vector3(-7.5f, 0, 0);
		}
		else if (_selNum.SelectNum == 1)
		{
			transform.localPosition = new Vector3(-2.5f, 0, 0);
		}
		else if (_selNum.SelectNum == 2)
		{
			transform.localPosition = new Vector3(2.5f, 0, 0);
		}
		else if (_selNum.SelectNum == 3)
		{
			transform.localPosition = new Vector3(7.5f, 0, 0);
		}
		else if (_selNum.SelectNum < 0)
		{
			_selNum.SelectNum = 3;
		}
		else if (_selNum.SelectNum > 3)
		{
			_selNum.SelectNum = 0;
		}

	}
}
