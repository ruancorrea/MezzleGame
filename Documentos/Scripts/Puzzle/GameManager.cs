using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text CongratulationsMessage;
    public static GameObject currentPiece;
    public static int currentScore, scoreTotal;
    
    private void Awake() => Timer.playing = true;

    private void Start() => CongratulationsMessage.gameObject.SetActive(false);
    
    private void Update()
    {
        if (currentScore == scoreTotal)
        {
            currentScore++;
            Timer.playing = false;
            GameObject.Find("PauseBackground").GetComponent<Image>().enabled = true;
            StartCoroutine(CheckHighscore());
        }
    }

    IEnumerator CheckHighscore()
    {
        yield return new WaitForSeconds(3 * Time.deltaTime);

        if (Timer.isNewHighscore)
        {
            CongratulationsMessage.text = "Congratulations!!\nYou Have Finished The Game!\nAnd Also Have Beaten Your Own Record!!";
            Timer.isNewHighscore = false;
        }
        else
            CongratulationsMessage.text = "Congratulations!!\nYou Have Finished The Game!";

        CongratulationsMessage.gameObject.SetActive(true);
    }
}
