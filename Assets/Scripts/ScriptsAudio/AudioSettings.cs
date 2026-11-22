
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour // script que controla la confi del volumen
{
	[Header("Audio Mixer")]
	[SerializeField] private AudioMixer audioMixer;

	[Header("Sliders")]
	[SerializeField] private Slider musicSlider;
	[SerializeField] private Slider gameplayMusicSlider;
	[SerializeField] private Slider sfxSlider;

	private const string MUSIC_KEY = "MusicVolume";
	private const string GAMEPLAY_KEY = "GameplayMusicVolume";
	private const string SFX_KEY = "SFXVolume";

	private void Start()
	{
		// Recuperamos los volúmenes guardados.
		float music = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
		float gameplay = PlayerPrefs.GetFloat(GAMEPLAY_KEY, 1f);
		float sfx = PlayerPrefs.GetFloat(SFX_KEY, 1f);

		// Mostramos los valores en los sliders.
		musicSlider.value = music;
		gameplayMusicSlider.value = gameplay;
		sfxSlider.value = sfx;

		// Escuchamos los cambios de los sliders.
		musicSlider.onValueChanged.AddListener(SetMusicVolume);
		gameplayMusicSlider.onValueChanged.AddListener(SetGameplayMusicVolume);
		sfxSlider.onValueChanged.AddListener(SetSFXVolume);
	}

	public void SetMusicVolume(float value)
	{
		SetMixerVolume("MusicVolume", value);
		PlayerPrefs.SetFloat(MUSIC_KEY, value);
	}

	public void SetGameplayMusicVolume(float value)
	{
		SetMixerVolume("GameplayMusicVolume", value);
		PlayerPrefs.SetFloat(GAMEPLAY_KEY, value);
	}

	public void SetSFXVolume(float value)
	{
		SetMixerVolume("SFXVolume", value);
		PlayerPrefs.SetFloat(SFX_KEY, value);
	}

	private void SetMixerVolume(string parameter, float value)
	{
		// Convertimos el valor del slider a decibelios.
		float volume = value > 0.0001f ? Mathf.Log10(value) * 20f : -80f;

		audioMixer.SetFloat(parameter, volume);
	}

	private void OnDisable()
	{
		PlayerPrefs.Save();
	}

	private void OnDestroy()
	{
		if (musicSlider != null)
			musicSlider.onValueChanged.RemoveListener(SetMusicVolume);

		if (gameplayMusicSlider != null)
			gameplayMusicSlider.onValueChanged.RemoveListener(SetGameplayMusicVolume);

		if (sfxSlider != null)
			sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
	}
}
