using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simulator : MonoBehaviour
{
    public Text yearText;
    public Text moneyText;
    public Text co2Text;
    public Text staticDisasterText;
    public Text disasterText;
    public Button button;

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
    private Queue<Text> texts;

    public GameObject male;
    public GameObject female;

    void Start()
    {
        // Toggle the correct picture
        male.SetActive(false);
        female.SetActive(false);
        if(DataManager.playerCharacter == "Male") {
            male.SetActive(true);
        }
        else {
            female.SetActive(true);
        }


        texts = new Queue<Text>();

        yearText.GetComponent<CanvasGroup>().alpha = 0;
        moneyText.GetComponent<CanvasGroup>().alpha = 0;
        co2Text.GetComponent<CanvasGroup>().alpha = 0;
        staticDisasterText.GetComponent<CanvasGroup>().alpha = 0;
        disasterText.GetComponent<CanvasGroup>().alpha = 0;
        button.GetComponent<CanvasGroup>().alpha = 0;
        button.interactable = false;

        texts.Enqueue(yearText);
        texts.Enqueue(moneyText);
        texts.Enqueue(co2Text);
        texts.Enqueue(staticDisasterText);
        texts.Enqueue(disasterText);

        yearText.text = $"Results from Year {DataManager.currentYear}";
        stateList = DataManager.GetStates();
        RunSimulation();

        FadeIns();
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
        string disasterUiText = "None";
        
        //TODO: Update visuals if we get a disaster
        foreach(string disaster in disasters)
        {
            disasterUiText = "";    
            if (disaster == "drought")
            {
                disasterUiText += "Drought\n";
                if (DataManager.brownState.numCows > 0 && DataManager.orangeState.numCows > 0)
                {
                    DataManager.brownState.numCows -= 1;
                    DataManager.orangeState.numCows -= 1;
                }
            }
            else if (disaster == "fire")
            {
                disasterUiText += "Fire\n";
                //Yellow lose some grains
                if (DataManager.yellowState.numGrains > 0) {
                    DataManager.yellowState.numGrains -= 1;
                }
            }
            else if (disaster == "flood") {
                disasterUiText += "Flood\n";
                //Coastal cities lose cattle
                if (DataManager.blueState.numCows > 0 && DataManager.aquaState.numCows > 0)
                {
                    DataManager.aquaState.numCows -= 1;
                    DataManager.blueState.numCows -= 1;
                }
            }
        }

        disasterText.text = disasterUiText;

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

    private void FadeIns() {
        if(texts.Count > 0) {
            Text element = texts.Dequeue();

            // Start the fade in processes
            CanvasGroup canvas = element.GetComponent<CanvasGroup>();

            // Fade in the year text
            LeanTween.value(gameObject,
                (val) => canvas.alpha = val,
                0, 1, 0.2f)
                .setOnComplete(() => FadeIns());
        }
        else {
            // Fade in button
            CanvasGroup canvas = button.GetComponent<CanvasGroup>();
            // Fade in the year text
            LeanTween.value(gameObject,
                (val) => canvas.alpha = val,
                0, 1, 0.2f)
                .setOnComplete(() => button.interactable = true);
        }
        
    }
}
