using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Sikou {



	private void GetMaxTe(ref Te t,Kyokumenn k,ref int value){

		//現在の局面での合法手を生成
		var teList = new List<Te>();
		teList = k.GenerateLegalMoves();
		value = -100000;
		var tempTe = new List<Te>();


		for (int i = 0; i < teList.Count; i++) {
			Te te = teList [i];

			//その手で一手進めた局面を作る
			Kyokumenn nextKyokumenn = k.DeepCopyKyokumenn();
			nextKyokumenn.Move (te);

			int eval = nextKyokumenn.evaluate ();
			//大きかったら
			if (eval > value) {
				value = eval;
				tempTe.Clear ();
				tempTe.Add (te);

			}
			//同じなら候補に入れる
			if (eval == value) {
				tempTe.Add (te);
			}
			//空ならvalueを返す
			if (tempTe.Count == 0) {
//				return value;
			}

			te = tempTe [Random.Range (0, tempTe.Count)];
			//同じ評価値を持った中からランダムで引数電もらったtに入れる
			t.koma = te.koma;
			t.from_dan = te.from_dan;
			t.from_suji = te.from_suji;
			t.to_dan = te.to_dan;
			t.to_suji = te.to_suji;
			t.promote = te.promote;

//			return value;
		}
	}

	private void GetMinTe(ref Te t,Kyokumenn k,ref int value){

		//現在の局面での合法手を生成
		var teList = new List<Te>();
		teList = k.GenerateLegalMoves();
		value = 100000;
		var tempTe = new List<Te>();


		for (int i = 0; i < teList.Count; i++) {
			Te te = teList [i];

			//その手で一手進めた局面を作る
			Kyokumenn nextKyokumenn = k.DeepCopyKyokumenn();
			nextKyokumenn.Move (te);

			int eval = nextKyokumenn.evaluate ();
			//小さかったら
			if (eval < value) {
				value = eval;
				tempTe.Clear ();
				tempTe.Add (te);

			}
			//同じなら候補に入れる
			if (eval == value) {
				tempTe.Add (te);
			}
			//空ならvalueを返す
			if (tempTe.Count == 0) {
				//				return value;
			}

			//同じ評価値を持った中からランダムで引数電もらったtに入れる
			te = tempTe [Random.Range (0, tempTe.Count)];
			t.koma = te.koma;
			t.from_dan = te.from_dan;
			t.from_suji = te.from_suji;
			t.to_dan = te.to_dan;
			t.to_suji = te.to_suji;
			t.promote = te.promote;

			//			return value;
		}
	}

	public Te GetNextTe(Kyokumenn k){
		
		Te te = new Te ();
		int value = new int ();

		if (k.turn % 2 == 1) {
			//評価値最大の手をえる
			this.GetMaxTe (ref te, k,ref value);
		} else {
			//評価値最小の手をえる
			this.GetMinTe (ref te, k,ref value);
		}

		return te;
	}



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
