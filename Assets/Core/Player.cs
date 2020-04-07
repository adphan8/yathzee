using System;
using System.Collections.Generic;
using ScoreCategoryEnum;

public class Player
{
    private String name;
    private ScoreSheet scoreSheet;
    private int order = 0;
    // TODO: change language to enumerate
    private String language;

    public Player(String name, String language)
    {
        this.name = name;
        this.language = language;
        this.scoreSheet = new ScoreSheet();
    }

    public Player(String name) : this(name, "English") { }

    public Player(String name, Dictionary<ScoreCategoryEnum.ScoreCategoryType, int> scorePerCategory)
    {
        this.name = name;
        this.scoreSheet = new ScoreSheet(scorePerCategory);
    }

    public string GetName()
    {
        return name;
    }

    public int getScore(ScoreCategoryEnum.ScoreCategoryType a)
    {
        return scoreSheet.getScore(a);
    }

    public int calculateScore(List<int> dice, ScoreCategoryEnum.ScoreCategoryType category)
    {
        return scoreSheet.CalculateScore(dice, category);
    }
    public int getUpperTotal()
    {
        return scoreSheet.CalculateUpperTotal();
    }

    public Dictionary<ScoreCategoryEnum.ScoreCategoryType, int> saveScore(ScoreCategoryEnum.ScoreCategoryType category, int score, bool yahtzee)
    {
        scoreSheet.saveScore(category, score, yahtzee);
        return scoreSheet.getDetailScore();
    }

    public bool isYahtzeeFilled()
    {
        return scoreSheet.checkYahtzeeFilled();
    }

    public void setLanguage(string language)
    {
        this.language = language;
    }

    public void setOrder(int order)
    {
        this.order = order;
    }

    public int getOrder()
    { 
        return this.order;
    }
}
