using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class DifficultyScript : MonoBehaviour
{
    int difficulty;

    private void Start() => refreshScores();

    public void click(int location)
    {
        switch (location)
        {
            case 0:
                SceneScript.FadeToScene("MemoryGame");
                CropTextures.GridType = CropTextures.Options.Grid3X3;
                GameController.num_buttons = 18;
                GameController.cellSize = 135f;
                GameController.collum = 6;
                break;

            case 1:
                SceneScript.FadeToScene("MemoryGame");
                CropTextures.GridType = CropTextures.Options.Grid4X4;
                GameController.num_buttons = 32;
                GameController.cellSize = 98f;
                GameController.collum = 8;
                break;

            case 2:
                SceneScript.FadeToScene("MemoryGame");
                CropTextures.GridType = CropTextures.Options.Grid5X5;
                GameController.num_buttons = 50;
                GameController.cellSize = 73f;
                GameController.collum = 10;
                break;

            case 3:
                SceneScript.FadeToScene("MenuGame");
                break;
        }

        if(location != 3)// indica que foi escolhida uma dificuldade
            difficulty = location;
    }

    public void hoverButton(int identifier)
    {
        Sprite hover = Resources.Load<Sprite>("Images/button3");
        switch (identifier)
        {
            case 0:
                GameObject.Find("Easy").GetComponent<Image>().sprite = hover;
                break;

            case 1:
                GameObject.Find("Medium").GetComponent<Image>().sprite = hover;
                break;

            case 2:
                GameObject.Find("Hard").GetComponent<Image>().sprite = hover;
                break;

            case 3:
                GameObject.Find("MainMenu").GetComponent<Image>().sprite = hover;
                break;
        }
    }

    public void exitButton(int identifier)
    {
        Sprite defaultImage = Resources.Load<Sprite>("Images/button");
        switch (identifier)
        {
            case 0:
                GameObject.Find("Easy").GetComponent<Image>().sprite = defaultImage;
                break;

            case 1:
                GameObject.Find("Medium").GetComponent<Image>().sprite = defaultImage;
                break;

            case 2:
                GameObject.Find("Hard").GetComponent<Image>().sprite = defaultImage;
                break;

            case 3:
                GameObject.Find("MainMenu").GetComponent<Image>().sprite = defaultImage;
                break;
        }
    }

    public void refreshScores()
    {
        string[] textNames = { "EasyH", "MediumH", "HardH" };
        
        StreamReader reader = new StreamReader(Application.streamingAssetsPath + "/Records.txt");
        string receive = reader.ReadToEnd();// le todos os recordes
        string[] records = (receive.Split(';'))[ImageSelect.imageSelected].Split("\n"[0]);// divide os recordes em dificuldades de acordo com o indice da dificuldade
        reader.Close();

        float record;
        for(int i=0;i<3;i++)
        {
            record = float.Parse(records[i].Split('/')[0]);
            string hours = Mathf.Floor((record % 216000) / 3600).ToString("00");
            string minutes = Mathf.Floor((record % 3600) / 60).ToString("00");
            string seconds = (record % 60).ToString("00");
            receive = hours + ":" + minutes + ":" + seconds + "  /  " + int.Parse(records[i].Split('/')[1]);
            GameObject.Find(textNames[i]).GetComponent<Text>().text = receive;
        }
    }
}
