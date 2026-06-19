using UnityEngine;

public class DayCycle : MonoBehaviour
{

    public CarrotSim carrot;
    public FailureTracker failures;
    public int currentDay = 0;
    public bool manualModeActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SkipOneDay()
    {
        currentDay++;
        ProcessDay();
    }

    public void ProcessDay()
    {
        DrainStats();
        carrot.ClampStats();
        int optimalCount = CountOptimalStats();
        failures.CheckFailures(carrot, optimalCount);
        if (carrot.plantHealth > 0f && carrot.currentStage < 6) DecideGrowth(optimalCount);
        carrot.UpdatePlantAppearance();
        Debug.Log("Day " + currentDay + " | Optimal stats: " + optimalCount);
    }

    void DrainStats()
    {
        if (carrot.province == "Northern Province")
        {
            carrot.moisture -= 6f;
            carrot.fertilizer -= 5f;
            carrot.compost -= 3f;
            carrot.weedInfestation += Random.Range(4f, 8f);
        }
        else
        {
            carrot.moisture -= 14f;
            carrot.fertilizer -= 8f;
            carrot.compost -= 5f;
            carrot.weedInfestation += Random.Range(3f, 6f);
        }
    }

    public int CountOptimalStats()
    {
        int count = 0;
        float upperWater = (carrot.province == "Northern Province") ? 70f : 75f;
        if (carrot.moisture >= 40f && carrot.moisture <= upperWater) count++;
        if (carrot.fertilizer >= 30f && carrot.fertilizer <= 60f) count++;
        if (carrot.compost >= 30f) count++;
        if (carrot.weedInfestation < 30f) count++;
        return count;
    }

    void DecideGrowth(int optimalCount)
    {
        if (optimalCount == 4)
        {
            int newStage = Mathf.Min(carrot.currentStage + 2, 6);
            if (newStage != carrot.currentStage)
            {
                carrot.currentStage = newStage;
                carrot.popupSystem?.Show("EXCELLENT GROWTH!", "All optimal — jumped 2 stages today.");
            }
        }
        else if (optimalCount == 3 && carrot.currentStage < 6)
        {
            carrot.currentStage++;
        }
        else if (optimalCount == 2 && currentDay % 2 == 0 && carrot.currentStage < 6)
        {
            carrot.currentStage++;
        }
    }
    public void ResetDays()
    {
        currentDay = 0;
    }

}
