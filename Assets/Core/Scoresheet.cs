using System;
using System.Collections.Generic;
using ScoreCategoryEnum;
using UnityEngine;

public class ScoreSheet
{
    //TODO: Change to ScoreCategory instead of String
    private Dictionary<ScoreCategoryEnum.ScoreCategoryType, int> scorePerCategory;        

    public ScoreSheet()
    {
        scorePerCategory = new Dictionary<ScoreCategoryEnum.ScoreCategoryType, int>();
    }

    public ScoreSheet(Dictionary<ScoreCategoryEnum.ScoreCategoryType, int> scorePerCategory)
    {
        this.scorePerCategory = scorePerCategory;
    }

    public int CalculateUpperTotal()
    {
        int[] values = { getScore(ScoreCategoryEnum.ScoreCategoryType.ACE), getScore(ScoreCategoryEnum.ScoreCategoryType.TWO), getScore(ScoreCategoryEnum.ScoreCategoryType.THREE), getScore(ScoreCategoryEnum.ScoreCategoryType.FOUR), getScore(ScoreCategoryEnum.ScoreCategoryType.FIVE), getScore(ScoreCategoryEnum.ScoreCategoryType.SIX) };
        int total = 0;

        for (int i = 0; i < 5; i++)
        {
            if (values[i] != -1)
            {
                total += values[i];
            }
        }

        return total;
    }

    public int CalculateScore(List<int> dice, ScoreCategoryEnum.ScoreCategoryType category)
    {
        if (dice.Count > 6)
            return 0;
        int result = 0;
        if (category.Equals(ScoreCategoryEnum.ScoreCategoryType.ACE))
        {
            for (int i = 0; i < dice.Count; i++)
            {
                if (dice[i] == 1)
                {
                    result += dice[i];

                }
            }
            return result;
        }
        else if (category.Equals(ScoreCategoryEnum.ScoreCategoryType.TWO))
        {
            for (int i = 0; i < dice.Count; i++)
            {
                if (dice[i] == 2)
                {
                    result += dice[i];

                }

            }
            return result;
        }
        else if (category.Equals(ScoreCategoryEnum.ScoreCategoryType.THREE))
        {
            for (int i = 0; i < dice.Count; i++)
            {
                if (dice[i] == 3)
                {
                    result += dice[i];

                }
            }
            return result;
        }
        else if (category.Equals(ScoreCategoryEnum.ScoreCategoryType.FOUR))
        {
            for (int i = 0; i < dice.Count; i++)
            {
                if (dice[i] == 4)
                {
                    result += dice[i];

                }
            }
            return result;
        }
        else if (category.Equals(ScoreCategoryEnum.ScoreCategoryType.FIVE))
        {
            for (int i = 0; i < dice.Count; i++)
            {
                if (dice[i] == 5)
                {
                    result += dice[i];
                }
            }
            return result;
        }
        else if (category.Equals(ScoreCategoryEnum.ScoreCategoryType.SIX))
        {
            for (int i = 0; i < dice.Count; i++)
            {
                if (dice[i] == 6)
                {
                    result += dice[i];

                }
            }
            return result;
        }
        else if (category.Equals(ScoreCategoryEnum.ScoreCategoryType.THREE_A_KIND))
        {
            for (int i = 0; i < dice.Count; i++)
            {
                result += dice[i];

            }
            return ScoreCategory.isNOfKind(dice, 3) ? result : 0;
        }
        else if (category.Equals(ScoreCategoryEnum.ScoreCategoryType.FOUR_A_KIND))
        {
            for (int i = 0; i < dice.Count; i++)
            {
                result += dice[i];
            }
            return ScoreCategory.isNOfKind(dice, 4) ? result : 0;
        }
        else if (category.Equals(ScoreCategoryEnum.ScoreCategoryType.FULLHOUSE))
        {
            return ScoreCategory.isFullHouse(dice) ? 25 : 0;
        }
        else if (category.Equals(ScoreCategoryEnum.ScoreCategoryType.TINY_STRAIGHT))
        {
            return ScoreCategory.isTinyStraight(dice) ? 20 : 0;
        }
        else if (category.Equals(ScoreCategoryEnum.ScoreCategoryType.SMALL_STRAIGHT))
        {
            return ScoreCategory.isSmallStraight(dice) ? 30 : 0;
        }
        else if (category.Equals(ScoreCategoryEnum.ScoreCategoryType.LARGE_STRAIGHT))
        {
            return ScoreCategory.isLargeStraight(dice) ? 40 : 0;
        }
        else if (category.Equals(ScoreCategoryEnum.ScoreCategoryType.YAHTZEE))
        {
            return ScoreCategory.isYahtzee(dice) ? 50 : 0;
        }
        else if (category.Equals(ScoreCategoryEnum.ScoreCategoryType.CHANCE))
        {
            for (int i = 0; i < dice.Count; i++)
            {
                result += dice[i];
            }
            return result;
        }
        return 0;
    }

