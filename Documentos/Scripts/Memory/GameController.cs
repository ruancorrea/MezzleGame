using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour, ButtonInterface
{
    [SerializeField]
    private GameObject canvasMemory, canvasPuzzle;// canvas que contém cada jogo

    [SerializeField]
    private Sprite bgSprite;// sprite que esconde a carta do jogo da memoria / costas da carta

    public Sprite[] puzzles;// variavel que recebe as texturas cortadas que serao duplicadas e salvas estas no gamePuzzles

    public Camera firstC;// puzzle camera
    public Camera secondC;// memory camera

    public List<Sprite> gamePuzzles = new List<Sprite>();// armazenador das texturas escondidas dos botoes do jogo da memoria

    public List<Button> btns = new List<Button>();// botoes do jogo da memoria

    private bool first, second;// saber se ja clicou uma ou duas vezes

    private int firstIndex, secondIndex;// indice da carta selecionada
    private string firstName, secondName; // identificador para ver se as cartas sao iguais
    private int acertos=0, num_acertos=0; // acertos = acertos atuais / num_acertos = maximo de acertos / usado para saber se o jogo da memoria acabou
    public static int tentativas = 0;

    //controladores da dificuldade do jogo da memoria
    public static int num_buttons, collum;
    public static float cellSize;

    //Variaveis de animacao dos botoes do jogo da memoria
    private float animationTimer = 0;
    private Transform objectToAnimate;
    private bool isAnimation;

    public float animationDuration = 0.1f; // tempo que a animacao do botao demora para um volta completa unidade: segundos

    void Awake()
    {
        Screen.SetResolution(1280, 720, true);
        GameObject.Find("Attempts").GetComponent<Text>().text = "Attempts: " + 0;

        GridLayoutGroup memoryGrid = GameObject.FindGameObjectWithTag("MemoryGrid").GetComponent<GridLayoutGroup>();
        memoryGrid.constraintCount = collum;
        memoryGrid.cellSize = new Vector2(cellSize, cellSize);
        GetComponent<AddButtons>().CreateButtons(num_buttons);

        if (num_buttons == 18)
            Timer.difficulty = 0;
        else if (num_buttons == 32)
            Timer.difficulty = 1;
        else if (num_buttons == 50)
            Timer.difficulty = 2;
    }

    void Start() => Inicialize();

    void Inicialize()
    {
        //Debug.Log("alo");
        gamePuzzles.Clear();
        btns.Clear();
        puzzles = CropTextures.cropList;
        getButtons();
        AddListeners();
        AddGamePuzzles();
        Flush(gamePuzzles);

        acertos = 0;
        tentativas = 0;
        num_acertos = gamePuzzles.Count / 2; // numero de acertos é metade das imagens da matriz

        foreach(Button btn in btns)
        {
            btn.interactable = true;
        }
        canvasPuzzle.GetComponent<CanvasGroup>().interactable = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        AnimateMemoryGameButtons();
        CheckIfTheGameFinished();
        GameObject.Find("Attempts").GetComponent<Text>().text = "Attempts: " + tentativas;
        if (Input.GetKey(KeyCode.V) && !PauseScript.isPaused)
        {
            canvasMemory.GetComponent<CanvasGroup>().interactable = true;
            canvasPuzzle.GetComponent<CanvasGroup>().interactable = false;

            canvasMemory.GetComponent<CanvasGroup>().blocksRaycasts = true;
            canvasPuzzle.GetComponent<CanvasGroup>().blocksRaycasts = false;
            
            firstC.enabled = false;
            secondC.enabled = true;
        }
        if(Input.GetKey(KeyCode.C) && !PauseScript.isPaused)
        {
            canvasMemory.GetComponent<CanvasGroup>().interactable = false;
            canvasPuzzle.GetComponent<CanvasGroup>().interactable = true;

            canvasMemory.GetComponent<CanvasGroup>().blocksRaycasts = false;
            canvasPuzzle.GetComponent<CanvasGroup>().blocksRaycasts = true;

            firstC.enabled = true;
            secondC.enabled = false;
        }
    }

    void AnimateMemoryGameButtons()
    {
        if (objectToAnimate != null && isAnimation)
        {
            GameObject.Find("Panel").GetComponent<CanvasGroup>().blocksRaycasts = false;
            if (animationTimer <= animationDuration / 2)
                objectToAnimate.rotation = Quaternion.RotateTowards(objectToAnimate.rotation, Quaternion.Euler(0, 90f, 0), 5);
            else if (animationTimer > animationDuration / 2 && animationTimer <= animationDuration)
            {
                if(!second)
                {
                    firstIndex = int.Parse(objectToAnimate.name);
                    btns[firstIndex].image.sprite = gamePuzzles[firstIndex];
                    firstName = gamePuzzles[firstIndex].name;
                }
                else
                {
                    secondIndex = int.Parse(objectToAnimate.name);
                    if (secondIndex != firstIndex)// nao pode clicar na primeira imagem
                    {
                        btns[secondIndex].image.sprite = gamePuzzles[secondIndex];
                        secondName = gamePuzzles[secondIndex].name;   
                    }
                }
                objectToAnimate.rotation = Quaternion.RotateTowards(objectToAnimate.rotation, Quaternion.Euler(0, 360f, 0), 5);
            }
            else
            {
                if (second)
                    StartCoroutine(CheckIfThePuzzleMatch());
                else
                    GameObject.Find("Panel").GetComponent<CanvasGroup>().blocksRaycasts = true;
                    animationTimer = 0;
                    UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
                    isAnimation = false;
                    objectToAnimate = null;
            }

            animationTimer += Time.deltaTime;
        }
    }

    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;

        for(int i = 0;i<looper;i++)// loop para adicionar o puzzle
        {
            if(index == looper/2)// necessario para poder colocar a mesma sprite novamente
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }

    public void AddListeners()
    {
        GameObject.Find("Return").GetComponent<Button>().onClick.AddListener(() => ReturnMenu());
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
            btn.GetComponentInChildren<Text>().text = "";
        }
    }

    public void PickAPuzzle()
    {
        if (!first) // se nao pegou a primeira, indica que esta que ele clicou eh a primeira
        {
            first = true;
        }
        else if(!second)// se ele ja clicou na primeira, entao esta eh a segunda
        {
            second = true;
            tentativas++;
        }
        objectToAnimate = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform;// recebe botao clicado para aplicar animacao de rotacao
        if(int.Parse(objectToAnimate.name) == firstIndex && second)
        {
            second = false;
            tentativas--;
            objectToAnimate = null;
        }
        else
            isAnimation = true;// flag que indica que a animacao esta em andamento, logo n se pode clicar em outro botao ainda
    }

    IEnumerator CheckIfThePuzzleMatch()
    {
        
        //yield return new WaitForSeconds(1f);// espera 1 segundo

        if(firstName == secondName)
        {
            CropTextures.turnPiece(firstName);
            
            yield return new WaitForSeconds(.7f);// espera meio segundo

            btns[firstIndex].interactable = false;// impede a interação com aquelas imagens
            btns[secondIndex].interactable = false;
            acertos++;
        }
        else
        {
            yield return new WaitForSeconds(.7f);
            
            btns[firstIndex].image.sprite = bgSprite;
            btns[secondIndex].image.sprite = bgSprite;
            // volta as cartas para a imagem padrao
        }

        first = second = false;

        GameObject.Find("Panel").GetComponent<CanvasGroup>().blocksRaycasts = true;
        animationTimer = 0;
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        isAnimation = false;
        objectToAnimate = null;
    }

    void CheckIfTheGameFinished()
    {
        if(acertos == num_acertos)
        {
            acertos = 0;
        }
    }

    void getButtons()
    {
        GameObject[] botoes = GameObject.FindGameObjectsWithTag("PuzzleButton");// recebe todos os objetos que estão com a tag PuzzleButtons
            // ou seja, recebe todos os botoes do jogo
        

        for (int i = 0; i < num_buttons ;i++)
        {
            btns.Add(botoes[i].GetComponent<Button>());// adiciona os botoes na lista
            btns[i].image.sprite = bgSprite; // poe imagem padrao nos botoes
        }
    }

    void Flush(List<Sprite> sprites)
    {
        for(int i = 0; i < sprites.Count; i++)
        {
            Sprite aux = sprites[i];
            int randomIndex = Random.Range(0, sprites.Count);
            sprites[i] = sprites[randomIndex];
            sprites[randomIndex] = aux;
        }
    }

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
