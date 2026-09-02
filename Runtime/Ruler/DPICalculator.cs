//-----------------------------------------------------------------------
// calculates DPI of user device based off of scaled ruler image
// loads saved DPI from PlayerPrefs if it exists, otherwise calculates it on startup and saves it to PlayerPrefs
// created by Emilia Pyyny-Polat, 2026
//-----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DPICalculator : MonoBehaviour
{
    public Image rulerImage;   // ruler image
    public Text dpiText;       // UI text
    float DPI;
    float rulerWidthPixels;
    float rulerWidthInches; // set per-scene in RecalculateDPI
    public static float CalculatedXDPI;      // calculated DPI value to be accessed by other scripts
    public static float CalculatedYDPI;     // calculated Y-DPI value to be accessed by other scripts

    private Canvas canvas;
    //private float scaleFactor;  // canvas scale factor


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        canvas = GetComponentInParent<Canvas>();

        SceneManager.sceneLoaded += OnSceneLoaded;
        LoadPrefsForScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadPrefsForScene(scene.buildIndex);
    }

    private void LoadPrefsForScene(int sceneIndex)
    {
        string dpiKey = sceneIndex == 1 ? "SavedYDPI" : "SavedXDPI";
        string widthKey = sceneIndex == 1 ? "RulerWidthY" : "RulerWidthX";

        if (PlayerPrefs.HasKey(widthKey))
        {
            float savedWidth = PlayerPrefs.GetFloat(widthKey);
            RectTransform rt = rulerImage.rectTransform;
            rt.sizeDelta = sceneIndex == 1
                ? new Vector2(rt.sizeDelta.x, savedWidth)
                : new Vector2(savedWidth, rt.sizeDelta.y);
        }

        if (PlayerPrefs.HasKey(dpiKey))
            if (sceneIndex == 1)
                CalculatedYDPI = PlayerPrefs.GetFloat(dpiKey);
            else
                CalculatedXDPI = PlayerPrefs.GetFloat(dpiKey);

        UpdateDPIText();
    }

    public void RecalculateDPI()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        rulerWidthInches = sceneIndex == 1 ? 0.062f / 0.0254f : 0.124f / 0.0254f;
        rulerWidthPixels = sceneIndex == 1
            ? rulerImage.rectTransform.sizeDelta.y
            : rulerImage.rectTransform.sizeDelta.x;
        if (sceneIndex == 1)
        {
            CalculatedYDPI = (rulerWidthPixels * canvas.scaleFactor) / rulerWidthInches;
        }
        else
        {
            CalculatedXDPI = (rulerWidthPixels * canvas.scaleFactor) / rulerWidthInches;
        }

        string dpiKey = sceneIndex == 1 ? "SavedYDPI" : "SavedXDPI";
        string widthKey = sceneIndex == 1 ? "RulerWidthY" : "RulerWidthX";
        PlayerPrefs.SetFloat(dpiKey, sceneIndex == 1 ? CalculatedYDPI : CalculatedXDPI);
        PlayerPrefs.SetFloat(widthKey, rulerWidthPixels);
        PlayerPrefs.Save();

        UpdateDPIText();
    }

    private void UpdateDPIText()
    {
        if (dpiText != null)
            if (SceneManager.GetActiveScene().buildIndex == 1)
                dpiText.text = "Y-DPI: " + CalculatedYDPI.ToString("F1");
            else
                dpiText.text = "X-DPI: " + CalculatedXDPI.ToString("F1");
    }
}
