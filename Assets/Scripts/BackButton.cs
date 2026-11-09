using UnityEngine;

public class BackButton : MonoBehaviour
{
    public void GoBack()
    {
        SceneNavigator.Instance.GoToMainMenu();
    }
}