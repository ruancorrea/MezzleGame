using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    private AudioSource music;
    private static MusicScript SingleTon = null;

    private void Awake()
    {
        /* Eh necessario usar singleton aqui pois toda vez que voltamos
         * ao menu principal ela tenta ser instanciada novamente já que
         * é a cena onde ela foi criada.
         * Precisamos somente de uma instancia, pois a musica comeca no
         * menu principal e continua tocando mesmo trocando cenas
         * se nao usassemos singleton aqui a musica seria duplicada
         * ocasionando em duas trilhas sonoras tocando ao mesmo tempo.
        */
        if (SingleTon != null && SingleTon != this)// verifica se uma nova instacia foi criada
        {
            Destroy(this.gameObject);// se sim então destroi ela e fica com a anterior
            // nesse caso destruimos o objeto que foi duplicado ficando somente com
            // que já existia.
            return;
        }
        else
            SingleTon = this;// primeira instancia do objeto

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
