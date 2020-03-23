// Pulling from the first suggestion here: https://gamedev.stackexchange.com/questions/110958/what-is-the-proper-way-to-handle-data-between-scenes
// Access data from this script anywhere using DataManager.blueState...
public static class DataManager
{
    public static State blueState { get; set; }
    public static State lightBlueState { get; set; }
    public static State greenState { get; set; }
    public static State redState { get; set; }
    public static State yellowState { get; set; }
    public static State whiteState { get; set; }
    public static State darkGreenState { get; set; }
    public static State greyState { get; set; }
    public static int currentYear { get; set; }

    public static int droughtChance { get; set; }
    public static int floodingChance { get; set; }
    public static int fireChance { get; set; }

    static DataManager() {
        // Order is (Name, NumCows, NumGrains, StartingDollars)
        blueState = new State("Blue", 10, 10, 10);
        lightBlueState = new State("Lightblue", 10, 10, 10);
        greenState = new State("Green", 10, 10, 10);
        redState = new State("Red", 10, 10, 10);
        yellowState = new State("Yellow", 10, 10, 10);
        whiteState = new State("White", 10, 10, 10);
        darkGreenState = new State("DarkGreen", 10, 10, 10);
        greyState = new State("Grey", 10, 10, 10);

        currentYear = 1;

        droughtChance = 5;
        floodingChance = 5;
        fireChance = 5;
    }

    public static int GetTotalCash() {
        return blueState.dollars + lightBlueState.dollars +
            greenState.dollars + redState.dollars +
            yellowState.dollars + whiteState.dollars +
            darkGreenState.dollars + greyState.dollars;
    }

    public static float GetTotalCo2() {
        return blueState.co2Emissions + lightBlueState.co2Emissions +
            greenState.co2Emissions + redState.co2Emissions +
            yellowState.co2Emissions + whiteState.co2Emissions +
            darkGreenState.co2Emissions + greyState.co2Emissions;
    }
}
