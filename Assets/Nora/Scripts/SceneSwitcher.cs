using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string nextSceneName;

    public void GoToNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
