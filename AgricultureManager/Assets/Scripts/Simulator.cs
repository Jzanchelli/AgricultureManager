using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simulator : MonoBehaviour
{
    public Text yearText;
    public Text moneyText;
    public Text co2Text;
    public Text diasterText;

    [Range(1, 100)]
    public int cowDeathChance = 25;
    public float moneyFromCows = 1f;
    public float moneyFromGrain = 2f;
    public float co2FromCows = 0.5f;
    public float co2FromGrain = 0.1f;
    public float moneyLostFromco2 = .1f;
    public int operatingCosts = 20;

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

        // Increase the state dollar amounts based on their number of cows/grains
        foreach (State state in stateList) {
            state.dollars += (int) Mathf.Floor(state.numCows * moneyFromCows + state.numGrains + moneyFromGrain);
            //Operating costs
            state.dollars -= (int) Mathf.Floor(state.co2Emissions * moneyLostFromco2) + operatingCosts;
            //Calculate co2 from cows
            state.co2Emissions += (state.numCows * co2FromCows) + (state.numGrains * co2FromGrain);
        }

        foreach (State state in stateList)
        {
            totalMoney += state.dollars;
            totalCo2 += state.co2Emissions;
        }
        
        moneyText.text = $"Total Cash: ${totalMoney}";
        co2Text.text = $"Total CO2: {totalCo2}";
        // Run random effects
        RunCowDeath();
        List<string> disasters = DataManager.randomDisasters();
        diasterText.text = $"Natural Diasters: {disasters.Count}";
        //TODO: Update visuals if we get a disaster
        foreach(string disaster in disasters)
        {
            if (disaster == "drought")
            {
                
                if (DataManager.brownState.numCows > 0 && DataManager.orangeState.numCows > 0)
                {
                    DataManager.brownState.numCows -= 1;
                    DataManager.orangeState.numCows -= 1;
                }
            }
            else if (disaster == "fire")
            {
                //Yellow lose some grains
                if (DataManager.yellowState.numGrains > 0) {
                    DataManager.yellowState.numGrains -= 1;
                }
            }
            else if (disaster == "flood") {
                //Coastal cities lose cattle
                if (DataManager.blueState.numCows > 0 && DataManager.aquaState.numCows > 0)
                {
                    DataManager.aquaState.numCows -= 1;
                    DataManager.blueState.numCows -= 1;
                }
            }
        }
        // Update values
        DataManager.currentYear += 1;
    }

    public void ResetGrainCount() {
        foreach(State state in stateList) {
            state.numGrains = 0;
        }
    }

    private void RunCowDeath() {
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

    private void IncreaseMoney() {
        
    }
}
