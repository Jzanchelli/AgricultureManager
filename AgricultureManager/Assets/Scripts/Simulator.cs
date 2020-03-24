using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simulator : MonoBehaviour
{
    public Text yearText;
    public Text moneyText;
    public Text co2Text;
    // Start is called before the first frame update
    private List<State> stateList;
    void Start()
    {
        yearText.text = $"Results from Year {DataManager.currentYear}";
        stateList = DataManager.GetStates();
        RunSimulation();
    }

    // Resolve everything that the player did on the previous page
    void RunSimulation() {
        int totalMoney = 0;
        float totalCo2 = 0;
        // Compute state costs
        // Compute CO2 costs
        foreach (State state in stateList) {
            totalMoney += state.dollars;
            totalCo2 += state.co2Emissions;
        }

        moneyText.text = $"Total Cash: ${totalMoney}";
        co2Text.text = $"Total CO2: {totalCo2}";
        
        // Run random effects

        // Update values
        DataManager.currentYear += 1;
    }

}
