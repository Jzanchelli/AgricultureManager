﻿
public class State
{
    public State(string name, int numCows, int numGrains, int dollars) {
        this.name = name;
        this.numCows = numCows;
        this.numGrains = numGrains;
        this.dollars = dollars;
        this.co2Emissions = (numCows * .5f) + (numGrains * .1f);
    }

    public string name;
    public int numCows;
    public int numGrains;
    public int dollars;
    public float co2Emissions;
}