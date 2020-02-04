using System.Collections.Generic;
using UnityEngine;

public class CropTextures : MonoBehaviour
{
    ///Um enumerador que irá definir o tamanho da grade do jogo, 
    ///variando entre as grades 2x2 até 9x9 //
    public enum Options {
        Grid2X2 = 2,
        Grid3X3 = 3,
        Grid4X4 = 4,
        Grid5X5 = 5,
        Grid6X6 = 6,
        Grid7X7 = 7,
        Grid8X8 = 8,
        Grid9X9 = 9
    };

    public static Options GridType;
    public static Texture2D sourceTexture; // para recebermos a imagem que será cortada
    public GameObject piecePrefab; // para armazenar o prefab da peça a ser instanciada
    public GameObject gridPrefab;

    public static Sprite[] cropList = new Sprite[99999];
    private static GameObject[] pieces;

    private int amountPieces; // representará a quantidade de peças
    private List<Vector2> positions = new List<Vector2>(); // guardar todas as posições possíveis que as peças ficarão
    private List<Vector2> sortedPositions = new List<Vector2>(); // irá armazenadas as peças já sorteadas, para que não se repita um local e alguma peça fique sobre a outra
    private Vector2 position, distancePieces, resolutionPieces;

    private int identification = 0;// tags anexadas para comparacao entre a peca e a grid que a pertence
    // position irá guardar a posição da peça que será instanciada
    // distancePieces irá guardar a resolução do tamanho das peças para que possamos instanciá-las uma do lado da outra
    // resolutionPieces para armazenar a resolução de cada parte da imagem

    public void Awake()
    {
        StartComponents();
        CreatePositions();
        CreatePiece();
    }

    void StartComponents(){
        amountPieces = (int)GridType;
        resolutionPieces = new Vector2 (sourceTexture.width / amountPieces, sourceTexture.height / amountPieces); 
        GameManager.currentScore = 0 ;
        GameManager.scoreTotal = amountPieces * amountPieces;
    }

    Texture2D CropTexture(int row, int line) {
        var resolutionX = Mathf.RoundToInt (resolutionPieces.x);
        var resolutionY = Mathf.RoundToInt (resolutionPieces.y);
        Color[] pixels = sourceTexture.GetPixels(row * resolutionX, line * resolutionY, resolutionX, resolutionY);
    
        Texture2D tex = new Texture2D(resolutionX, resolutionY);
        tex.SetPixels(pixels);
        tex.Apply();
        return tex;
    }

    // Responsável por criar as possíveis posições para que as peças possam ser instanciadas
    void CreatePositions() {
        distancePieces = new Vector2 (resolutionPieces.x / 100.0f, resolutionPieces.y / 100.0f);
        for(int x = 0; x < amountPieces; x++){
            for(int y = 0; y < amountPieces ; y++){
                positions.Add(new Vector2(x * distancePieces.x , y * distancePieces.y));
            }
        }
    }

    Vector2 RandomPosition() {
        var sorted = false;
        var pos = Vector2.zero;
        while (!sorted) {
            pos = positions[Random.Range(0, positions.Count)];
            sorted = !sortedPositions.Contains(pos);
            if(sorted)
                sortedPositions.Add(pos);
        }
        return pos;
    }

    void CreateGrid(int j, int i, GameObject quad) {
        var grid = Instantiate (gridPrefab, new Vector2((j*distancePieces.x) - 10, i*distancePieces.y), Quaternion.identity) as GameObject;

        grid.name = identification + "";
        identification++;

        var newScale = new Vector2(resolutionPieces.x / 200f, resolutionPieces.y / 200f);
        grid.transform.localScale = new Vector3(newScale.x, newScale.y, grid.transform.localScale.z);
        quad.GetComponent<PieceScript>().endPosition = grid.transform.position;
        grid.GetComponent<BoxCollider2D>().size = new Vector2(2f, 2f);
        grid.transform.SetParent(GameObject.FindGameObjectWithTag("CanvasPuzzle").transform);
    }

    void CreatePiece() {
        var start = amountPieces - 1;
        int index = 0;
        for(int i = start; i >= 0; i--){
            for(int j = 0; j < amountPieces; j++){
                var texture = CropTexture(j, i);
                position = RandomPosition();

                var quad = Instantiate(piecePrefab, position, Quaternion.identity) as GameObject;
                quad.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                quad.GetComponent<SpriteRenderer>().sprite.name = "crop_" + index;

                cropList[index] = quad.GetComponent<SpriteRenderer>().sprite;
                index++;

                quad.GetComponent<BoxCollider2D>().size = new Vector2(distancePieces.x, distancePieces.y);
                quad.GetComponent<PieceScript>().startPosition = position;
                pieces = GameObject.FindGameObjectsWithTag("Piece");
                quad.transform.SetParent(GameObject.FindGameObjectWithTag("CanvasPuzzle").transform);
                quad.SetActive(false);
                quad.name = "" + identification;
                
                CreateGrid(j, i, quad);
            }
        }
        identification = 0;
    }

    public static void turnPiece(string name)
    {
        foreach (GameObject b in Resources.FindObjectsOfTypeAll(typeof (GameObject)) as GameObject[])
        {
            if (b.tag == "Piece")
            {
                if (b.GetComponent<SpriteRenderer>().sprite != null)
                {
                    if (b.GetComponent<SpriteRenderer>().sprite.name == name)
                        b.SetActive(true);
                }
                else
                    b.SetActive(false);
            }
        }
    }
}
