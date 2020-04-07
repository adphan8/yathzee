using System;
using System.Collections.Generic;
using ScoreCategoryEnum;
using UnityEngine;

public class ScoreCategory{

    private String name;
    private int score;
    private int result;
    public ScoreCategory()
    {

    }
    public ScoreCategory(String name, int score){
        this.name = name;
        this.score = score;
    }
    public String getName(){
        return this.name;
    }
    public int getScore(){
        return this.score;
    }

    public static int CalculateScore(List<int> dice, ScoreCategoryEnum.ScoreCategoryType category)
    {
        dice.Sort();
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

    public static ScoreCategoryType convert(int value)
    {       
        foreach (var type in (ScoreCategoryEnum.ScoreCategoryType[])Enum.GetValues(typeof(ScoreCategoryEnum.ScoreCategoryType)))
        {
            if ((int)type == value)
                return type;
        }
        return ScoreCategoryEnum.ScoreCategoryType.CHANCE;
    }

    private static int haveMoreThan(List<int> a, int n)
    {
        int[] cnt = new int[7];
        for (int i = 1; i <= 6; i++)
            cnt[i] = 0;
        int ans = 0;
        foreach (int x in a)
        {
            cnt[x]++;
            if (cnt[x] == n)
                ans++;
        }
        return ans;
    }

    public static bool isNOfKind(List<int> a, int n)
    {
        return haveMoreThan(a, n) >= 1;
    }

    public static bool isFullHouse(List<int> a)
    {
        return haveMoreThan(a, 2) >= 2 && haveMoreThan(a, 3) >= 1;
    }

    public static bool isTinyStraight(List<int> a)
    {
        bool check = false;
        for(int i = 0; i< a.Count; i++)
        {
            for(int j = 0;j< a.Count; j++)
            {
                if(a[j] == (a[i]+1))
                {
                    for(int k = 0;k < a.Count; k++)
                    {
                        if(a[k] == (a[j] + 1))
                        {
                            check = true;
                            break;
                        }                            
                    }
                    if (check == true) break;
                }
            }
            if (check == true) break;
        }
        return check;
    }

    public static bool isSmallStraight(List<int> a)
    {        
        int[] cnt = new int[7];
        for (int i = 1; i <= 6; i++)
            cnt[i] = 0;
        foreach (int x in a)
            cnt[x]++;
        for (int i=1; i<=2; i++)
        {
            bool ok = true;
            for (int j = 0; j < 4; j++)
                if (cnt[i+j+1] == 0)
                    ok = false;
            if (ok)
                return ok;
        }
        return false;
    }

    public static bool isLargeStraight(List<int> a)
    {
        a.Sort();
        bool found = true;
        for (int i = 0; i < 4; i++)
            if (a[i] + 1 != a[i + 1])
                found = false;
        return found;        
    }

    public static bool isYahtzee(List<int> a)
    {
        return isNOfKind(a, 5);
    }

}
namespace ScoreCategoryEnum
{
    public enum ScoreCategoryType : byte
    {
        // UpperSection
        ACE = 1, TWO, THREE, FOUR, FIVE, SIX, UPPER_BONUS, UPPER_SCORE,
        // LowerSection
        THREE_A_KIND = 9, FOUR_A_KIND, FULLHOUSE, TINY_STRAIGHT, SMALL_STRAIGHT, LARGE_STRAIGHT, YAHTZEE, CHANCE, LOWER_SCORE,
        // Bonus
        BONUS_YAHTZEE, TOTAL_SCORE
    }    
}