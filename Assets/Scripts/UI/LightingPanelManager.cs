using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LightingPanelManager : MonoBehaviour
{
	[SerializeField] private GameObject LightingPanel;
	[SerializeField] private int rows = 3;
	[SerializeField] private int cols = 5;
	[SerializeField] private float rowSpacing = 4.5f;
	[SerializeField] private float colSpacing = 4.5f;
	private Camera mainCamera;

	private BombLightingPanel[,] panels;

	private LightingPattern[] bombLightingPatterns;
	private LightingPattern[] explosionLightingPatterns;

	void Start()
	{
		mainCamera = Camera.main;
		panels = new BombLightingPanel[rows, cols];
		CreateGridCentered();
		AdjustCameraToFitGrid();
		InitializePattern();

		StartCoroutine(LightingSequence());
	}


	/// <summary>
	/// lightingPanel initialization
	/// </summary>
	private void CreateGridCentered()
	{
		Vector2 centerOffset = new Vector2((
			cols - 1) * colSpacing / 2,
			(rows - 1) * rowSpacing / 2);

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				// Adjust position based on the center offset
				Vector3 position = new Vector3(
					transform.position.x + (j * colSpacing) - centerOffset.x,
					transform.position.y + (i * rowSpacing) - centerOffset.y,
					0f + transform.position.z
				);

				GameObject panelInstance = Instantiate(
					LightingPanel, position, Quaternion.identity, transform);
				BombLightingPanel panelComp =
					panelInstance.GetComponent<BombLightingPanel>();

				if (panelComp != null) { panels[i, j] = panelComp; }
			}
		}
	}

	private void AdjustCameraToFitGrid()
	{
		float gridWidth = colSpacing * (cols - 1);
		float gridHeight = rowSpacing * (rows - 1);

		float requiredOrthoSizeVertical = gridHeight / 2f + rowSpacing;


		float requiredOrthoSizeHorizontal = gridWidth / 2f + colSpacing;
		requiredOrthoSizeHorizontal /= mainCamera.aspect;

		float setCameraCloser = 0.8f;
		mainCamera.orthographicSize = Mathf.Max(
			requiredOrthoSizeVertical,
			requiredOrthoSizeHorizontal) * setCameraCloser;
	}

	void OnScreenSizeChanged(int width, int height)
	{
		AdjustCameraToFitGrid();
	}

	/// <summary>
	/// methods for setting lighting
	/// </summary>
	public void SetBombLighting(int row, int col, bool onOff)
	{
		if (IsIndexValid(row, col) && panels[row, col] != null)
		{
			panels[row, col].SetBombLighting(onOff);
		}
	}

	public void SetExplosionLighting(int row, int col, bool onOff)
	{
		if (IsIndexValid(row, col) && panels[row, col] != null)
		{
			panels[row, col].SetExplosionLighting(onOff);
		}
	}

	private bool IsIndexValid(int row, int col)
	{
		return row >= 0 && row < rows && col >= 0 && col < cols;
	}

	/// <summary>
	/// arrays and methods for lighting patterns
	/// </summary>
	private void InitializePattern()
	{
		bombLightingPatterns = new LightingPattern[]
		{
			new LightingPattern(new int[,] {{1, 1, 0, 1, 1}, {1, 1, 1, 0, 1}, {0, 1, 1, 1, 0} }),
			new LightingPattern(new int[,] {{0, 0, 1, 1, 0}, {1, 0, 0, 1, 1}, {1, 1, 0, 0, 1} }),
			new LightingPattern(new int[,] {{0, 0, 1, 0, 0}, {0, 0, 0, 1, 0}, {1, 0, 0, 0, 1} }),
			new LightingPattern(new int[,] {{1, 1, 0, 0, 1}, {0, 1, 1, 0, 0}, {0, 0, 1, 1, 0} })
		};

		explosionLightingPatterns = new LightingPattern[]
		{
			new LightingPattern(new int[,] {{0, 1, 0, 0, 0},{0, 0, 1, 0, 0}, {0, 0, 0, 1, 0}}),
			new LightingPattern(new int[,] {{1, 0, 0, 0, 1},{0, 1, 0, 0, 0}, {0, 0, 1, 0, 0}}),
			new LightingPattern(new int[,] {{0, 0, 0, 1, 0},{1, 0, 0, 0, 1}, {0, 1, 0, 0, 0}}),
			new LightingPattern(new int[,] {{0, 0, 1, 0, 0},{0, 0, 0, 1, 0}, {1, 0, 0, 0, 1}})
		};
	}



	/// <summary>
	/// loop 4 pattern of lighting animations for bombImage and explosionImage
	/// </summary>
	private IEnumerator LightingSequence()
	{
		// Start with both off
		SetBombLightingOff();
		SetExplosionLightingOff();

		// Start bomb lighting immediately
		StartCoroutine(CyclePatterns
			(bombLightingPatterns, SetBombPattern, SetBombLightingOff));

		// Delay explosion lighting start by 1 second
		yield return new WaitForSeconds(1f);
		StartCoroutine(CyclePatterns
			(explosionLightingPatterns, SetExplosionPattern, SetExplosionLightingOff));
	}

	private IEnumerator CyclePatterns(LightingPattern[] patterns,
		System.Action<int> setPatternAction,
		System.Action setAllPanelsOff)
	{
		float minOffDuration = 0.2f;
		float maxOffDuration = 0.5f;

		float patternDuration = 1.2f;

		while (true) // Loop indefinitely
		{
			for (int i = 0; i < patterns.Length; i++)
			{
				// Turn on the pattern
				setPatternAction(i);
				// Randomly determine off duration within the pattern
				float randomOffDuration = Random.Range(minOffDuration, maxOffDuration);

				// Wait for a duration before turning off
				yield return new WaitForSeconds(patternDuration - randomOffDuration);

				// Turn off all panels briefly
				setAllPanelsOff();

				// Wait for the off duration
				yield return new WaitForSeconds(randomOffDuration);
			}
		}
	}

	/// <summary>
	/// methods for System.Action setAllPanelsOf
	/// </summary>
	private void SetBombLightingOff()
	{
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				panels[i, j].SetBombLighting(false);
			}
		}

	}
	private void SetExplosionLightingOff()
	{
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				panels[i, j].SetExplosionLighting(false);
			}
		}

	}

	/// <summary>
	/// methods for System.Action setPatternAction
	/// </summary>
	/// <param name="patternIndex"></param>
	public void SetBombPattern(int patternIndex)
	{
		if (patternIndex >= 0 && patternIndex < bombLightingPatterns.Length)
		{
			bombLightingPatterns[patternIndex].ApplyPatternToBomb(panels);
		}
	}

	public void SetExplosionPattern(int patternIndex)
	{
		if (patternIndex >= 0 && patternIndex < explosionLightingPatterns.Length)
		{
			explosionLightingPatterns[patternIndex].ApplyPatternToExplosion(panels);
		}
	}
}


public class LightingPattern
{
	private int[,] pattern;

	public LightingPattern(int[,] pattern)
	{
		this.pattern = pattern;
	}

	public void ApplyPatternToBomb(BombLightingPanel[,] panels)
	{
		for (int i = 0; i < panels.GetLength(0); i++)
		{
			for (int j = 0; j < panels.GetLength(1); j++)
			{
				bool state = this.pattern[i, j] == 1;
				panels[i, j].SetBombLighting(state);
			}
		}
	}

	public void ApplyPatternToExplosion(BombLightingPanel[,] panels)
	{
		for (int i = 0; i < panels.GetLength(0); i++)
		{
			for (int j = 0; j < panels.GetLength(1); j++)
			{
				bool state = this.pattern[i, j] == 1;
				panels[i, j].SetExplosionLighting(state);
			}
		}
	}
}