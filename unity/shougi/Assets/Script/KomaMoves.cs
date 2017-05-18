/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomaMoves : MonoBehaviour {

//　　通常８方向の定義(盤面上の動き)
//
//  5   6   7
//      ↑
//  3 ← 駒 → 4
//      ↓
//  2   1   0
//
//
//　　桂馬の飛び方向の定義
//
//
//  8      9
//
//
//     桂
//
//
//  11     10
//

	//方向に沿った、段の移動の定義
	public static int[] diffDan = {
		1,1,1,0,0,-1,-1,-1,-2,-2,2,2
	};

	//方向に沿った、筋の移動の定義
	public static int[] diffSuji = {
		-1,0,1,1,-1,1,0,-1,1,-1,-1,1
	};

	//ある方向にある駒が動けるかどうかを表すテーブル
	//添え字の一つ目が方向で、二つ目が種類である
	//「香車」「角」「飛車」などの一直線に動く動きについては、後のcanjumpで表し、このテーブルでは無効にする。

	public static bool[,] canMove = {
		//方向０　斜め左下への動き
		{
			false,
			false,false,false,true,false,false,false,true,false,false,false,true,true,
			false,false,false,true,true,false,false,true,true,true,true,true,true
		},
		//方向１　真下への動き
		{
			false,
			false
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}*/
