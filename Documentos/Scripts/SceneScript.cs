using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    [SerializeField]
    private static Animator SceneAnimator;

    private static string nextScene;

    private void Start() => SceneAnimator = GetComponent<Animator>();

    public static void FadeToScene(string nextSceneName)
    {
        nextScene = nextSceneName;
        SceneAnimator.SetTrigger("FadeOut");
    }

    private void SwitchScene() => SceneManager.LoadScene(nextScene);
}
