using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimSetup : MonoBehaviour
{
    public AutoSim simulator;
    public MenuNavigation menuNavigation;
    public PlayerController playerController;
    public GameObject setupPanelRoot;
    public GameObject statsPanel;
    public Slider waterSlider;
    public TMP_Text waterLabel;
    public Toggle chemicalToggle;
    public Toggle compostToggle;
    public TMP_Text dayCounterText;
    public TMP_Text moistureText;
    public TMP_Text fertiliserText;
    public TMP_Text compostText;
    public TMP_Text weedText;
    public TMP_Text healthText;
    public TMP_Text stageText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (statsPanel != null) statsPanel.SetActive(false);
        LockRotation(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (waterLabel != null && waterSlider != null)
        {
            waterLabel.text = "Water per day: " + waterSlider.value.ToString("0");
        }

        if (dayCounterText != null && simulator != null)
        {
            dayCounterText.text = "Day " + simulator.currentDay + " / " + simulator.totalDays;
        }

        if (simulator != null && simulator.carrot != null)
        {
            var c = simulator.carrot;
            if (moistureText != null) moistureText.text = "Moisture: " + c.moisture.ToString("0") + "%";
            if (fertiliserText != null) fertiliserText.text = "fertiliser: " + c.fertiliser.ToString("0") + "%";
            if (compostText != null) compostText.text = "Compost: " + c.compost.ToString("0") + "%";
            if (weedText != null) weedText.text = "Weeds: " + c.weedInfestation.ToString("0") + "%";
            if (healthText != null) healthText.text = "Health: " + c.plantHealth.ToString("0") + "%";
            if (stageText != null) stageText.text = "Stage: " + c.currentStage + " / 7";
        }
    }

    public void OnSimulateButtonClicked()
    {
        if (simulator == null || menuNavigation == null) return;

        simulator.province = menuNavigation.Province;
        simulator.totalDays = menuNavigation.simDays;
        simulator.waterPerDay = waterSlider.value;
        simulator.useChemicalfertiliser = chemicalToggle.isOn;
        simulator.useCompost = compostToggle.isOn;

        Debug.Log("Starting simulation: " + simulator.province + " | " + simulator.totalDays + " days | Water: " + simulator.waterPerDay + " | Chemical: " + simulator.useChemicalfertiliser + " | Compost: " + simulator.useCompost);

        if (setupPanelRoot != null) setupPanelRoot.SetActive(false);
        if (statsPanel != null) statsPanel.SetActive(true);

        LockRotation(false);

        simulator.StartAutoSimulation();
    }

    void LockRotation(bool locked)
    {
        if (playerController != null)
        {
            playerController.isSimulating = !locked;
        }
    }

}
