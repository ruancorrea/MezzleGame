using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    private AudioSource music;
    private static MusicScript instance = null;

    public static MusicScript Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        music = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if(music.isPlaying)
            return;
        music.Play();
    }

    public void PauseMusic() => music.Pause();
}
