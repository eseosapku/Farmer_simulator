using TMPro;
using UnityEngine;

public class EduPopup : MonoBehaviour
{
    public GameObject popup;
    public TMP_Text titleText;
    public TMP_Text messageText;
    public PlayerController playerController;
    public AutoSim simulator;


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
        Debug.Log("POPUP SHOWING: " + title);

        if (popup == null) return;

        titleText.text = title;
        messageText.text = message;
        popup.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Cursor unlocked for popup interaction");
    }

    public void Hide()
    {
        Debug.Log("POPUP HIDE CALLED!");

        if (popup != null)
        {
            popup.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Cursor locked again, popup closed");
    }

    public void TestHideClick()
    {
        Debug.Log("Got It button was clicked!");
        Hide();
    }


}
