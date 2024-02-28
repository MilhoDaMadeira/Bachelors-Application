using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanguageSelect : MonoBehaviour
{
    // Start is called before the first frame update 
    public static string language;
    private static object[] loadedIcons;
    private static int folderNumber, folderNaming = 3;
    public static Dictionary<int, string> folder = new Dictionary<int, string> { { 0, "Images for Stroop Test(Portuguese)" }, { 1, "Images for Stroop Test(English)" }, { 2, "Images for Stroop Test(French)" }, { 3, "Images for Naming Images" }, { 4, "ImagesNomination" } };
    public static Text escToLeave;
    private Button button;
    public void ChangeLanguage(Button button)
    {
        switch (button.name)
        {
            case "FlagP":
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll("Portuguese");
                language = Lean.Localization.LeanLocalization.GetFirstCurrentLanguage();
                folderNumber = 0;
                break;
            case "FlagE":
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll("English");
                language = Lean.Localization.LeanLocalization.GetFirstCurrentLanguage();
                folderNumber = 1;
                break;
            case "FlagF":
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll("French");
                language = Lean.Localization.LeanLocalization.GetFirstCurrentLanguage();
                folderNumber = 2;
                break;
            default:
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll("English");
                language = Lean.Localization.LeanLocalization.GetFirstCurrentLanguage();
                folderNumber = 1;
                break;
        }
    }
    public static void languageSelect(Text escToLeave)
    {
        switch (language)
        {
            case "Portuguese":
                escToLeave.text = "Pressione ESC para sair";
                break;
            case "English":
                escToLeave.text = "Press ESC to exit";
                break;
            case "French":
                escToLeave.text = "Appuyez ESC pour quitter";
                break;
            default:
                escToLeave.text = "Press ESC to exit";
                break;
        }
    }
    public static Sprite[] loadImages(int number)
    {
        loadedIcons = Resources.LoadAll(folder[number], typeof(Sprite));
        Icons = new Sprite[loadedIcons.Length];
        for (int x = 0; x < loadedIcons.Length; x++)
        {
            Icons[x] = (Sprite)loadedIcons[x];
        }
        return Icons;
    }

    public static void InstantiateImagesOption(int folderNumber, GameObject ImagePrefab, Transform content)
    {
        if (folderNumber < 3)
        {
            for (int i = 0; i < loadImages(folderNumber).Length; i++)
            {
                Sprite[] loadedIcons = loadImages(folderNumber);
                var ImageFab = Instantiate(ImagePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                ImageFab.gameObject.tag = "Stroop Test Prefab";
                ImageFab.transform.SetParent(content, false);
                ImageFab.GetComponentInChildren<Image>().sprite = loadedIcons[i];
                ImageFab.transform.localScale = Vector3.one;
                ImageFab.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < loadImages(folderNumber).Length; i++)
            {
                Sprite[] loadedIcons = loadImages(folderNumber);
                var ImageFab = Instantiate(ImagePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                ImageFab.gameObject.tag = "Naming Images Prefab";
                ImageFab.transform.SetParent(content, false);
                ImageFab.GetComponentInChildren<Image>().sprite = loadedIcons[i];
                ImageFab.transform.localScale = Vector3.one;
                ImageFab.SetActive(true);
            }
        }
    }
    public static Sprite[] Icons { get; set; }

}
