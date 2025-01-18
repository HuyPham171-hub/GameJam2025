using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class ReloadScene : MonoBehaviour
{
    public static ReloadScene Instance;
    public TextMeshProUGUI StatusText;

    // Awake is called before Start and is used for initialization
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance of this object exists
            return;
        }

        DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed when loading new scenes

        // Set the object inactive when the scene starts or is reloaded
    }

    // Method to reload the current scene
    public void ReloadCurrentScene()
    {
        // Reload the current scene by using SceneManager
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameController.Instance.GameStart();
    }
    public void SettingStatus(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    public void SettingText(string statusText)
    {
        StatusText.text = statusText;
    }
}
