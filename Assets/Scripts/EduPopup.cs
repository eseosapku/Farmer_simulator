using UnityEngine;

public class EduPopup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(string title, string message)
    {
        Debug.Log("POPUP - " + title + ": " + message);
    }
}
