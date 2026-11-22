using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
	public void PlayClick()
	{
		AudioManager.Instance?.PlayButtonClick();
	}
}