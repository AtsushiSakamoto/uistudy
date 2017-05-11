using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kyokumenn: MonoBehaviour {

	public int[,] ban = new int[11,11];

	public static int[,] shokibanmen =  new int[9,9]{
		{2,3,4,5,8,5,4,3,2},
		{0,7,0,0,0,0,0,8,0},
		{1,1,1,1,1,1,1,1,1},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{1,1,1,1,1,1,1,1,1},
		{0,8,0,0,0,0,0,7,0},
		{2,3,4,5,8,5,4,3,2}
	};

	public void banshokika(){

		//対局開始時盤面を入れる
		for (int dan = 1; dan <= 9; dan++) {
			for (int suji = 9; suji >= 1; suji--) {
				this.ban[dan,suji]= shokibanmen[dan - 1, 9 - suji];
			}
		}

	}

	public void logKyokumen(){
		for (int dan = 1; dan <= 9; dan++) {
			for (int suji = 9; suji >= 1; suji--) {
				Debug.Log (this.ban [dan, suji]);
			}
		}
		Debug.Log (ban [7, 6]);
	}

	//ある位置のコマを取得する。
	public int get(Position p){
		//磐外なら「磐外＝壁」を返す。
		if(p.suji<1||9<p.suji||p.dan<1||9<p.dan){
			return Koma.WALL;
		}
		return this.ban [p.dan,p.suji];
	}

	//ある位置に駒をおく
	public void put(Position p,int koma){
		this.ban [p.dan, p.suji] = koma;

		/*
		for (int suji = 9; suji >= 1; suji--) {
			Debug.Log (ban [7, suji]);
		}
*/

	}

	//与えられた手で一手すすめる。
	public void move(Te te){
		Debug.Log ("move go!!");
		//元の位置をEMPTYに
		this.put(te.from,Koma.EMPTY);
		//移動先に進める
		this.put(te.to,te.koma);

	}

	// Use this for initialization
	void Start () {


	}

	// Update is called once per frame
	void Update () {

	}
}
