using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static int _GameScore ;
	public UILabel _score;
	// Use this for initialization
	void Start () {
		_GameScore = 1000;
		//Debug.Log (_score.text);
		MatchScore ();
	}
	
	// Update is called once per frame
	void Update () {
		MatchScore ();
	}

	void MatchScore(){
		if (_score.text != _GameScore.ToString())
			_score.text = _GameScore.ToString();
	}
}
