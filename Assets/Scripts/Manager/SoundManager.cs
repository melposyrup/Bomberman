using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * 
 * EXAMPLE
 * SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
 * SoundManager.Instance.PlaySEPlayerAttack(SESoundData.SE.TakeOutBomb);
 * SoundManager.Instance.PlaySEItem(SESoundData.SE.Explosion);
 */

public class SoundManager : MonoBehaviour
{
	// bgm settings
	[SerializeField] AudioSource bgmAudioSource;
	[SerializeField] List<BGMSoundData> bgmSoundDatas;

	// se settings
	[SerializeField] AudioSource seAudioSource;
	[SerializeField] AudioSource seAudioSourcePlayerAttack;
	[SerializeField] AudioSource seAudioSourceItem;

	[SerializeField] List<SESoundData> seSoundDatas;


	public float masterVolume = 1;
	public float bgmMasterVolume = 0.25f;
	public float seMasterVolume = 1f;

	public static SoundManager Instance { get; private set; }

	private void Start()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else { Destroy(gameObject); }

		//SceneManager.sceneLoaded += OnSceneLoaded;
	}
	//private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	//{
	//	if (scene.name == "QuitScene") { Destroy(gameObject); }
	//}

	//private void OnDestroy()
	//{
	//	SceneManager.sceneLoaded -= OnSceneLoaded;
	//}


	public void PlayBGM(BGMSoundData.BGM bgm)
	{
		BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
		bgmAudioSource.clip = data.audioClip;
		bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
		bgmAudioSource.Play();
	}

	public void StopBGM(BGMSoundData.BGM bgm)
	{
		bgmAudioSource.Stop();
	}

	/// <summary>
	/// change BGM from bgmA to bgmB with fadingOut
	/// </summary>
	/// <param name="bgm1"></param>
	/// <param name="bgm2"></param>
	/// <param name="fadeDuration"></param>

	//EXAMPLE: SoundManager.Instance.SWitchBGM(BGMSoundData.BGM.Title, BGMSoundData.BGM.Selections,1.5f);
	public void SWitchBGM(BGMSoundData.BGM bgm1, BGMSoundData.BGM bgm2, float fadeDuration)
	{
		FadeOutBGMbySeconds(bgm1, fadeDuration);
		PlayBGMWithDelaybySeconds(bgm2, fadeDuration);

	}

	public void FadeOutBGMbySeconds(BGMSoundData.BGM bgm, float fadeDuration)
	{
		StartCoroutine(FadeOutBGM(bgm, fadeDuration));
	}

	IEnumerator FadeOutBGM(BGMSoundData.BGM bgm, float fadeDuration)
	{
		BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
		bgmAudioSource.clip = data.audioClip;

		float startVolume = bgmAudioSource.volume;

		for (float t = 0; t < fadeDuration; t += Time.deltaTime)
		{
			float normalizedTime = t / fadeDuration;
			bgmAudioSource.volume = Mathf.Lerp(startVolume, 0, normalizedTime);
			yield return null;
		}

		bgmAudioSource.volume = 0;
		bgmAudioSource.Stop();
	}

	public void PlayBGMWithDelaybySeconds(BGMSoundData.BGM bgm, float delay)
	{
		StartCoroutine(PlayBGMWithDelay(bgm, delay));
	}
	IEnumerator PlayBGMWithDelay(BGMSoundData.BGM bgm, float delay)
	{
		yield return new WaitForSeconds(delay);

		BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
		bgmAudioSource.clip = data.audioClip;
		bgmAudioSource.volume = 1;
		bgmAudioSource.Play();
	}

	/***************************************/

	public void PlaySE(SESoundData.SE se)
	{
		SESoundData data = seSoundDatas.Find(data => data.se == se);
		seAudioSource.clip = data.audioClip;
		seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
		//seAudioSource.Play();
		seAudioSource.PlayOneShot(data.audioClip);
	}

	public void PlaySEPlayerAttack(SESoundData.SE se)
	{
		SESoundData data = seSoundDatas.Find(data => data.se == se);
		seAudioSourcePlayerAttack.clip = data.audioClip;
		seAudioSourcePlayerAttack.volume = data.volume * seMasterVolume * masterVolume;
		seAudioSourcePlayerAttack.Play();
		//seAudioSourcePlayerAttack.PlayOneShot(data.audioClip);
	}

	public void PlaySEItem(SESoundData.SE se)
	{
		SESoundData data = seSoundDatas.Find(data => data.se == se);
		seAudioSourceItem.clip = data.audioClip;
		seAudioSourceItem.volume = data.volume * seMasterVolume * masterVolume;
		seAudioSourceItem.Play();
		//seAudioSourceBossAttack.PlayOneShot(data.audioClip);
	}

	/*
	public void PlaySEnoRepeat(SESoundData.SE se)
	{
		SESoundData data = seSoundDatas.Find(data => data.se == se);
		seAudioSource.clip = data.audioClip;
		if (!seAudioSource.isPlaying)
		{
			seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
			seAudioSource.PlayOneShot(data.audioClip);
		}
	}
	*/
}

//BGM tag setting
[System.Serializable]
public class BGMSoundData
{
	public enum BGM	// changed by scene
	{
		
		Title,
		Selections,
		PlayScene,
		ResultScene,
		WinnerScene,
		// add tags here
	}

	public BGM bgm;
	public AudioClip audioClip;
	[Range(0, 1)]
	public float volume = 1f;
}

//SE tag setting
[System.Serializable]
public class SESoundData
{
	public enum SE	// sound effect
	{
		//MenuSE		
		SelectKey,
		EnterKey,
		GetInStage,
		ReadyGo,
		GameStartSE,

		//PlayerActionSE
		TakeOutBomb,
		ThrowBomb,
		KickBomb,
		GetItem,
		TakeOutRedBomb,
		Stunned,//ö›Ω~
		PlayerDie,

		//ItemSE
		Explosion,
		BombExpansionToMAX,
		ItemInstantiate,

		//ResultSE
		GameOver,
		ResultSE,
		ShowingStar,


		// add tags here
	}

	public SE se;
	public AudioClip audioClip;
	[Range(0, 1)]
	public float volume = 1f;
}
