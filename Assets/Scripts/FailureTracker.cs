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
        burnDays = (carrot.fertiliser >= 75f) ? burnDays + 1 : 0;
        chokeDays = (carrot.weedInfestation >= 50f && carrot.compost < 20f) ? chokeDays + 1 : 0;

        if (droughtDays == 1) carrot.popupSystem?.Show("DROUGHT WARNING", "Moisture too low.\n\n ACTION: Click and hold on the field to water now, OR throw compost to help retain moisture.");
        if (floodDays == 1) carrot.popupSystem?.Show("WATERLOGGED!", "Moisture too high.\n\n ACTION: STOP watering for 2-3 days. Press Skip Day to let it dry out naturally.");
        if (burnDays == 1) carrot.popupSystem?.Show("CHEMICAL BURN!", "fertiliser above 75%.\n\n ACTION: Stop throwing fertiliser bags. Throw COMPOST (press C) instead — it dilutes chemical buildup safely.");
        if (chokeDays == 1) carrot.popupSystem?.Show("WEEDS CHOKING", "Weeds spreading fast.\n\n ACTION: Press C to throw a Compost bag — it kills 15% of weeds instantly and strengthens your plant.");

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
