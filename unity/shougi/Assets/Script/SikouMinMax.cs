using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SikouMinMax {

	//読みの深さ
	static int DEPTH_MAX = 3;

	int leaf = 0;
	int node = 0;

	private int GetMaxTe(ref Te t,Kyokumenn k,int depth,int depthMax){

		int value = new int ();

		//深さが最大に達していたら評価値を返して終了
		if(depth >= depthMax){
			leaf++;
			value = k.evaluate ();
			return value;
		}
		node++;

		//現在の局面での合法手を生成
		var teList = new List<Te>();
		teList = k.GenerateLegalMoves();
		value = -100000;
		var tempTeList = new List<Te>();


		for (int i = 0; i < teList.Count; i++) {
			Te te = teList [i];

			//その手で一手進めた局面を作る
			Kyokumenn nextKyokumenn = k.DeepCopyKyokumenn();
			nextKyokumenn.Move (te);
			nextKyokumenn.turn += 1;

			Te tempTe = new Te ();

			int eval = -100000;
			eval =  GetMinTe(ref tempTe,nextKyokumenn,depth + 1,depthMax);
			//大きかったら
			if (eval > value) {
				value = eval;
				tempTeList.Clear ();
				tempTeList.Add (te);

			}
			//同じなら候補に入れる
			if (eval == value) {
				tempTeList.Add (te);
			}
			//空ならvalueを返す
			if (tempTeList.Count == 0) {
				return value;
			}

			te = tempTeList [Random.Range (0, tempTeList.Count)];
			//同じ評価値を持った中からランダムで引数電もらったtに入れる
			t.koma = te.koma;
			t.from_dan = te.from_dan;
			t.from_suji = te.from_suji;
			t.to_dan = te.to_dan;
			t.to_suji = te.to_suji;
			t.promote = te.promote;
		}
		return value;
	}

	private int GetMinTe(ref Te t,Kyokumenn k,int depth,int depthMax){

		int value = new int ();

		//深さが最大に達していたら評価値を返して終了
		if(depth >= depthMax){
			leaf++;
			value = k.evaluate ();
			return value;
		}
		node++;

		//現在の局面での合法手を生成
		var teList = new List<Te>();
		teList = k.GenerateLegalMoves();
		value = 100000;
		var tempTeList = new List<Te>();


		for (int i = 0; i < teList.Count; i++) {
			Te te = teList [i];

			//その手で一手進めた局面を作る
			Kyokumenn nextKyokumenn = k.DeepCopyKyokumenn();
			nextKyokumenn.Move (te);
			nextKyokumenn.turn += 1;

			Te tempTe = new Te ();

			int eval = 100000;
			eval = GetMaxTe (ref tempTe, nextKyokumenn, depth + 1, depthMax);
				//大きかったら
				if (eval < value) {
					value = eval;
					tempTeList.Clear ();
					tempTeList.Add (te);

				}
					//同じなら候補に入れる
				if (eval == value) {
					tempTeList.Add (te);
				}
					//空ならvalueを返す
				if (tempTeList.Count == 0) {
					return value;
				}

					te = tempTeList [Random.Range (0, tempTeList.Count)];
				//同じ評価値を持った中からランダムで引数電もらったtに入れる
				t.koma = te.koma;
			t.from_dan = te.from_dan;
			t.from_suji = te.from_suji;
			t.to_dan = te.to_dan;
			t.to_suji = te.to_suji;
			t.promote = te.promote;

		}

		return value;
	}

	public Te getNextTe(Kyokumenn k){
		
		leaf =  0;
		node = 0;
		Te te = new Te ();

		if (k.turn % 2 == 1) {
			//評価値最大の手をえる
			this.GetMaxTe (ref te, k,0,DEPTH_MAX);
		} else {
			//評価値最小の手をえる
			this.GetMinTe (ref te, k,0,DEPTH_MAX);
		}

		return te;
	}

}