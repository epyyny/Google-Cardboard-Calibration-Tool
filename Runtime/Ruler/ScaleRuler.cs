//-----------------------------------------------------------------------
// changes size of ruler image when button has been pressed
//
// created by Emilia Pyyny-Polat, 2026
//-----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScaleRuler : MonoBehaviour
{
    public Image rulerImage;       // image to be scaled
    public float widthStep = 0.5f; // How much to change width per press
    public float heightStep = 0.5f; // How much to change height per press (ydpi scene)
    public DPICalculator dpiCalculator;

    void Start()
    {
        
        Canvas canvas = GetComponentInParent<Canvas>();

    }

    public void IncreaseWidth()
    {
        RectTransform rt = rulerImage.rectTransform;
        if (SceneManager.GetActiveScene().buildIndex == 1)
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + heightStep);
        else
            rt.sizeDelta = new Vector2(rt.sizeDelta.x + widthStep, rt.sizeDelta.y);
        dpiCalculator.RecalculateDPI();
    }

    public void DecreaseWidth()
    {
        RectTransform rt = rulerImage.rectTransform;
        if (SceneManager.GetActiveScene().buildIndex == 1)
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - heightStep);
        else
            rt.sizeDelta = new Vector2(rt.sizeDelta.x - widthStep, rt.sizeDelta.y);
        dpiCalculator.RecalculateDPI();
    }
}
