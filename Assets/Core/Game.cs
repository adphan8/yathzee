using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScoreCategoryEnum;

public class Game
{

    private List<Player> players;
    private List<int> dice = new List<int>(5);
    private int count = 0;
    private List<int> diceSaver = new List<int>();
    private int score = 0;
    private List<int> turnOrder = new List<int>();
    private int nRolls = 0;

    public Game(List<string> names)
    {
        players = new List<Player>();
        foreach (string name in names)
        {
            players.Add(new Player(name));
            turnOrder.Add(turnOrder.Count);
        }
        for (int i = 0; i < 5; i++)
            dice.Add(0);
    }

    public List<Player> GetPlayers()
    {
        return players;
    }

    public List<int> rollDice()
    {
        System.Random rand = new System.Random();
        int temp = 5 - count;
        for (int i = 0; i < temp; i++)
        {            
            dice[i] = rand.Next(1, 7);
            Debug.Log("Random: " + dice[i]);
        }
        nRolls += 1;
        return new List<int>(dice);
    }
    public List<int> saveDice(int n)
    {
        count++;
        for (int i = 0; i < dice.Count; i++)
        {
            if (dice[i] == n)
            {
                dice.RemoveAt(i);
                break;
            }
        }
        diceSaver.Add(n);        
        return new List<int>(diceSaver);
    }
    public List<int> unsaveDice(int n)
    {
        count--;
        for (int i = 0; i < diceSaver.Count; i++)
        {
            if (diceSaver[i] == n)
            {
                diceSaver.RemoveAt(i);
                break;
            }
        }
        dice.Add(n);
        return new List<int>(diceSaver);
    }

    public void surrender()
    {
        turnOrder.RemoveAt(0);
        if (turnOrder.Count == 0)
            throw new EndGameException();
    }

    public int calculateScore(ScoreCategoryEnum.ScoreCategoryType t)
    {
        List<int> allDices = new List<int>(diceSaver);
        allDices.AddRange(dice);
        return ScoreCategory.CalculateScore(allDices, t);
    }

    // Player choose a category to score and end his/her turn. It is end-turn action.
    public Dictionary<ScoreCategoryEnum.ScoreCategoryType, int> chooseCategory(ScoreCategoryEnum.ScoreCategoryType t)
    {
        List<int> allDices = new List<int>(diceSaver);
        allDices.AddRange(dice);
        int score = calculateScore(t);
        return currentPlayer().saveScore(t, score, isYahtzee(allDices));
    }

    public List<int> getDices()
    {
        return new List<int>(dice);
    }

    public List<int> getSaveDices()
    {
        return new List<int>(diceSaver);
    }

    public List<int> getAllDices()
    {
        List<int> allDices = new List<int>(dice);
        allDices.AddRange(diceSaver);
        return allDices;
    }

    public bool isYahtzee(List<int> dices)
    {
        for (int i = 0; i < 4; i++)
            if (dices[i] != dices[i + 1])
                return false;
        return true;
    }

    public Player currentPlayer()
    {
        if (turnOrder.Count == 0)
            throw new EndGameException();
        return players[turnOrder[0]];
    }

    public void endTurn()
    {
        nRolls = 0;
        if (turnOrder.Count == 0)
            throw new EndGameException();
        resetDices(new List<int>());
        int currentPlayerId = turnOrder[0];
        turnOrder.RemoveAt(0);
        turnOrder.Add(currentPlayerId);
    }

    public void resetDices(List<int> dices)
    {
        dice = new List<int>(dices);
        while (dice.Count < 5)
            dice.Add(1);
        diceSaver.Clear();
        count = 0;
    }

    public bool isRollable()
    {
        return nRolls < 3;
    }

    public int getNRolls()
    {
        return nRolls;
    }
}
