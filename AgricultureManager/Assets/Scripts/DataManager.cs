using System.Reflection; // For accessing properties of this DataManager dynamically
using System.Collections.Generic;
using UnityEngine;

// Pulling from the first suggestion here: https://gamedev.stackexchange.com/questions/110958/what-is-the-proper-way-to-handle-data-between-scenes
// Access data from this script anywhere using DataManager.blueState...
public static class DataManager
{
    public static State aquaState { get; set; }
    public static State blueState { get; set; }
    public static State brownState { get; set; }
    public static State darkGreenState { get; set; }
    public static State greenState { get; set; }
    public static State lightGreenState { get; set; }
    public static State yellowState { get; set; }
    public static State orangeState { get; set; }

    public static int currentYear { get; set; }
    public static int droughtChance { get; set; }
    public static int floodingChance { get; set; }
    public static int fireChance { get; set; }

    private static PropertyInfo[] properties;

    static DataManager() {
        Reset();

        properties = typeof(DataManager).GetProperties();
    }

    /**
     * Returns the sum of cash values from each state
     */
    public static int GetTotalCash() {
        int totalCash = 0;
        foreach(var property in properties) {
            if(property.PropertyType == typeof(State)) {
                State state = (State)property.GetValue(null);
                totalCash += state.dollars;
            }
        }

        return totalCash;
    }

    /**
     * Returns the sum of co2 emissiosn from each state
     */
    public static float GetTotalCo2() {
        float co2EmissionsTotal = 0;
        foreach(PropertyInfo property in properties) {
            if(property.PropertyType == typeof(State)) {
                State state = (State)property.GetValue(null);
                co2EmissionsTotal += state.co2Emissions;
            }
        }

        return co2EmissionsTotal;
    }

    /**
     * Returns a list of weather effects, or an empty list otherwise.
     * Potential options are: drought, flood, fire
     */
    public static List<string> randomDisasters() {
        List<string> disasterList = new List<string>();
        int droughtRoll = Random.Range(1, 101);
        int floodRoll = Random.Range(1, 101);
        int fireRoll = Random.Range(1, 101);

        if(droughtRoll <= droughtChance) {
            disasterList.Add("drought");
        }
        if(floodRoll <= floodingChance) {
            disasterList.Add("flood");
        }
        if(fireRoll <= fireChance) {
            disasterList.Add("fire");
        }

        return disasterList;
    }

    public static List<State> GetStates() {
        List<State> states = new List<State>();
        foreach(var property in properties) {
            if(property.PropertyType == typeof(State)) {
                states.Add((State)property.GetValue(null));
            }
        }

        return states;
    }

    public static void Reset() {
        int startingCows = 2;
        int startingGrains = 2;
        int startingDollars = 50;

        // Order is (Name, NumCows, NumGrains, StartingDollars)
        aquaState = new State("Aqua", startingCows, startingGrains, startingDollars);
        blueState = new State("Blue", startingCows, startingGrains, startingDollars);
        brownState = new State("Brown", startingCows, startingGrains, startingDollars);
        darkGreenState = new State("Dark Green", startingCows, startingGrains, startingDollars);
        greenState = new State("Green", startingCows, startingGrains, startingDollars);
        lightGreenState = new State("Light Green", startingCows, startingGrains, startingDollars);
        yellowState = new State("Yellow", startingCows, startingGrains, startingDollars);
        orangeState = new State("Orange", startingCows, startingGrains, startingDollars);

        currentYear = 1;

        droughtChance = 5;
        floodingChance = 5;
        fireChance = 5;
    }
}
