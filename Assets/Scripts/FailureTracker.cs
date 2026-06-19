using UnityEngine;

public class FailureTracker : MonoBehaviour
{
    private int droughtDays = 0;
    private int floodDays = 0;
    private int burnDays = 0;
    private int chokeDays = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckFailures(CarrotSim carrot, int optimalCount)
    {
        float droughtLimit = (carrot.province == "Eastern Province") ? 35f : 30f;
        float floodLimit = (carrot.province == "Northern Province") ? 80f : 90f;

        droughtDays = (carrot.moisture <= droughtLimit) ? droughtDays + 1 : 0;
        floodDays = (carrot.moisture >= floodLimit) ? floodDays + 1 : 0;
        burnDays = (carrot.fertilizer >= 75f) ? burnDays + 1 : 0;
        chokeDays = (carrot.weedInfestation >= 50f && carrot.compost < 20f) ? chokeDays + 1 : 0;

        if (droughtDays == 1) carrot.popupSystem?.Show("DROUGHT WARNING", "Moisture too low. Water within 3 days or crops wither.");
        if (floodDays == 1) carrot.popupSystem?.Show("WATERLOGGED!", "Moisture too high. Rotting risk in 2 days.");
        if (burnDays == 1) carrot.popupSystem?.Show("CHEMICAL BURN!", "Fertilizer above 75%. Keep at 30-60%.");
        if (chokeDays == 1) carrot.popupSystem?.Show("WEEDS CHOKING", "Throw compost to suppress weeds.");

        if (burnDays > 0) { carrot.plantHealth -= 15f; carrot.weedInfestation += 10f; }
        if (chokeDays > 0) carrot.plantHealth -= 10f;

        if (droughtDays >= 3) Kill(carrot, "CROP WITHERED", "Three days of drought killed your carrots.");
        if (floodDays >= 2) Kill(carrot, "CROP ROTTED", "Two days waterlogged killed your carrots.");
        if (burnDays >= 3) Kill(carrot, "ROOTS DESTROYED", "Sustained chemical burn killed your crop.");
        if (chokeDays >= 4) Kill(carrot, "CHOKED OUT", "Weeds overran your crop.");

        if (optimalCount >= 3) carrot.plantHealth += 8f;
        else if (optimalCount <= 1) carrot.plantHealth -= 10f;

        carrot.ClampStats();
        if (carrot.plantHealth <= 0f) carrot.SetStage(7);
    }

    void Kill(CarrotSim carrot, string title, string msg)
    {
        carrot.popupSystem?.Show(title, msg);
        carrot.SetStage(7);
        carrot.plantHealth = 0f;
    }

}
