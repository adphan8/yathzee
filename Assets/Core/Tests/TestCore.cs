using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using ScoreCategoryEnum;

public class TestCore
{

    private Game game;    

    private bool checkSameList(List<int> a, List<int> b)
    {
        if (a == null || b == null)
            return false;        
        a.Sort();
        b.Sort();
        if (a.Count != b.Count)
            return false;
        for (int i = 0; i < a.Count; i++) 
            if (a[i] != b[i])
            {                
                return false;
            }                
        return true;
    }

    [SetUp]
    public void Setup()
    {
        List<string> names = new List<string>(3);
        for (int i = 0; i < 6; i++) 
        {
            names.Add("Player" + (i + 1));
        }     
        game = new Game(names);
    }

    // Roll dice test one time.
    // Testcase 2
    [Test]
    public void Test02_RollDiceOne()
    {
        List<int> dices = game.rollDice();
        Assert.AreEqual(dices.Count, 5);
        List<int> gameDices = game.getDices();
        Assert.True(checkSameList(gameDices, dices));
    }

    // Roll dice three time without saving.
    // Testcase 3
    [Test]
    public void Test03_RollDiceThree()
    {
        List<int> dices, gameDices;
        for (int i = 0; i < 3; i++) 
        {
            dices = game.rollDice();
            Assert.AreEqual(dices.Count, 5);
            gameDices = game.getDices();
            Assert.True(checkSameList(gameDices, dices));
        }       
    }

    // Roll dice, then save dice 0, then roll again.
    [Test]
    public void Test04_SaveOneDice()
    {
        List<int> dices = game.rollDice();
        List<int> saves = game.saveDice(dices[0]);
        dices.RemoveAt(0);
        List<int> gameDices = game.getDices();
        List<int> gameSaveDices = game.getSaveDices();
        Assert.True(checkSameList(dices, gameDices));
        Assert.True(checkSameList(saves, gameSaveDices));
        Assert.AreEqual(dices.Count + saves.Count, 5);
        dices = game.rollDice();
        Assert.True(checkSameList(dices, game.getDices()));
        Assert.AreEqual(dices.Count, 4);
    }

    [Test]
    public void Test05_SaveAllDices()
    {
        List<int> dices = game.rollDice();
        List<int> saves = null;
        foreach (var dice in dices)
            saves = game.saveDice(dice);
        Assert.True(checkSameList(saves, dices));
        Assert.True(checkSameList(saves, game.getSaveDices()));
        Assert.True(checkSameList(new List<int>(), game.getDices()));
        dices = game.rollDice();
        Assert.True(checkSameList(new List<int>(), dices));
    }

    // Roll dice, save dices, then unsave dices.
    [Test]
    public void Test06_UnsavedDice()
    {
        List<int> dices = game.rollDice();
        game.saveDice(dices[0]);
        List<int> saves = game.saveDice(dices[3]);
        Assert.True(dices.Remove(dices[3]));
        Assert.True(dices.Remove(dices[0]));
        Assert.True(checkSameList(dices, game.getDices()));
        Assert.True(checkSameList(saves, game.getSaveDices()));
        Assert.AreEqual(game.getSaveDices().Count, 2);
        Assert.AreEqual(game.getDices().Count, 3);
        saves = game.unsaveDice(saves[0]);
        dices = game.rollDice();
        Assert.True(checkSameList(dices, game.getDices()));
        Assert.True(checkSameList(saves, game.getSaveDices()));
        Assert.AreEqual(game.getSaveDices().Count, 1);
        Assert.AreEqual(game.getDices().Count, 4);
    }

