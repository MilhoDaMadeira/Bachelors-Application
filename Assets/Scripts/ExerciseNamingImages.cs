using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExerciseNamingImages : MonoBehaviour
{
    public new Camera camera;
    public Image imagePrefab,
        redFlash;
    public Text escToLeave;
    private float timerCountExercise;

    private List<Object> sprites;
    private int i = 0;
    private bool first = true,
        last = true;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        redFlash.gameObject.SetActive(false);
        imagePrefab.gameObject.SetActive(true);
        camera.clearFlags = CameraClearFlags.SolidColor;
        LanguageSelect.languageSelect(escToLeave);
        sprites = OptionMenu.imagesForUseNomination;
        if (sprites == null || sprites.Count == 0)
        {
            SceneManager.LoadSceneAsync("Main Menu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        ExerciseStart();

        GameManager();
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            continueImagesRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            continueImagesLeft();
        }
    }

    public void ExerciseStart()
    {
        timerCountExercise = timerCountExercise - Time.deltaTime;
        if (timerCountExercise > 0) { }
        else
        {
            if (sprites.Count != i)
            {
                if (!first)
                {
                    flashRed(redFlash);
                }
                else
                {
                    timerCountExercise = 5;
                    imagePrefab.sprite = (Sprite)sprites[i];
                    first = false;
                }
            }
            else
            {
                if (last)
                {
                    flashRed(redFlash);
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        last = false;
                        i = 0;
                    }
                }
            }
        }
    }

    public void GameManager()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync("Main Menu");
        }
    }

    void flashRed(Image redFlash)
    {
        redFlash.gameObject.SetActive(true);
    }

    public void continueImagesRight()
    {
        if (i == sprites.Count - 1)
        {
            i = 0;
            redFlash.gameObject.SetActive(false);
            timerCountExercise = 5;
            imagePrefab.sprite = (Sprite)sprites[i];
        }
        else
        {
            i++;
            redFlash.gameObject.SetActive(false);
            timerCountExercise = 5;
            imagePrefab.sprite = (Sprite)sprites[i];
        }
    }

    public void continueImagesLeft()
    {
        if (i == 0)
        {
            i = sprites.Count - 1;
            redFlash.gameObject.SetActive(false);
            timerCountExercise = 5;
            imagePrefab.sprite = (Sprite)sprites[i];
        }
        else
        {
            i--;
            redFlash.gameObject.SetActive(false);
            timerCountExercise = 5;
            imagePrefab.sprite = (Sprite)sprites[i];
        }
    }
}
