using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    public GameObject welcome;
    public GameObject config;
    public GameObject modePicker;
    public GameObject simulation;
    public GameObject setupPanel;
    public GameObject statsPanel;

    public TMP_Dropdown location;
    public TMP_Dropdown crop;
    public TMP_Dropdown time;

    public HUDManager hudManager;
    public PlayerController playerScript;

    public string Province = "";
    public string Crop = "";
    public int simDays = 10;
    public float climateDrySpeed = 1.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        welcome.SetActive(true);
        config.SetActive(false);
        if (modePicker != null) modePicker.SetActive(false);
        if (simulation != null) simulation.SetActive(false);
        if (setupPanel != null) setupPanel.SetActive(false);
        if (statsPanel != null) statsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Configuration()
    {
        welcome.SetActive(false);
        config.SetActive(true);
    }

   

    public void ConfirmConfig()
    {
        int locationIndex = location.value;
        if (locationIndex == 0)
        {
            Province = "Northern Province";
            climateDrySpeed = 0.5f;
        }
        else
        {
            Province = "Eastern Province";
            climateDrySpeed = 2.0f;
        }

        Crop = crop.options[crop.value].text;

        if (time.value == 0) simDays = 10;
        else simDays = 30;

        Debug.Log("Configured: " + Province + " | " + Crop + " | " + simDays + " days");

        config.SetActive(false);
        if (modePicker != null) modePicker.SetActive(true);
    }

    public void StartAutoMode()
    {
        if (modePicker != null) modePicker.SetActive(false);
        if (simulation != null) simulation.SetActive(true);
        if (setupPanel != null) setupPanel.SetActive(true);

        if (hudManager != null) hudManager.SimulationView();

        // Lock rotation so cursor can be used on setup panel
        if (playerScript != null) playerScript.isSimulating = false;

        Debug.Log("AUTO MODE: Setup panel active, waiting for Simulate button.");
    }

    public void StartManualMode()
    {
        if (modePicker != null) modePicker.SetActive(false);
        if (simulation != null) simulation.SetActive(true);
        if (statsPanel != null) statsPanel.SetActive(true);

        if (hudManager != null) hudManager.SimulationView();

        if (playerScript != null) playerScript.isSimulating = true;

        Debug.Log("MANUAL MODE: Player controls active.");
    }

    public void StartSimulation()
    {
        ConfirmConfig();
    }


}
