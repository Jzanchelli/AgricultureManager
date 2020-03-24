using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simulator : MonoBehaviour
{
    public Text yearText;
    public Text moneyText;
    public Text co2Text;

    public int cowDeathChance = 25;
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
        RunCowDeath();
        List<string> disasters = DataManager.randomDisasters();

        // Update values
        DataManager.currentYear += 1;
    }

    public void ResetGrainCount() {
        foreach(State state in stateList) {
            state.numGrains = 0;
        }
    }

    public void RunCowDeath() {
        foreach(State state in stateList) {
            int deaths = 0;
            for(int i = 0; i < state.numCows; ++i) {
                if(Random.Range(1, 101) <= cowDeathChance) {
                    deaths++;
                }
            }

            state.numCows -= deaths;
        }
    }
}
