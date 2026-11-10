using UnityEngine;

public class SceneButton : MonoBehaviour
{
    [SerializeField] private string sceneName;

    // Carga la escena configurada en el Inspector.
    public void LoadScene()
    {
        SceneNavigator.Instance.LoadScene(sceneName);
    }
}