    [Test]
    public void Test07_CalculateScoreUpperSection()
    {
        // Upper section test
        game.resetDices(new List<int>() { 1, 2, 3, 3, 1 });
        Assert.AreEqual(game.calculateScore(ScoreCategoryEnum.ScoreCategoryType.ACE), 2);
        Assert.AreEqual(game.calculateScore(ScoreCategoryEnum.ScoreCategoryType.TWO), 2);
        Assert.AreEqual(game.calculateScore(ScoreCategoryEnum.ScoreCategoryType.THREE), 6);
        Assert.AreEqual(game.calculateScore(ScoreCategoryEnum.ScoreCategoryType.FOUR), 0);
        Assert.AreEqual(game.calculateScore(ScoreCategoryEnum.ScoreCategoryType.FIVE), 0);
        Assert.AreEqual(game.calculateScore(ScoreCategoryEnum.ScoreCategoryType.SIX), 0);
        Assert.AreEqual(game.calculateScore(ScoreCategoryEnum.ScoreCategoryType.SIX), 0);
        Assert.AreEqual(game.calculateScore(ScoreCategoryEnum.ScoreCategoryType.SIX), 0);
    }
    
    [Test]
    public void Test08_CalculateScoreLowerSection()
    {
        // Lower section test
        game.resetDices(new List<int>() { 6, 6, 5, 5, 6 });
        Assert.AreEqual(game.calculateScore(ScoreCategoryEnum.ScoreCategoryType.THREE_A_KIND), 28);
        Assert.AreEqual(game.calculateScore(ScoreCategoryEnum.ScoreCategoryType.FOUR_A_KIND), 0);
        Assert.AreEqual(game.calculateScore(ScoreCategoryEnum.ScoreCategoryType.FULLHOUSE), 25);
        Assert.AreEqual(game.calculateScore(ScoreCategoryEnum.ScoreCategoryType.SMALL_STRAIGHT), 0);
        Assert.AreEqual(game.calculateScore(ScoreCategoryEnum.ScoreCategoryType.LARGE_STRAIGHT), 0);
        Assert.AreEqual(game.calculateScore(ScoreCategoryEnum.ScoreCategoryType.YAHTZEE), 0);
        Assert.AreEqual(game.calculateScore(ScoreCategoryEnum.ScoreCategoryType.CHANCE), 28);
        Assert.AreEqual(game.calculateScore(ScoreCategoryEnum.ScoreCategoryType.TINY_STRAIGHT), 0);
    }

