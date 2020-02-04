using UnityEngine;
using UnityEngine.UI;

public class ImageSelect : MonoBehaviour, ButtonInterface
{
    public GameObject[] images;// imagens que serao usadas para a selecao no Menu de Selecao
    public static int imageSelected;// imagem selecionada que sera usada no jogo

    void Awake()
    {
        images = GameObject.FindGameObjectsWithTag("ImageSel");
        string a;
        for (int i = 0; i < 6; i++)
            images[i].GetComponent<Button>().onClick.AddListener(() => Check());
    }

    void Check()
    {
        GameObject selected = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (selected.tag == "ImageSel")
        {
            CropTextures.sourceTexture = Resources.Load<Texture2D>("Images/ImagesToSelect/" + selected.name);
            imageSelected = int.Parse(selected.name.Split('e')[1]);
            SceneScript.FadeToScene("DifficultyMenuGame");
        }
    }

    public void ReturnMenu() => SceneScript.FadeToScene("MenuGame");

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
}
