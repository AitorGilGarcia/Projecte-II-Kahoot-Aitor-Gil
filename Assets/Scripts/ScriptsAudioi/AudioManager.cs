using UnityEngine.Audio;
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

	[Header("Audio Mixer")]
	[SerializeField] private AudioMixer audioMixer;

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
		LoadAudioSettings(); // cargamos las los ajustes para que se aplique el volumen y no cuando abra el panel de ajustes
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

	public void PlayButtonClick() // metodo para que suene el sonido del clic de los botones
	{
		if (buttonClickSFX != null)
			sfxSource.PlayOneShot(buttonClickSFX);
	}

	private void LoadAudioSettings() // metodo que pilla el volumen de la musica, con la intencion de que cuando ejecute el juego ya se aplique y
									 // no tenga que abrir el panel de ajustes
	{
		float music = PlayerPrefs.GetFloat("MusicVolume", 1f);
		float gameplay = PlayerPrefs.GetFloat("GameplayMusicVolume", 1f);
		float sfx = PlayerPrefs.GetFloat("SFXVolume", 1f);

		SetMixerVolume("MusicVolume", music);
		SetMixerVolume("GameplayMusicVolume", gameplay);
		SetMixerVolume("SFXVolume", sfx);
	}

	private void SetMixerVolume(string parameter, float value)
	{
		float volume = value > 0.0001f ? Mathf.Log10(value) * 20f : -80f;
		audioMixer.SetFloat(parameter, volume);
	}
}
