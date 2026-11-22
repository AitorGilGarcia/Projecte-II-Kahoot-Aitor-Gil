
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance { get; private set; }

	[Header("Sonidos")]
	[SerializeField] private AudioClip buttonClickSFX;

	[Header("Audio Sources")]
	[SerializeField] private AudioSource musicSource;
	[SerializeField] private AudioSource gameplayMusicSource;
	[SerializeField] private AudioSource sfxSource;

	private void Awake()
	{
		// Evitar tener varios AudioManagers
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		PlayMenuMusic();
	}

	// Música de los menús
	public void PlayMenuMusic()
	{
		if (musicSource.isPlaying){
			return;
		}
			
		gameplayMusicSource.Stop();

		musicSource.Play();
	}

	// Música de las preguntas
	public void PlayGameplayMusic()
	{
		if (gameplayMusicSource.isPlaying) {
			return;
		}
			
		musicSource.Stop();

		gameplayMusicSource.Play();
	}

	// Efectos de sonido
	public void PlaySFX(AudioClip clip)
	{
		if (clip == null){
			return;
		}	

		sfxSource.PlayOneShot(clip);
	}

	// Detener toda la música
	public void StopMusic()
	{
		musicSource.Stop();
		gameplayMusicSource.Stop();
	}

	public void PlayButtonClick()
	{
		if (buttonClickSFX != null)
			sfxSource.PlayOneShot(buttonClickSFX);
	}
}
