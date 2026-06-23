using UnityEngine;
using UnityEngine.InputSystem;

public class CarrotSim : MonoBehaviour
{
    public GameObject[] growthStages;
    public int currentStage = 0;

    public float moisture = 50f;
    public float fertiliser = 30f;
    public float compost = 20f;
    public float weedInfestation = 0f;
    public float plantHealth = 100f;
    public GameObject dustPuffPrefab;

    public string province = "Northern Province";
    public EduPopup popupSystem;


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
        if (other.gameObject.name.Contains("fertiliser"))
        {
            float oldFert = fertiliser;
            fertiliser = Mathf.Min(fertiliser + 35f, 100f);
            if (dustPuffPrefab != null)
            {
                Instantiate(dustPuffPrefab, other.transform.position, Quaternion.identity);
            }
            Destroy(other.gameObject);

            if (oldFert >= 60f)
                popupSystem?.Show("TOO MUCH fertiliser", "Chemical burn risk!\n\n ACTION: Stop adding fertiliser. Wait for it to drain below 60%, OR throw compost (C key) to balance the soil.");
            else
                popupSystem?.Show("fertiliser ADDED", "+35% fertiliser.\n\n TIP: Optimal range is 30-60%. Stop here — adding more will burn roots.");
        }
        else if (other.gameObject.name.Contains("Compost"))
        {
            compost = Mathf.Min(compost + 40f, 100f);
            weedInfestation = Mathf.Max(weedInfestation - 15f, 0f);
            plantHealth = Mathf.Min(plantHealth + 5f, 100f);
            if (dustPuffPrefab != null)
            {
                Instantiate(dustPuffPrefab, other.transform.position, Quaternion.identity);
            }
            Destroy(other.gameObject);

            popupSystem?.Show("COMPOST ADDED", "+40% Compost, -15% Weeds, +5% Health.\n\n TIP: Composts has no upper limit. Throw more if weeds keep growing or fertiliser is too high.");
        }
    }

    public void SetStage(int stage)
    {
        currentStage = Mathf.Clamp(stage, 0, growthStages.Length - 1);
        UpdatePlantAppearance();
    }

    public void ClampStats()
    {
        moisture = Mathf.Clamp(moisture, 0f, 100f);
        fertiliser = Mathf.Clamp(fertiliser, 0f, 100f);
        compost = Mathf.Clamp(compost, 0f, 100f);
        weedInfestation = Mathf.Clamp(weedInfestation, 0f, 100f);
        plantHealth = Mathf.Clamp(plantHealth, 0f, 100f);
    }

    public void ResetField()
    {
        currentStage = 0;
        moisture = 50f;
        fertiliser = 30f;
        compost = 20f;
        weedInfestation = 0f;
        plantHealth = 100f;
        UpdatePlantAppearance();
        popupSystem?.Show("FIELD RESET", "Your plot has been cleared and replanted with fresh seeds. Try again!");
    }
    public void UpdatePlantAppearance()
    {
        for (int i = 0; i < growthStages.Length; i++)
        {
            if (growthStages[i] != null)
                growthStages[i].SetActive(i == currentStage);
        }
    }
}
