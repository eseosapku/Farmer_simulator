using UnityEngine;

public class SimOutcomes : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CheckOutcomes(CarrotSim carrot, string province, bool useChemical, bool useCompost, EduPopup popup)
    {
        if (province == "Northern Province" && carrot.moisture >= 90f)
        {
            popup.Show("WATERLOGGED!", "Carrots in Northern Province rot easily. Only water until moisture hits 70%, not 100%. Your plants drowned.");
            carrot.SetStage(7);
            carrot.plantHealth = 0f;
            return;
        }

        if (province == "Eastern Province" && carrot.moisture <= 15f)
        {
            popup.Show("DROUGHT!", "Eastern Province soil dries fast. You didn't water enough. Plants are withering.");
            if (carrot.currentStage > 0) carrot.SetStage(carrot.currentStage - 1);
            carrot.plantHealth -= 30f;
            if (carrot.plantHealth <= 0f) carrot.SetStage(7);
            return;
        }

        if (useChemical && !useCompost && carrot.weedInfestation >= 70f)
        {
            popup.Show("WEEDS TOOK OVER!", "Chemical fertilizer alone makes weeds explode. Organic compost makes plants more pest-resistant. Your crop is choked.");
            carrot.SetStage(7);
            carrot.plantHealth = 0f;
            return;
        }

        if (!useChemical && !useCompost && carrot.fertilizer <= 10f)
        {
            popup.Show("LOW NUTRIENTS", "Your soil is running out of nutrients. Add fertilizer or compost to keep plants healthy.");
            carrot.plantHealth -= 15f;
        }
    }
}
