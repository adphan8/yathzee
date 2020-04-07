using System;
using System.Collections.Generic;
using ScoreCategoryEnum;

public class Move{
    private Player player;
    private ScoreCategoryType scoreCategory;
    List<int> diceValue = new List<int>();

    public Move(Player player, ScoreCategoryType scoreCategory, List<int> diceValue ){
        //TODO: fill this out.
    }

    public Player getPlayer(){
        //TODO: fill this out.
        return null;
    }
    public ScoreCategoryType getScoreCategory(){
        //TODO: fill this out.
        return scoreCategory;
    }

    public List<int> getDiceValue(){
        //TODO: return a copy of diceValues.
        return null;
    }

    void calculateScore(ScoreCategoryType scoreCategory, List<int> diceValue){
        //TODO: calculate score based on dice value and score categories.
    }

    public String toString(){
        //TODO: fill this out'
        return "";
    }
}