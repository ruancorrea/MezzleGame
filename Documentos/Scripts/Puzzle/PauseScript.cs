using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    private CanvasGroup canvasPuzzle, canvasMemory;
    [SerializeField]
    private Image grey;// tela escurecida que acontece quando se pausa o jogo
    private GameObject PauseText;
    public static bool isPaused;
    bool flag;

    private bool whichCanvas;

    void Start()
    {
        PauseText = GameObject.Find("PauseText");
        PauseText.SetActive(false);
        grey.GetComponent<Image>().enabled = false;
        flag = false;
        canvasPuzzle = GameObject.FindGameObjectWithTag("CanvasPuzzle").GetComponent<CanvasGroup>();
        canvasMemory = GameObject.FindGameObjectWithTag("CanvasMemory").GetComponent<CanvasGroup>();
    }
    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P) && GameManager.currentScore < GameManager.scoreTotal)
        {
            if (!isPaused)
            {
                GameObject.Find("Music").GetComponent<AudioSource>().Pause();
                // guarda qual canvas estava ligado no momento do pause
                whichCanvas = canvasMemory.blocksRaycasts;
                canvasMemory.blocksRaycasts = false;
                canvasPuzzle.blocksRaycasts = false;

                Timer.playing = false;
                canvasMemory.interactable = false;
                canvasPuzzle.interactable = false;
                grey.GetComponent<Image>().enabled = true;
                PauseText.SetActive(true);
                isPaused = true;
            }
            else
            {
                if(whichCanvas)
                {
                    //Memory
                    canvasMemory.interactable = true;
                    canvasMemory.blocksRaycasts = true;
                }
                else
                {
                    //Puzzle
                    canvasPuzzle.interactable = true;
                    canvasPuzzle.blocksRaycasts = true;
                }

                GameObject.Find("Music").GetComponent<AudioSource>().Play();
                Timer.playing = true;
                PauseText.SetActive(false);
                grey.GetComponent<Image>().enabled = false;
                isPaused = false;
            }  
        }
    }
}
