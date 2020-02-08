using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour, ButtonInterface
{
    public void exitButton(int identifier)
    {
        Sprite defaultImage = Resources.Load<Sprite>("Images/button");
        switch(identifier)
        {
            case 0:
                GameObject.Find("Start Game").GetComponent<Image>().sprite = defaultImage;
                break;
            case 1:
                GameObject.Find("How To Play").GetComponent<Image>().sprite = defaultImage;
                break;
            case 2:
                GameObject.Find("Credits").GetComponent<Image>().sprite = defaultImage;
                break;
            case 3:
                GameObject.Find("Exit Game").GetComponent<Image>().sprite = defaultImage;
                break;
        }
    }

    public void hoverButton(int identifier)
    {
        Sprite hover = Resources.Load<Sprite>("Images/button3");
        switch (identifier)
        {
            case 0:
                GameObject.Find("Start Game").GetComponent<Image>().sprite = hover;
                break;
            case 1:
                GameObject.Find("How To Play").GetComponent<Image>().sprite = hover;
                break;
            case 2:
                GameObject.Find("Credits").GetComponent<Image>().sprite = hover;
                break;
            case 3:
                GameObject.Find("Exit Game").GetComponent<Image>().sprite = hover;
                break;
        }
    }

    public void ReturnMenu() => Application.Quit();

    private void Awake() => Screen.SetResolution(1280, 720, true);

    public void GoTo(string location) => SceneScript.FadeToScene(location);
}
