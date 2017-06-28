﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SikouAlphaBeta {

	//読みの深さ
	static int DEPTH_MAX = 3;
	//読みの最大深さ・・・これ以上の読みは不可能
	static int LIMIT_DEPTH = 16;

	//最前手順を格納する配列
	private Te[,] best = new Te[LIMIT_DEPTH,LIMIT_DEPTH];

	int leaf = 0;
	int node = 0;

	private int GetMaxTe(ref Te t,Kyokumenn k,int alpha,int beta,int depth,int depthMax){

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
		value = -100000000;

		for (int i = 0; i < teList.Count; i++) {
			Te te = teList [i];

			//その手で一手進めた局面を作る
			Kyokumenn nextKyokumenn = k.DeepCopyKyokumenn ();
			nextKyokumenn.Move (te);
			nextKyokumenn.turn += 1;

			Te tempTe = new Te ();

			int eval = new int();
			eval = GetMinTe (ref tempTe, nextKyokumenn, alpha, beta, depth + 1, depthMax);
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
				t.from_dan = te.from_dan;
				t.from_suji = te.from_suji;
				t.to_dan = te.to_dan;
				t.to_suji = te.to_suji;
				t.promote = te.promote;

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

	private int GetMinTe(ref Te t,Kyokumenn k,int alpha,int beta,int depth,int depthMax){

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
		value = 1000000;


		for (int i = 0; i < teList.Count; i++) {
			Te te = teList [i];

			//その手で一手進めた局面を作る
			Kyokumenn nextKyokumenn = k.DeepCopyKyokumenn();
			nextKyokumenn.Move (te);
			nextKyokumenn.turn += 1;

			Te tempTe = new Te ();

			int eval = new int();
			eval = GetMaxTe (ref tempTe, nextKyokumenn,alpha,beta, depth + 1, depthMax);
			//大きかったら
			if (eval < value) {
				value = eval;

				//βの値も更新
				if (eval < beta) {
					beta = eval;
				}
				//最善手を更新
				best [depth, depth] = te;
				t.koma = te.koma;
				t.from_dan = te.from_dan;
				t.from_suji = te.from_suji;
				t.to_dan = te.to_dan;
				t.to_suji = te.to_suji;
				t.promote = te.promote;

				for (int j = depth + 1; j < depthMax; j++) {
					best [depth, j] = best [depth + 1, j];
				}
				//βカットの条件を満たしていたらループ終了
				if (eval <= alpha) {
					break;
				}
			}
		}
		return value;
	}

	public Te getNextTe(Kyokumenn k){

		leaf =  0;
		node = 0;
		Te te = new Te ();

		if (k.turn % 2 == 1) {
			//評価値最大の手をえる
			this.GetMaxTe (ref te, k,-1000000,1000000,0,DEPTH_MAX);
		} else {
			//評価値最小の手をえる
			this.GetMinTe (ref te, k,-100000,1000000,0,DEPTH_MAX);
		}

		Debug.Log (leaf);
		Debug.Log (node);
		return te;
	}

}