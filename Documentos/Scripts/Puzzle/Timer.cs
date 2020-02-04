using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Timer : MonoBehaviour
{
    private string path = Application.streamingAssetsPath + "/Records.txt";// caminho para o arquivo que guarda os recordes das imagens
    public static bool playing;// logica que indica que o tempo esta passando
    private float bestTime;// melhor tempo daquela partida
    public static int difficulty;// dificuldade daquela partida
    private string[] records;// recordes da imagem selecionada no Menu de Selecao
    private Text timer;// texto que mostra o tempo atual
    private float current_time;// variavel que soma o tempo entre frames
    public int speed = 1;// velocidade que o tempo passa 
    private string receive;// variavel que recebe todo o arquivo dos recordes
    public static bool isNewHighscore;

    void Start()
    {
        StreamReader reader = new StreamReader(path);
        receive = reader.ReadToEnd();// le todos os recordes
        records = (receive.Split(';'))[ImageSelect.imageSelected].Split("\n"[0]);// divide os recordes de acordo com a imagem
        reader.Close();

        bestTime = GetBestTime(difficulty);// recebe o melhor tempo da dificuldade escolhida para aquela imagem
        timer = GameObject.Find("Timer").GetComponent<Text>();
        current_time = 0;
    }

    void Update()
    {
        if (current_time < bestTime && GameManager.currentScore >= GameManager.scoreTotal)
        {
            /*
                Caso ele bata o recorde daquela imagem e dificuldade
                o novo recorde é salvo devolta no arquivo de recordes
            */
            isNewHighscore = true;
            bestTime = current_time;
            WriteRecords(bestTime, difficulty);
        }
        if (playing)// atualiza o texto do tempo atual
        {
            current_time += Time.deltaTime * speed;
            string hours = Mathf.Floor((current_time % 216000) / 3600).ToString("00");
            string minutes = Mathf.Floor((current_time % 3600) / 60).ToString("00");
            string seconds = (current_time % 60).ToString("00");
            timer.text = hours + ":" + minutes + ":" + seconds;
        }
    }

    void WriteRecords(float new_record, int difficulty)
    {
        File.WriteAllText(path, string.Empty);

        StreamWriter writer = new StreamWriter(path, false);

        for (int i = 0; i < 6; i++)
        {
            records = (receive.Split(';'))[i].Split("\n"[0]);
            if (ImageSelect.imageSelected == i)
                records[difficulty] = new_record + "/" + GameController.tentativas;

            for (int j = 0; j < 3; j++)
            {
                if (j != 2)
                {
                    writer.Write(records[j]);
                    writer.Write("\n");
                }
                else
                    writer.Write(records[j]);
            }
            if (i != 5)
                writer.Write(";");
        }
        writer.Close();
    }

    // retorna o melhor tempo da imagem e dificuldade atual
    float GetBestTime(int difficulty) => float.Parse(records[difficulty].Split('/')[0]);
}
    
/*
86400/0
86400/0
86400/0;86400/0
86400/0
86400/0;86400/0
86400/0
86400/0;86400/0
86400/0
86400/0;86400/0
86400/0
86400/0;86400/0
86400/0
86400/0
 */