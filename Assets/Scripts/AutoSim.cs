using UnityEngine;
using System.Collections;

public class AutoSim : MonoBehaviour
{
    public CarrotSim carrot;
    public SimOutcomes outcomes;
    public EduPopup popupSystem;

    public string province = "Northern Province";
    public int totalDays = 10;
    public float secondsPerDay = 2f;

    public float waterPerDay = 30f;
    public bool useChemicalfertiliser = false;
    public bool useCompost = false;

    public int currentDay = 0;
    private bool isRunning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartAutoSimulation()
    {
        if (isRunning) return;
        ResetStats();
        StartCoroutine(RunDays());
    }

    void ResetStats()
    {
        currentDay = 0;
        carrot.currentStage = 0;
        carrot.moisture = 50f;
        carrot.fertiliser = useChemicalfertiliser ? 70f : 30f;
        carrot.compost = useCompost ? 60f : 0f;
        carrot.weedInfestation = 0f;
        carrot.plantHealth = 100f;
        carrot.UpdatePlantAppearance();
    }

    IEnumerator RunDays()
    {
        isRunning = true;

        while (currentDay < totalDays && carrot.currentStage < 7)
        {
            yield return new WaitForSeconds(secondsPerDay);
            currentDay++;
            Debug.Log("--- Day " + currentDay + " ---");
            ProcessDay();

            if (carrot.currentStage == 7) break;
        }

        isRunning = false;
        ShowFinalResult();
    }

    void ProcessDay()
    {
        if (province == "Northern Province")
        {
            carrot.moisture += waterPerDay * 1.2f;
            carrot.moisture -= 10f;
        }
        else
        {
            carrot.moisture += waterPerDay;
            carrot.moisture -= 25f;
        }

        if (useChemicalfertiliser)
        {
            carrot.fertiliser -= 5f;
            carrot.weedInfestation += 15f;
        }
        else
        {
            carrot.fertiliser -= 10f;
        }

        if (useCompost)
        {
            carrot.compost -= 5f;
            carrot.weedInfestation -= 5f;
            carrot.plantHealth += 3f;
        }
        else
        {
            carrot.compost -= 8f;
            carrot.weedInfestation += 5f;
        }

        ClampAllStats();

        outcomes.CheckOutcomes(carrot, province, useChemicalfertiliser, useCompost, popupSystem);

        if (carrot.plantHealth > 30f && carrot.currentStage < 6)
        {
            if (currentDay % 2 == 0)
            {
                carrot.SetStage(carrot.currentStage + 1);
            }
        }
    }

    void ClampAllStats()
    {
        carrot.moisture = Mathf.Clamp(carrot.moisture, 0f, 100f);
        carrot.fertiliser = Mathf.Clamp(carrot.fertiliser, 0f, 100f);
        carrot.compost = Mathf.Clamp(carrot.compost, 0f, 100f);
        carrot.weedInfestation = Mathf.Clamp(carrot.weedInfestation, 0f, 100f);
        carrot.plantHealth = Mathf.Clamp(carrot.plantHealth, 0f, 100f);
    }

    void ShowFinalResult()
    {
        if (carrot.currentStage == 6)
        {
            popupSystem.Show("SUCCESS!", "Your carrots reached full maturity! Balanced water, fertiliser, and compost gave you a healthy harvest.");
        }
        else if (carrot.currentStage == 7)
        {
            popupSystem.Show("CROP FAILED", "Your carrots did not survive. Check the messages to see what went wrong.");
        }
        else
        {
            popupSystem.Show("TIME UP", "The season ended before your carrots fully grew. Try a longer time limit or better care.");
        }
    }

}
