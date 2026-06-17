using TMPro;
using UnityEngine;

public class EduPopup : MonoBehaviour
{
    public GameObject popup;
    public TMP_Text titleText;
    public TMP_Text messageText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (popup != null) popup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(string title, string message)
    {
        if (popup == null) return;

        titleText.text = title;
        messageText.text = message;
        popup.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Hide()
    {
        if (popup != null) popup.SetActive(false);
        Time.timeScale = 1f;
    }

   
}
