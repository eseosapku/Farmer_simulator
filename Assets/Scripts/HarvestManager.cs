using UnityEngine;

public class HarvestManager : MonoBehaviour
{
    public CarrotSim carrot;
    public EconomyManager economy;
    public EduPopup popup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryHarvest()
    {
        if (carrot == null || economy == null) return;

        if (carrot.currentStage == 6) 
        {
            economy.HarvestCarrots(36);
            carrot.SetStage(0); 
            carrot.moisture = 50f;
            carrot.fertiliser = 30f;
            carrot.compost = 20f;
            carrot.weedInfestation = 0f;
            carrot.plantHealth = 100f;
        }
        else if (carrot.currentStage == 7)
        {
            if (popup != null) popup.Show("DEAD CROP", "These carrots died. Clear the field and plant again — but you'll lose them.");
        }
        else
        {
            if (popup != null) popup.Show("NOT READY", "Your carrots aren't fully grown yet. Wait for them to reach the harvest stage.");
        }
    }
}
