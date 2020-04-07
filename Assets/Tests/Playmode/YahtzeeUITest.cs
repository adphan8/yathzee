using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class NewTestScript : UITest {
    
    // System test testcase 1
    [UnityTest]
    public IEnumerator Test01_StartGame()
    {
        yield return LoadScene("Scene05");
        yield return Press("English Button");
        yield return LoadScene("Scene01");
        yield return Press("LoadSceneButton");
        yield return LoadScene("Scene02");
        int cnt = 0;
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("player name"))
        {
            go.GetComponent<InputField>().text = "Player " + cnt;
            cnt += 1;
            Debug.Log(go.GetComponent<InputField>().text);
        }        
        yield return Press("LoadGameButton");
        yield return LoadScene("Scene03");        
        System.Threading.Thread.Sleep(1000);
        Assert.AreEqual(cnt, 6);        
    }

    private void Setup(int nPlayers)
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("player name"))
        {
            go.GetComponent<InputField>().text = nPlayers.ToString();
            nPlayers -= 1;
            if (nPlayers == 0)
                break;
        }
    }

    private Dictionary<string, int> GetDices()
    {
        Dictionary<string, int> dices = new Dictionary<string, int>();
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("dice"))
        {
            int dice = Int32.Parse(go.GetComponent<Button>().GetComponentInChildren<Text>().text);
            dices.Add(go.name, dice);
        }
        return dices;
    }

    private bool sameDices(Dictionary<string, int> olds, Dictionary<string, int> news)
    {
        if (olds.Count != news.Count)
            return false;
        foreach (string dice in olds.Keys)
            if (olds[dice] != news[dice])
                return false;
        return true;
    }    
    
    [UnityTest]
    public IEnumerator Test15_RollDice()
    {
        yield return LoadScene("Scene05");
        yield return Press("English Button");
        yield return LoadScene("Scene01");
        yield return Press("LoadSceneButton");
        yield return LoadScene("Scene02");
        Setup(1);
        yield return Press("LoadGameButton");
        yield return LoadScene("Scene03");
        var oldDices = GetDices();
        yield return Press("RollDice");
        var newDices = GetDices();     
        // If this assert fails, run it again. Random numbers may be the same.
        Assert.False(sameDices(oldDices, newDices));
    }
    
    [UnityTest]
    public IEnumerator Test16_RollDiceThreeTimes()
    {
        yield return LoadScene("Scene05");
        yield return Press("English Button");
        yield return LoadScene("Scene01");
        yield return Press("LoadSceneButton");
        yield return LoadScene("Scene02");
        Setup(1);
        yield return Press("LoadGameButton");
        yield return LoadScene("Scene03");
        for(int i=0; i<3; i++)
        {
            var oldDices = GetDices();
            yield return Press("RollDice");
            var newDices = GetDices();
            // If this assert fails, run it again. Random numbers may be the same.
            Assert.False(sameDices(oldDices, newDices));
            System.Threading.Thread.Sleep(1000);
        }
        Assert.False(GameObject.Find("RollDice").GetComponent<Button>().IsInteractable());
        System.Threading.Thread.Sleep(1000);
    }
    
    [UnityTest]
    public IEnumerator Test17_SaveOneDice()
    {
        yield return LoadScene("Scene05");
        yield return Press("English Button");
        yield return LoadScene("Scene01");
        yield return Press("LoadSceneButton");
        yield return LoadScene("Scene02");
        Setup(1);
        yield return Press("LoadGameButton");
        yield return LoadScene("Scene03");
        var oldDices = GetDices();
        yield return Press("RollDice");
        var newDices = GetDices();
        Assert.False(sameDices(oldDices, newDices));
        // Save dice 1 and role again
        yield return Press("Dice 1");
        System.Threading.Thread.Sleep(1000);
        yield return Press("RollDice");
        System.Threading.Thread.Sleep(1000);
        var newDices2 = GetDices();
        Assert.False(sameDices(newDices2, newDices));
        Assert.AreEqual(newDices["Dice 1"], newDices2["Dice 1"]);
        System.Threading.Thread.Sleep(1000);
    }
    
    [UnityTest]
    public IEnumerator Test18_SaveAllDice()
    {
        yield return LoadScene("Scene05");
        yield return Press("English Button");
        yield return LoadScene("Scene01");
        yield return Press("LoadSceneButton");
        yield return LoadScene("Scene02");
        Setup(1);
        yield return Press("LoadGameButton");
        yield return LoadScene("Scene03");
        var oldDices = GetDices();
        yield return Press("RollDice");
        var newDices = GetDices();
        Assert.False(sameDices(oldDices, newDices));
        // Save dice 1 and role again
        yield return Press("Dice 1");
        yield return Press("Dice 2");
        yield return Press("Dice 3");
        yield return Press("Dice 4");
        yield return Press("Dice 5");
        System.Threading.Thread.Sleep(2000);
        yield return Press("RollDice");
        System.Threading.Thread.Sleep(2000);
        var newDices2 = GetDices();
        Assert.True(sameDices(newDices2, newDices));
        System.Threading.Thread.Sleep(2000);        
    }
    
    [UnityTest]
    public IEnumerator Test19_UnsaveOneDice()
    {
        Assert.Fail("No test yet");
        return null;
    }

    private bool clickable(string name)
    {
        return GameObject.Find(name).GetComponent<Button>().IsInteractable();
    }

    // Return number of button that is clickable
    private int categoryButtonClickable()
    {
        int cnt = 0;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("category"))
        {
            Debug.Log(go.name);
            if (go.GetComponent<Button>().IsInteractable())
                cnt += 1;
        }
        return cnt;
    }
    
    [UnityTest]
    public IEnumerator Test20_ChooseScoreButton()
    {
        yield return LoadScene("Scene05");
        yield return Press("English Button");
        yield return LoadScene("Scene01");
        yield return Press("LoadSceneButton");
        yield return LoadScene("Scene02");
        Setup(1);
        yield return Press("LoadGameButton");
        System.Threading.Thread.Sleep(2000);
        yield return LoadScene("Scene03");       
        // At the beginning all buttons are diabled
        Assert.AreEqual(categoryButtonClickable(), 0);
        // Click roll dice, category button is clickable
        yield return Press("RollDice");
        Assert.AreEqual(categoryButtonClickable(), 12);
    }

    private int getScoreFromScoresheet(string name)
    {
        GameObject go = GameObject.Find(name);
        return int.Parse(go.GetComponent<Text>().text);        
    }

    // System test testcase 8
    [UnityTest]
    public IEnumerator Test21_UpperScoreCategory()
    {
        yield return LoadScene("Scene05");
        yield return Press("English Button");
        yield return LoadScene("Scene01");
        yield return Press("LoadSceneButton");
        // Choose two category in the upper section
        yield return LoadScene("Scene02");
        Setup(1);
        yield return Press("LoadGameButton");
        yield return LoadScene("Scene03");
        yield return Press("RollDice");
        System.Threading.Thread.Sleep(1000);
        List<int> dices = new List<int>();
        dices.AddRange(GetDices().Values);
        // Choose ones
        yield return Press("ACE");
        System.Threading.Thread.Sleep(1000);
        // Check scoresheet
        int calculatedScore = ScoreCategory.CalculateScore(dices, ScoreCategoryEnum.ScoreCategoryType.ACE);
        // Get ACE score of player 1
        int scoreAce = getScoreFromScoresheet("01");
        Assert.AreEqual(calculatedScore, scoreAce);
        yield return Press("Next Turn");
        yield return Press("RollDice");
        System.Threading.Thread.Sleep(1000);
        dices.Clear();
        dices.AddRange(GetDices().Values);
        // Choose six
        yield return Press("SIX");
        System.Threading.Thread.Sleep(1000);
        calculatedScore = ScoreCategory.CalculateScore(dices, ScoreCategoryEnum.ScoreCategoryType.SIX);
        // Get SIX score of player 1
        int scoreSix = getScoreFromScoresheet("06");
        Assert.AreEqual(scoreSix, calculatedScore);
        // Check Upper section score
        int upperScore = getScoreFromScoresheet("08");
        Assert.AreEqual(upperScore, scoreSix + scoreAce);
        // Total score
        Assert.AreEqual(getScoreFromScoresheet("019"), upperScore);
        // Button ace and six not clickable
        Assert.False(clickable("ACE"));
        Assert.False(clickable("SIX"));
    }

    // System test testcase 9
    [UnityTest]
    public IEnumerator Test22_ChooseCategory()
    {
        yield return LoadScene("Scene05");
        yield return Press("English Button");
        yield return LoadScene("Scene01");
        yield return Press("LoadSceneButton");
        // Choose two category in the upper section
        yield return LoadScene("Scene02");
        Setup(1);
        yield return Press("LoadGameButton");
        yield return LoadScene("Scene03");
        yield return Press("RollDice");
        System.Threading.Thread.Sleep(1000);
        List<int> dices = new List<int>();
        dices.AddRange(GetDices().Values);
        // Choose CHANCE
        yield return Press("CHANCE");
        System.Threading.Thread.Sleep(1000);
        // Check scoresheet
        int calculatedScore = ScoreCategory.CalculateScore(dices, ScoreCategoryEnum.ScoreCategoryType.CHANCE);
        // Get CHANCE score of player 1
        int scoreChance = getScoreFromScoresheet("016");
        Assert.AreEqual(calculatedScore, scoreChance);
        int lowerScore = getScoreFromScoresheet("017");
        // Next turn
        yield return Press("Next Turn");
        yield return Press("RollDice");
        dices.Clear();
        dices.AddRange(GetDices().Values);
        // Choose THREE
        yield return Press("THREE");
        System.Threading.Thread.Sleep(1000);
        calculatedScore = ScoreCategory.CalculateScore(dices, ScoreCategoryEnum.ScoreCategoryType.THREE);
        // Get THREE score of player 1
        int scoreThree = getScoreFromScoresheet("03");
        Assert.AreEqual(scoreThree, calculatedScore);
        // Check Upper section score
        int upperScore = getScoreFromScoresheet("08");
        Assert.AreEqual(upperScore, scoreThree);
        // Total score
        Assert.AreEqual(getScoreFromScoresheet("019"), upperScore + lowerScore);
        // Button ace and six not clickable
        Assert.False(clickable("THREE"));
        Assert.False(clickable("CHANCE"));
    }

    [UnityTest]
    [Ignore("Test it manually")]
    public IEnumerator Test14_Sound()
    {
        Assert.Fail("Please test it manually. Start game and change volume.");        
        return null; 
    }
    
    [UnityTest]
    [Ignore("Test it manually")]
    public IEnumerator Test13_Language()
    {
        Assert.Fail("Please test it manually. Start game and change language.");
        return null;
    }
}