    [Test]
    public void Test09_ChooseCategoryAndSaveScore()
    {
        game = new Game(new List<string>() { "one player" });
        game.resetDices(new List<int>() { 1, 3, 4, 5, 1 });
        game.chooseCategory(ScoreCategoryEnum.ScoreCategoryType.TINY_STRAIGHT);
        game.resetDices(new List<int>() { 1, 1, 2, 2, 2 });
        game.chooseCategory(ScoreCategoryEnum.ScoreCategoryType.FULLHOUSE);
        game.resetDices(new List<int>() { 1, 1, 2, 2, 2 });
        game.chooseCategory(ScoreCategoryEnum.ScoreCategoryType.ACE);
        game.resetDices(new List<int>() { 2, 3, 5, 4, 4 });
        game.chooseCategory(ScoreCategoryEnum.ScoreCategoryType.SMALL_STRAIGHT);
        game.resetDices(new List<int>() { 1, 1, 1, 1, 2 });
        game.chooseCategory(ScoreCategoryEnum.ScoreCategoryType.FOUR_A_KIND);
        game.resetDices(new List<int>() { 1, 3, 1, 2, 2 });
        game.chooseCategory(ScoreCategoryEnum.ScoreCategoryType.TWO);
        game.resetDices(new List<int>() { 4, 6, 6, 5, 1 });
        game.chooseCategory(ScoreCategoryEnum.ScoreCategoryType.SIX);
        game.resetDices(new List<int>() { 4, 6, 6, 6, 1 });
        game.chooseCategory(ScoreCategoryEnum.ScoreCategoryType.THREE_A_KIND);
        game.resetDices(new List<int>() { 4, 6, 6, 6, 6 });
        game.chooseCategory(ScoreCategoryEnum.ScoreCategoryType.CHANCE);
        game.resetDices(new List<int>() { 4, 6, 6, 6, 1 });        
        Dictionary<ScoreCategoryEnum.ScoreCategoryType, int> detailScore = game.chooseCategory(ScoreCategoryEnum.ScoreCategoryType.YAHTZEE);
        // Mention field
        Assert.AreEqual(detailScore[ScoreCategoryEnum.ScoreCategoryType.TINY_STRAIGHT], 20);
        Assert.AreEqual(detailScore[ScoreCategoryEnum.ScoreCategoryType.FULLHOUSE], 25);
        Assert.AreEqual(detailScore[ScoreCategoryEnum.ScoreCategoryType.ACE], 2);
        Assert.AreEqual(detailScore[ScoreCategoryEnum.ScoreCategoryType.SMALL_STRAIGHT], 30);
        Assert.AreEqual(detailScore[ScoreCategoryEnum.ScoreCategoryType.FOUR_A_KIND], 6);
        Assert.AreEqual(detailScore[ScoreCategoryEnum.ScoreCategoryType.TWO], 4);
        Assert.AreEqual(detailScore[ScoreCategoryEnum.ScoreCategoryType.SIX], 12);
        Assert.AreEqual(detailScore[ScoreCategoryEnum.ScoreCategoryType.THREE_A_KIND], 23);
        Assert.AreEqual(detailScore[ScoreCategoryEnum.ScoreCategoryType.CHANCE], 28);
        Assert.AreEqual(detailScore[ScoreCategoryEnum.ScoreCategoryType.YAHTZEE], 0);
        // Unmention field
        int score = 0;
        Assert.False(detailScore.TryGetValue(ScoreCategoryEnum.ScoreCategoryType.THREE, out score));
        Assert.False(detailScore.TryGetValue(ScoreCategoryEnum.ScoreCategoryType.FOUR, out score));
        Assert.False(detailScore.TryGetValue(ScoreCategoryEnum.ScoreCategoryType.FIVE, out score));
        // Implied field
        Assert.AreEqual(detailScore[ScoreCategoryEnum.ScoreCategoryType.UPPER_SCORE], 18);
        Assert.AreEqual(detailScore[ScoreCategoryEnum.ScoreCategoryType.LOWER_SCORE], 132);
        Assert.AreEqual(detailScore[ScoreCategoryEnum.ScoreCategoryType.TOTAL_SCORE], 150);
    }

    [Test]
    public void Test10_TurnOrderForEndTurn()
    {
        List<string> playerNames = new List<string>();
        for (int i = 0; i < 6; i++)
        {
            playerNames.Add(game.currentPlayer().GetName());
            game.endTurn();
        }
        for (int i = 0; i < 6; i++)
        {
            Assert.AreEqual(game.currentPlayer().GetName(), playerNames[i]);
            game.endTurn();            
        }
    }

    [Test]
    public void Test11_OnePlayerSurrender()
    {
        List<string> playerNames = new List<string>();
        for (int i = 0; i < 6; i++) 
        {
            playerNames.Add(game.currentPlayer().GetName());
            game.endTurn();
        }
        for (int i = 0; i < 3; i++) 
        {
            game.surrender();
            Assert.AreEqual(game.currentPlayer().GetName(), playerNames[i+1]);
        }
    }

    [Test]
    public void Test12_LastPlayerSurrender()
    {
        game = new Game(new List<string>() { "Player" });
        try
        {
            game.surrender();
            Assert.Fail("Should throw EndGameException here when surrender");
        }
        catch (EndGameException e1)
        {
            try
            {
                game.currentPlayer();
                Assert.Fail("Should throw EndGameException here when call current player");
            }
            catch (EndGameException e2)
            {
                // Intentional empty
            }
        }
    }

    [Test]
    public void TestXXXX_ConvertIntToScoreCategoryType()
    {
        for (int i=1; i<=19; i++)
        {
            ScoreCategoryType type = ScoreCategory.convert(i);
            Assert.AreEqual((int)type, i);
        }
    }
}
