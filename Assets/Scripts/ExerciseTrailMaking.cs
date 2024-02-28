
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.GlobalIllumination;

public class ExerciseTrailMaking : MonoBehaviour
{
    public new Component light;
    public Camera camPacient;
    public static GameObject pontos;
    public Transform parent;
    private bool once = false;

    public Text escToLeave, timer;

    private float time;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        LanguageSelect.languageSelect(escToLeave);
    }

    void Start()
    {
        setLocation(pontos, parent);
    }
    // Update is called once per frame
    void Update()
    {
        timerCount();

        GameManager();
        
        
        if (pontos == null)
        {
            SceneManager.LoadSceneAsync("Main Menu");
        }

        light.transform.position = camPacient.transform.position;
        light.transform.rotation = camPacient.transform.rotation;
    }

    public void setLocation(GameObject pontos, Transform parent)  //not working properly
    {
        List<Vector3> positions = new List<Vector3>();
        Transform pontoI;

        for (int i = 0; i < OptionMenu.quantidadePontos; i++)
        {
            int x = UnityEngine.Random.Range(-688, 576);
            int y = UnityEngine.Random.Range(-220, 445);

            for (int h = 0; h < positions.Count; h++)
            {
                var tempX = positions[h].x;
                var tempY = positions[h].y;

                int sphere = circle(x, y, (int)tempX, (int)tempY, 110, 110);

                if (sphere == 0 || sphere == 1)
                {
                    x = UnityEngine.Random.Range(-688, 576);
                    y = UnityEngine.Random.Range(-220, 445);
                    h = -1;
                }
            }
            Vector3 temp = new Vector3(x, y, -21);
            positions.Add(temp);

            pontoI = Instantiate(pontos.transform.GetChild(i), parent, true);
            pontoI.transform.localPosition = new Vector3(x, y, -21);
            Quaternion rotation = Quaternion.Euler(90,0, 0);
            pontoI.transform.localRotation = rotation;
            pontoI.gameObject.SetActive(true);
        }
    }


    public void destroyPontos()  //Destroy pontos that exist in scene
    {
        GameObject[] pontos = GameObject.FindGameObjectsWithTag("Ponto"); //Search for every ponto in scene
        for (int j = 0; j < OptionMenu.quantidadePontos; j++)
        {
            if (pontos[j].activeInHierarchy)
            {
                Destroy(pontos[j]);
            }
        }
    }

    public void timerCount()
    {
        time += Time.deltaTime;
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        string timeText = String.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
        timer.text = timeText;
    }

    public void GameManager()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");  //If ESC is pressed return to main menu
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            destroyPontos();                    //if "R" key is pressed, call function destroyPontos() to delete all pontos
            setLocation(pontos, parent);        //and then create 5 more pontos in random locations by calling setLocation
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            time = 0;
            timer.text = "00:00:00";
        }
        else if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
    // public bool checkCollision(Vector3 pos)  //not working properly
    // {
    //     float sphereRad = 0.6f;
    //     Collider[] cols = Physics.OverlapSphere(pos, sphereRad, default, QueryTriggerInteraction.Collide);

    //     if (cols.Length == 0)
    //     {
    //         return true;
    //     }

    //     return false;
    // }

    private int circle(int x1, int y1, int x2,
                      int y2, int r1, int r2)
    {
        int distSq = (x1 - x2) * (x1 - x2) +
                     (y1 - y2) * (y1 - y2);
        int radSumSq = (r1 + r2) * (r1 + r2);
        if (distSq == radSumSq)
            return 1;
        else if (distSq > radSumSq)
            return -1;
        else
            return 0;
    }
}

