using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Te : MonoBehaviour {

	public int koma;
	public Position from;
	public Position to;

//	bool promote;

	public int selectkoma = 0;

	public Te(int _koma,Position _from,Position _to,bool _promote){
		
		koma = _koma;
		from = _from;
		to = _to;
//		promote = _promote;

	}



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
