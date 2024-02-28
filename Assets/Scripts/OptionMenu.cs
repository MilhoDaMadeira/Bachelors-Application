using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class OptionMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ImagePrefab, prefabN, prefabL, SelectRemove, TrailMakingOptions;
    public Transform ContentStroop, ContentNaming;
    public Slider slider;
    private bool clickedN = false, clickedS = false;

    public static List<Object> imagesForUseNomination, imagesForUseStroop;
    public static int quantidadePontos;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start()
    {
        SelectRemove.SetActive(false);
    }
    public void Return(GameObject mainMenu)
    {
        mainMenu.gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Option Menu").SetActive(false);
        ContentNaming.parent.parent.gameObject.SetActive(false);
        ContentStroop.parent.parent.gameObject.SetActive(false);
        for (int i = 0; i < ContentStroop.childCount; i++)
        {
            Destroy(obj: ContentStroop.GetChild(i).gameObject);
        }
        clickedS = false;
    }

    public void ImagesPressButton()
    {
        if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name == "Stroop Test" && (!clickedS))
        {
            switch (LanguageSelect.language)
            {
                case "Portuguese":
                    LanguageSelect.InstantiateImagesOption(0, ImagePrefab, ContentStroop);
                    break;
                case "English":
                    LanguageSelect.InstantiateImagesOption(1, ImagePrefab, ContentStroop);
                    break;
                case "French":
                    LanguageSelect.InstantiateImagesOption(2, ImagePrefab, ContentStroop);
                    break;
                default:
                    LanguageSelect.InstantiateImagesOption(1, ImagePrefab, ContentStroop);
                    break;
            }
            clickedS = true;
        }
        else if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name == "Naming Images" && (!clickedN))
        {
            LanguageSelect.InstantiateImagesOption(3, ImagePrefab, ContentNaming);
            clickedN = true;
        }
    }
    public void applySettingsExerciseTrail(ToggleGroup toggled)   //NOT WORKING YET
    {
        if (TrailMakingOptions.activeInHierarchy)
        {
            quantidadePontos = (int)slider.value;
            if (toggled.transform.GetChild(0).GetComponent<Toggle>().isOn)
            {
                ExerciseTrailMaking.pontos = prefabN;
            }
            else
            {
                ExerciseTrailMaking.pontos = prefabL;
            }
        }
    }

    public void applySettingsExercises()
    {
        if (ContentNaming.transform.gameObject.activeInHierarchy)
        {
            imagesForUseNomination = applySettingsFolder("Images for Naming Images", "Naming Images Prefab");
        }
        else if (ContentStroop.transform.gameObject.activeInHierarchy)
        {
            imagesForUseStroop = applySettingsFolderStroop();
        }
    }

    public List<Object> applySettingsFolder(string folder, string prefab)
    {
        List<Object> imagesForUse = new List<Object>();
        Object[] imagesResources = Resources.LoadAll(folder, typeof(Sprite));
        GameObject[] instances = GameObject.FindGameObjectsWithTag(prefab);
        int k = 0;
        for (int l = 0; l < imagesResources.Length; l++)
        {
            for (int i = 0; i < instances.Length; i++)
            {
                if ((imagesResources[l].name == instances[i].transform.GetChild(0).GetComponent<Image>().sprite.name) && instances[i].transform.GetChild(1).GetComponent<Toggle>().isOn)
                {
                    imagesForUse.Add(imagesResources[l]);
                    k++;
                }
            }
        }
        return imagesForUse;
    }

    public List<Object> applySettingsFolderStroop()
    {
        string language = LanguageSelect.language;
        string folder;
        string prefab = "Stroop Test Prefab";
        switch (language)
        {
            case "Portuguese":
                folder = "Images for Stroop Test(Portuguese)";
                return applySettingsFolder(folder, prefab);
            case "English":
                folder = "Images for Stroop Test(English)";
                return applySettingsFolder(folder, prefab);
            case "French":
                folder = "Images for Stroop Test(French)";
                return applySettingsFolder(folder, prefab);
            default:
                folder = "Images for Stroop Test(English)";
                return applySettingsFolder(folder, prefab);
        }
    }

    public void UseAll(GameObject content)
    {
        GameObject contentType = content;
        if (contentType.gameObject.activeInHierarchy && contentType.gameObject.name == "Content for Nomination")
        {
            for (int i = 0; i < content.transform.childCount; i++)
            {
                content.transform.GetChild(i).transform.GetChild(1).GetComponent<Toggle>().isOn = true;
            }
        }
        else
        {
            contentType = GameObject.FindGameObjectWithTag("Content for Stroop");
            for (int i = 0; i < contentType.transform.childCount; i++)
            {
                contentType.transform.GetChild(i).transform.GetChild(1).GetComponent<Toggle>().isOn = true;
            }
        }
    }

    public void RemoveAll(GameObject content)
    {
        GameObject contentType = content;
        if (contentType.gameObject.activeInHierarchy && contentType.gameObject.name == "Content for Nomination")
        {
            for (int i = 0; i < content.transform.childCount; i++)
            {
                content.transform.GetChild(i).transform.GetChild(1).GetComponent<Toggle>().isOn = false;
            }
        }
        else
        {
            contentType = GameObject.FindGameObjectWithTag("Content for Stroop");
            for (int i = 0; i < contentType.transform.childCount; i++)
            {
                contentType.transform.GetChild(i).transform.GetChild(1).GetComponent<Toggle>().isOn = false;
            }
        }
    }

    public void ConfirmationBox(GameObject confirmationBox)
    {
        if(ContentStroop.gameObject.activeInHierarchy || ContentNaming.gameObject.activeInHierarchy || TrailMakingOptions.gameObject.activeInHierarchy)
        {
            confirmationBox.gameObject.SetActive(true);
        }
    }
}
