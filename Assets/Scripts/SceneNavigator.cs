using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public static SceneNavigator Instance { get; private set; }

    private void Awake()
    {
        // Comprobamos si ya existe una instancia.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Guardamos esta instancia.
        Instance = this;

        // Evitamos que se destruya al cambiar de escena.
        DontDestroyOnLoad(gameObject);
    }

    // Carga una escena indicando su nombre.
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Vuelve al menú principal.
    public void GoToMainMenu()
    {
        LoadScene("EscenaMenuPrincipalKahoot");
    }

    // Cierra el juego.
    public void ExitGame()
    {
        Application.Quit();
    }
}