using UnityEngine;
using UnityEngine.UI;

public class InstructionsScript : MonoBehaviour, ButtonInterface
{
    public void hoverButton(int identifier)
    {
        Sprite hover = Resources.Load<Sprite>("Images/button3");
        GameObject.Find("Return").GetComponent<Image>().sprite = hover;
    }

    public void exitButton(int identifier)
    {
        Sprite defaultImage = Resources.Load<Sprite>("Images/button");
        GameObject.Find("Return").GetComponent<Image>().sprite = defaultImage;
    }

    public void ReturnMenu() => SceneScript.FadeToScene("MenuGame");
}
