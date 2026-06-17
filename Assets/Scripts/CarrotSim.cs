using UnityEngine;
using UnityEngine.InputSystem;

public class CarrotSim : MonoBehaviour
{
    public GameObject[] growthStages;
    public int currentStage = 0;
    public float moisture = 50f;
    public float fertilizer = 30f;
    public float compost = 20f;
    public float weedInfestation = 0f;
    public float plantHealth = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdatePlantAppearance();
    }


    // Update is called once per frame
    void Update()
    {

    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Fertilizer"))
        {
            fertilizer = Mathf.Min(fertilizer + 35f, 100f);
            plantHealth = Mathf.Min(plantHealth + 10f, 100f);
            Debug.Log("Fertilizer dropped into the carrot patch!");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.name.Contains("Compost"))
        {
            compost = Mathf.Min(compost + 40f, 100f);
            weedInfestation = Mathf.Max(weedInfestation - 15f, 0f);
            Debug.Log("Compost dropped into the carrot patch!");
            Destroy(other.gameObject);
        }
    }

    public void TestingForceGrowth()
    {
        if (currentStage < growthStages.Length - 1)
        {
            currentStage++;
        }
        UpdatePlantAppearance();
    }

    public void AdvanceSimulationCycle(float regionalDryModifier)
    {
        moisture -= 15f * regionalDryModifier;
        fertilizer -= 10f;
        compost -= 8f;
        weedInfestation += Random.Range(5f, 15f);

        moisture = Mathf.Clamp(moisture, 0f, 100f);
        fertilizer = Mathf.Clamp(fertilizer, 0f, 100f);
        compost = Mathf.Clamp(compost, 0f, 100f);
        weedInfestation = Mathf.Clamp(weedInfestation, 0f, 100f);

        EvaluatePlantHealth();

        if (plantHealth > 30f && moisture > 20f && moisture < 85f)
        {
            if (currentStage < growthStages.Length - 1)
            {
                currentStage++;
                Debug.Log("Your crop field progressed to growth cycle: " + currentStage);
            }
        }
        else
        {
            if (plantHealth <= 0f)
            {
                Debug.Log("The crops withered due to poor environment conditions.");
            }
            else if (currentStage > 0)
            {
                currentStage--;
                Debug.Log("Crops decaying due to poor metrics. Regression triggered.");
            }
        }

        UpdatePlantAppearance();
    }

    public void SetStage(int stage)
    {
        currentStage = Mathf.Clamp(stage, 0, growthStages.Length - 1);
        UpdatePlantAppearance();
    }

    public void EvaluatePlantHealth()
    {
        if (moisture <= 10f || moisture >= 90f) plantHealth -= 15f;
        if (fertilizer <= 5f && compost <= 5f) plantHealth -= 10f;
        if (weedInfestation >= 60f) plantHealth -= 12f;

        if (moisture > 30f && moisture < 75f && fertilizer > 40f)
        {
            plantHealth = Mathf.Min(plantHealth + 15f, 100f);
        }

        plantHealth = Mathf.Clamp(plantHealth, 0f, 100f);
    }

    public void UpdatePlantAppearance()
    {
        for (int i = 0; i < growthStages.Length; i++)
        {
            if (growthStages[i] != null)
            {
                growthStages[i].SetActive(i == currentStage);
            }
        }
    }
}
