using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConnectPHP : MonoBehaviour {

	public string[] runnersNew;
	public string[] runnersOnMyLvl;
	public Text opponentName;
	public Text winningText;
	private string total;
	private int totalPoints;
	private int myTotalPoints;
	private bool fight;
	private int i;
	private int j=0;
	private int currentLvl;
	private string challenger;
	private float stivos;
	private int wns;
	private string selectedpoints;
	
	// Use this for initialization
	IEnumerator Start () {
		WWW run = new WWW ("http://katya.herobo.com/Tom_Run/phpdata.php");
		yield return run;
		string runString = run.text;
		runnersNew = runString.Split(';');
		//get opponents name and show
		currentLvl = PlayerPrefs.GetInt ("level");
		runnersOnMyLvl = new string[runnersNew.Length];

		for (i=0; i<runnersNew.Length; i++) {
			if (runnersNew[i].Contains("Level:"+currentLvl.ToString())){
			    //add players of the same level to the new array
				runnersOnMyLvl[i] = runnersNew[i]  ;
			}
		}
		//find a random challenger that is not null
		while (challenger == null & currentLvl <=2) {
			challenger = runnersOnMyLvl [Random.Range (0, runnersOnMyLvl.Length)];
		}
		print (challenger);
		//get opponents name
		opponentName.text = (GetDataValue(challenger, "ID:"));
		//get opponents selected points
		SelectPoint selectedpoint = GameObject.Find ("GameObject").GetComponent<SelectPoint> ();
		//wait until value-button selected
		while (!selectedpoint.sp) {
		yield return null;
		}
		selectedpoints = selectedpoint.value;
		print (selectedpoints);
		total = (GetDataValue (challenger, selectedpoints));
		totalPoints = int.Parse (total);
		//get mine selected value
		myTotalPoints = PlayerPrefs.GetInt(selectedpoint.valueM);
		print (myTotalPoints);
		print (total);
		//fight
		fight = FightInTheArena (myTotalPoints, totalPoints);
		if (fight == true) {
			winningText.text = "WIN";
			//get 1 stivos point
			stivos = PlayerPrefs.GetFloat ("stivos");
			PlayerPrefs.SetFloat ("stivos", stivos + 1);
			wns = PlayerPrefs.GetInt("wins");
			PlayerPrefs.SetInt("wins", wns+1);
		} else {
			winningText.text = "LOOSE";
		}
	}
	//funcion to take what you want from php's echo!
    string GetDataValue(string data, string index){
			string value = data.Substring (data.IndexOf (index) + index.Length);
			value = value.Remove(value.IndexOf ("|"));
			return value;
	}
 //fighting function
	bool FightInTheArena(int mine, int his){
		bool win = true;
		if (mine >= his) {
			 win = true;
		} else {
			 win = false;
		}
		return win;
	}
}
