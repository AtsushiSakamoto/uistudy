using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Sikou{
	Joseki joseki;
	//読みの深さ
	static int DEPTH_MAX = 3;
	//読みの最大深さ・・・これ以上の読みは不可能
	static int LIMIT_DEPTH = 16;

	//最前手順を格納する配列
	private Te[,] best = new Te[LIMIT_DEPTH,LIMIT_DEPTH];

	int leaf = 0;
	int node = 0;

	private int NegaMax(ref Te t,KyokumennArray k,int alpha,int beta,int depth,int depthMax){

		int value = new int ();

		//深さが最大に達していたら評価値を返して終了
		if(depth >= depthMax){
			leaf++;
			value = k.evaluate ();

			//先手ならプラス、後手でマイナスの値を返す
			if (k.turn % 2 == 1) {
				return value;
			} else {
				return -value;
			}
		}

		node++;

		//現在の局面での合法手を生成
		var teList = new List<Te>();
		teList = k.GenerateLegalMoves();

		value = -100000000;

		for (int i = 0; i < teList.Count; i++) {
			Te te = teList [i];

			//その手で一手進めた局面を作る
	//		KyokumennArray nextKyokumenn = k.DeepCopyKyokumenn ();
			k.Move (te);
			k.turn += 1;

			Te tempTe = new Te ();

			int eval = -NegaMax (ref tempTe, k, -beta, -alpha, depth + 1, depthMax);
			k.Back (te);
			k.turn -= 1;

			//大きかったら
			if (eval > value) {
				value = eval;

				//αの値も更新
				if (eval > alpha) {
					alpha = eval;
				}
				//最善手を更新
				best [depth, depth] = te;
				t.koma = te.koma;
				t.from = te.from;
				t.to = te.to;
				t.promote = te.promote;
				t.capture = k.banKoma [te.to];

				for (int j = depth + 1; j < depthMax; j++) {
					best [depth, j] = best [depth + 1, j];
				}
				//βカットの条件を満たしていたらループ終了
				if (eval >= beta) {
					break;
				}
			}
		}
		return value;
	}

	private int NegaMaxKai(ref Te t,KyokumennArray k,int alpha,int beta,int depth,int depthMax){

		int value = new int ();

		//深さが最大に達していたら評価値を返して終了
		if(depth >= depthMax){
			leaf++;
			value = k.evaluate ();

			//先手ならプラス、後手でマイナスの値を返す
			if (k.turn % 2 == 1) {
				return value;
			} else {
				return -value;
			}
		}

		node++;

		//現在の局面での合法手を生成
		var teList = new List<Te>();
		teList = k.GenerateLegalMoves();

		if (node == 1) {
			k.SortTe (ref teList);
		}

		value = -100000000;

		for (int i = 0; i < teList.Count; i++) {
			Te te = teList [i];

			//その手で一手進めた局面を作る
			//		KyokumennArray nextKyokumenn = k.DeepCopyKyokumenn ();
			k.Move (te);
			k.turn += 1;

			Te tempTe = new Te ();

			int eval = -NegaMax (ref tempTe, k, -beta, -alpha, depth + 1, depthMax);
			k.Back (te);
			k.turn -= 1;

			//大きかったら
			if (eval > value) {
				value = eval;

				//αの値も更新
				if (eval > alpha) {
					alpha = eval;
				}
				//最善手を更新
				best [depth, depth] = te;
				t.koma = te.koma;
				t.from = te.from;
				t.to = te.to;
				t.promote = te.promote;
				t.capture = k.banKoma [te.to];

				for (int j = depth + 1; j < depthMax; j++) {
					best [depth, j] = best [depth + 1, j];
				}
				//βカットの条件を満たしていたらループ終了
				if (eval >= beta) {
					break;
				}
			}
		}
		return value;
	}



	public Te getNextTe(KyokumennArray k,int tesu){

		Te te;

		if((te = joseki.fromjoseki(k,tesu)) != null){
			Debug.Log("定跡より");
			return te;
		}

		leaf =  0;
		node = 0;
		List<Te> teList = k.GenerateLegalMoves ();
		te = teList[Random.Range (0, teList.Count)];


		if (k.turn % 2 == 1) {
			//評価値最大の手をえる
			this.NegaMax (ref te, k,-1000000,1000000,0,DEPTH_MAX);
		} else {
			//評価値最小の手をえる
			this.NegaMax (ref te, k,-100000,1000000,0,DEPTH_MAX);
		}


		Debug.Log (leaf);
		return te;
	}

	public Te getNextTeKai(KyokumennArray k,int tesu){

		Te te;

		if((te = joseki.fromjoseki(k,tesu)) != null){
			Debug.Log("定跡より");
			return te;
		}

		leaf =  0;
		node = 0;
		List<Te> teList = k.GenerateLegalMoves ();
		te = teList[Random.Range (0, teList.Count)];


		if (k.turn % 2 == 1) {
			//評価値最大の手をえる
			this.NegaMaxKai (ref te, k,-1000000,1000000,0,DEPTH_MAX);
		} else {
			//評価値最小の手をえる
			this.NegaMaxKai (ref te, k,-100000,1000000,0,DEPTH_MAX);
		}


		Debug.Log (leaf);
		return te;
	}


	public Sikou(){
		joseki = new Joseki("public.bin");
	}


}