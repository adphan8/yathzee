using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScoreCategoryEnum;
using System;

public class ScoreController : MonoBehaviour {

    public Text currentPlayer;    
    public Text DiceRoll;   
    public Game g;

	// Use this for initialization
	void Start () {
		
        GameObject obj = GameObject.FindGameObjectWithTag("OurGameObject");
        g = Controller.instance.getGame();
        Player p = g.currentPlayer();
        currentPlayer.text = "Current Player: " + p.GetName();
        disableButton("category");
        disableButton("endturn");
        enableButton("rolldice");
        disableButton("dice");
        // Create columns on scoresheet
        GameObject scoreText = GameObject.Find("ScoreText");
        int cnt = 0;
        foreach(Player player in g.GetPlayers())
        {
            foreach (var type in (ScoreCategoryEnum.ScoreCategoryType[])Enum.GetValues(typeof(ScoreCategoryEnum.ScoreCategoryType)))
            {
                GameObject tmp = Instantiate(scoreText, new Vector3(-90 + 50 * cnt, 280 - 30*(int)type, 0), UnityEngine.Quaternion.identity);
                tmp.name = cnt.ToString() + (int)type;
                tmp.GetComponent<Text>().text = "-";
                tmp.transform.SetParent(GameObject.Find("ScorePanel").transform, false);
                Debug.Log("Create " + player.GetName());
            }
            player.setOrder(cnt);
            cnt += 1;
        }
		
		//PlayerPrefs.SetInt("HasPlayed", 1);
		if(PlayerPrefs.GetInt("HasPlayed") == 1)
		{
			List<Player> savePlayerList = g.GetPlayers();
			foreach ( Player pla in g.GetPlayers() )
			{
				initScoreBoardOnLoad(pla);
			}
		}
		
    }

    public void Surrender()
    {
        g.surrender();
        updateScoreBoard();
        disableButton("category");
        disableButton("endturn");
        enableButton("rolldice");
        disableButton("dice");
    }

    
    public void updateScoreText(int type)
    {
        Player p = g.currentPlayer();
        List<int> dice = getDice();
        ScoreCategoryEnum.ScoreCategoryType category = ScoreCategory.convert(type);
        if (p.calculateScore(dice, category) >= 0 && p.getScore(category) == -1)
        {
            p.saveScore(category, p.calculateScore(dice, category), g.isYahtzee(dice));
            updateScoreBoard();
            disableButton("category");
            disableButton("rolldice");
            enableButton("endturn");
        }        
    }

    public void rollDice()
    {
        if (g.getNRolls() == 0)
            enableButton("dice");
        DiceRoll.text = "";
        List<int> dices = g.rollDice();        
        int cnt = 0;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("dice"))
        {            
            Button button = go.GetComponent<Button>();
            if (button.IsInteractable())
            {               
                button.GetComponentInChildren<Text>().text = dices[cnt].ToString();
                DiceRoll.text = dices[cnt].ToString() + " " + DiceRoll.text;
                cnt++;
            }            
        }
        enableButton("category");
        if (!g.isRollable())
            disableButton("rolldice");        
    }

    public List<int> getDice()
    {
        return g.getAllDices();
    }

    public void endTurn()
    {
        g.endTurn();
        updateScoreBoard();
        disableButton("category");
        disableButton("endturn");
        enableButton("rolldice");
        disableButton("dice");
    }

	public void initScoreBoardOnLoad(Player play)
    {       
		PlayerPrefs.SetInt("HasPlayed", 1);
        currentPlayer.text = "Current Player: " + play.GetName();

        foreach (var type in (ScoreCategoryEnum.ScoreCategoryType[])Enum.GetValues(typeof(ScoreCategoryEnum.ScoreCategoryType)))
        {
            GameObject tmp = GameObject.Find(play.getOrder().ToString() + (int)type);
            int score = play.getScore(type);
            tmp.GetComponent<Text>().text = (score >= 0) ? score.ToString() : "-";
        }
    }
	
    public void updateScoreBoard()
    {       
		PlayerPrefs.SetInt("HasPlayed", 1);
        Player p = g.currentPlayer();
        currentPlayer.text = "Current Player: " + p.GetName();

        foreach (var type in (ScoreCategoryEnum.ScoreCategoryType[])Enum.GetValues(typeof(ScoreCategoryEnum.ScoreCategoryType)))
        {
            GameObject tmp = GameObject.Find(p.getOrder().ToString() + (int)type);
            int score = p.getScore(type);
            tmp.GetComponent<Text>().text = (score >= 0) ? score.ToString() : "-";
        }

        //p = g.getCurrentPlayer();
        //p.saveScore(ScoreCategoryEnum.ScoreCategoryType.ACE,2,false);
        //int a = p.getScore(ScoreCategoryEnum.ScoreCategoryType.ACE);
        //currentPlayer.text = a.ToString(); 
        //currentPlayer.text = p.GetName();
    }

    private void enableButton(string tag)
    {
        Player p = Controller.instance.game.currentPlayer();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))            
        {
            Button button = go.GetComponent<Button>();
            if (button == null)
            {
                continue;
            }
            if (tag == "category")
            {
                try
                {
                    ScoreCategoryType sc = (ScoreCategoryType)Enum.Parse(typeof(ScoreCategoryType), go.name);
                    if (p.getScore(sc) != -1)
                        continue;
                } 
                catch (Exception e)
                {
                    // Blank block
                }
            }
            button.interactable = true;
        }       
    }

    private void disableButton(string tag)
    {        
        foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))             
        {
            Button button = go.GetComponent<Button>();
            if (button == null)
            {                
                continue;
            }
            button.interactable = false;
        }
    }

    public void saveDice(Button button)
    {
        int value = Int32.Parse(button.GetComponentInChildren<Text>().text);
        g.saveDice(value);
        button.interactable = false;
    }
}
