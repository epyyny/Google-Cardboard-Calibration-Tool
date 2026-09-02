using UnityEngine;
using UnityEngine.EventSystems;

public class Help_button : MonoBehaviour,
IPointerClickHandler
{
    public GameObject hintPanel;   // full-screen panel containing the hint image

    public void ShowHint()
    {
        hintPanel.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        hintPanel.SetActive(false);
    }
}
