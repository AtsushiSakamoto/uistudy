using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {

	public int suji;
	public int dan;

	public Position(){
		suji = 0;
		dan = 0;
	}

	public Position(int _dan,int _suji){
		suji = _suji;
		dan = _dan;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