    public int getScore(ScoreCategoryEnum.ScoreCategoryType category)
    {
        try
        {
            return scorePerCategory[category];
        }
        catch (Exception e)
        {
            // throw new Exception("Cant find category");
            return -1;
        }
    }
   
    /*
     * 
     */
    public void saveScore(ScoreCategoryEnum.ScoreCategoryType category, int score, bool yahtzee)
    {
        if (scorePerCategory.ContainsKey(category))
            throw new Exception("Category is already filled");
        int bonusYahtzee = 0;
        scorePerCategory.TryGetValue(ScoreCategoryEnum.ScoreCategoryType.BONUS_YAHTZEE, out bonusYahtzee);
        if (yahtzee)
            if (checkYahtzee())
            {                                
                bonusYahtzee += 100;
                scorePerCategory[ScoreCategoryEnum.ScoreCategoryType.BONUS_YAHTZEE] = bonusYahtzee;
            }            
        scorePerCategory.Add(category, score);
        // Update lower section
        int lowerScore = 0;
        foreach (var pair in scorePerCategory)
            if (9 <= (int)pair.Key && (int)pair.Key <= 16)
                lowerScore += pair.Value;
        scorePerCategory[ScoreCategoryEnum.ScoreCategoryType.LOWER_SCORE] = lowerScore;
        // Update upper section
        int upperScore = 0;
        foreach (var pair in scorePerCategory)
            if (1 <= (int)pair.Key && (int)pair.Key <= 6)
                upperScore += pair.Value;
        scorePerCategory[ScoreCategoryEnum.ScoreCategoryType.UPPER_SCORE] = upperScore;
        int upperBonus = (upperScore >= 63) ? 35 : 0;
        scorePerCategory[ScoreCategoryEnum.ScoreCategoryType.UPPER_BONUS] = upperBonus;
        // Update total score
        int totalScore = lowerScore + upperScore + upperBonus + bonusYahtzee;
        scorePerCategory[ScoreCategoryEnum.ScoreCategoryType.TOTAL_SCORE] = totalScore;
    }

    /*
	* Check if the player has filled in Yahtzee or not.
	* Used when consider Joker rule.
	*/
    public bool checkYahtzeeFilled()
    {
        return getScore(ScoreCategoryType.YAHTZEE) != -1;
    }
        
    /*
     * Check if Yahtzee is filled with an actual Yahtzee.
     */
    public bool checkYahtzee()
    {
        if (!checkYahtzeeFilled())
            return false;
        return scorePerCategory[ScoreCategoryEnum.ScoreCategoryType.YAHTZEE] == 50;
    }

    public Dictionary<ScoreCategoryEnum.ScoreCategoryType, int> getDetailScore()
    {
        return new Dictionary<ScoreCategoryEnum.ScoreCategoryType, int>(scorePerCategory);
    }    
}
