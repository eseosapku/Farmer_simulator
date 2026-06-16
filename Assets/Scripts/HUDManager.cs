using UnityEngine;
using UnityEngine.InputSystem;

public class HUDManager : MonoBehaviour
{
    public GameObject manageCrops;
    public GameObject manageAnimals;
    public GameObject manageResources;
    public GameObject bottomBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manageCrops.SetActive(true);
        manageAnimals.SetActive(false);
        manageResources.SetActive(false);
        bottomBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ToggleNavigationMenu();
        }
    }


    public void SimulationView()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        bottomBar.SetActive(false);
    }

        void ToggleNavigationMenu()
    {
        if (bottomBar.activeSelf == false)
        {
            bottomBar.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            bottomBar.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false;
        }
    }

    public void CropsSelected()
    {
        manageCrops.SetActive(true);
        manageAnimals.SetActive(false);
        manageResources.SetActive(false);
        ToggleNavigationMenu();
    }

    public void AnimalsSelected()
    {
        manageCrops.SetActive(false);
        manageAnimals.SetActive(true);
        manageResources.SetActive(false);
        ToggleNavigationMenu();
    }

    public void ResourcesSelected()
    {
        manageCrops.SetActive(false);
        manageAnimals.SetActive(false);
        manageResources.SetActive(true);
        ToggleNavigationMenu();
    }

}
