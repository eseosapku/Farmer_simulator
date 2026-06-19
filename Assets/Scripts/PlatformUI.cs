using UnityEngine;

public class PlatformUI : MonoBehaviour
{

    public GameObject mobileControls;
    public GameObject pcControls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool isMobile = Application.isMobilePlatform;

        if (mobileControls != null) mobileControls.SetActive(isMobile);
        if (pcControls != null) pcControls.SetActive(!isMobile);

        Debug.Log("Platform detected: " + (isMobile ? "Mobile" : "PC/Web"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
