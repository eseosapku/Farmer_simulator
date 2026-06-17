using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimSetup : MonoBehaviour
{
    public AutoSim simulator;
    public MenuNavigation menuNavigation;

    public Slider waterSlider;
    public TMP_Text waterLabel;
    public Toggle chemicalToggle;
    public Toggle compostToggle;
    public TMP_Text dayCounterText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
    }

    public void OnSimulateButtonClicked()
    {
        if (simulator == null || menuNavigation == null) return;

        simulator.province = menuNavigation.Province;
        simulator.totalDays = menuNavigation.simDays;
        simulator.waterPerDay = waterSlider.value;
        simulator.useChemicalFertilizer = chemicalToggle.isOn;
        simulator.useCompost = compostToggle.isOn;

        Debug.Log("Starting simulation: " + simulator.province + " | " + simulator.totalDays + " days | Water: " + simulator.waterPerDay + " | Chemical: " + simulator.useChemicalFertilizer + " | Compost: " + simulator.useCompost);

        simulator.StartAutoSimulation();
    }
}
