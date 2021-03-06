﻿
public class KomaMoves {

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
		1,0,-1,-1,1,-1,0,1,-1,1,1,-1
	};


	// 方向の定義に沿った、「移動」の定義
	public static  int[] diff = {
		diffSuji[0] +diffDan[0] * 9,
		diffSuji[1] +diffDan[1] * 9,
		diffSuji[2] +diffDan[2] * 9,
		diffSuji[3] +diffDan[3] * 9,
		diffSuji[4] +diffDan[4] * 9,
		diffSuji[5] +diffDan[5] * 9,
		diffSuji[6] +diffDan[6] * 9,
		diffSuji[7]  +diffDan[7] * 9,
		diffSuji[8]   +diffDan[8] * 9,
		diffSuji[9] +diffDan[9] * 9,
		diffSuji[10] +diffDan[10] * 9,
		diffSuji[11] +diffDan[11] * 9
	};



	//ある方向にある駒が動けるかどうかを表すテーブル
	//添え字の一つ目が方向で、二つ目が種類である
	//「香車」「角」「飛車」などの一直線に動く動きについては、後のcanjumpで表し、このテーブルでは無効にする。

	public static bool[,] canMove = {

		//方向０　斜め左下
		{
			false,false,false,false, true,false,false,false,true,false,false,false,false,false,false, true,
			false,false,false,false, true, true,false,false,true, true, true, true, true, true,false, true 
		},
		//方向１　真下
		{
			false,false,false,false,false, true,false,false,true, true, true, true, true,false, true,false,
			false, true,false,false, true, true,false,false,true, true, true, true, true,false, true,false 
		},
		//方向２　斜め右下
		{
			false,false,false,false, true,false,false,false,true,false,false,false,false,false,false, true,
			false,false,false,false, true, true,false,false,true, true, true, true, true, true,false, true 
		},
	    //方向３　左
		{
			false,false,false,false,false, true,false,false,true, true, true, true, true,false, true,false,
			false,false,false,false,false, true,false,false,true, true, true, true, true,false, true,false
		},
		//方向４　右
		{
			false,false,false,false,false, true,false,false,true, true, true, true, true,false, true,false,
			false,false,false,false,false, true,false,false,true, true, true, true, true,false, true,false
		},
		//方向５　斜め左上
		{
			false,false,false,false, true, true,false,false,true, true, true, true, true,false,false, true,
			false,false,false,false, true,false,false,false,true,false,false,false,false,false,false, true 
		},
		//方向６　真上
		{
			false, true,false,false, true, true,false,false,true, true, true, true, true,false, true,false,
			false,false,false,false,false, true,false,false,true, true, true, true, true,false, true,false
		},
		//方向７　斜め右上
		{
			false,false,false,false, true, true,false,false,true, true, true, true, true,false,false, true,
			false,false,false,false, true,false,false,false,true,false,false,false,false,false,false, true 
		},
		//方向８　先手桂馬
		{
			false,false,false, true,false,false,false,false,false,false,false,false,false,false,false,false,
			false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false 
		},
		//方向９　先手桂馬
		{
			false,false,false, true,false,false,false,false,false,false,false,false,false,false,false,false,
			false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false
		},
		//方向１０　後手桂馬
		{
			false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,
			false,false,false, true,false,false,false,false,false,false,false,false,false,false,false,false
		},
		//方向１１　後手桂馬
		{
			false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,
			false,false,false, true,false,false,false,false,false,false,false,false,false,false,false,false
		}
	};


	//ある方向にある駒が飛べるかどうかを表すテーブル
	//添字の一つ目が方向で、二つ目が駒の種類
	//「香車」や「飛車」、「角」、「竜」、「馬」の動きはこちらで表す
	public static bool[,] canJump = {
		// 斜め左下
		{
			false,false,false,false,false,false, true,false,false,false,false,false,false,false, true,false,
			false,false,false,false,false,false, true,false,false,false,false,false,false,false, true,false 
		},
		// 真下
		{
			false,false,false,false,false,false,false, true,false,false,false,false,false,false,false, true,
			false,false, true,false,false,false,false, true,false,false,false,false,false,false,false, true 
		},
		// 斜め右下
		{
			false,false,false,false,false,false, true,false,false,false,false,false,false,false, true,false,
			false,false,false,false,false,false, true,false,false,false,false,false,false,false, true,false
		},
		// 左
		{
			false,false,false,false,false,false,false, true,false,false,false,false,false,false,false, true,
			false,false,false,false,false,false,false, true,false,false,false,false,false,false,false, true
		},
		// 右
		{
			false,false,false,false,false,false,false, true,false,false,false,false,false,false,false, true,
			false,false,false,false,false,false,false, true,false,false,false,false,false,false,false, true
		},
		// 斜め左上
		{
			false,false,false,false,false,false, true,false,false,false,false,false,false,false, true,false,
			false,false,false,false,false,false, true,false,false,false,false,false,false,false, true,false 
		},
		// 真上
		{
			false,false, true,false,false,false,false, true,false,false,false,false,false,false,false, true,
			false,false,false,false,false,false,false, true,false,false,false,false,false,false,false, true 
		},
		// 斜め右上
		{
			false,false,false,false,false,false, true,false,false,false,false,false,false,false, true,false,
			false,false,false,false,false,false, true,false,false,false,false,false,false,false, true,false
		}

	};

	public static bool[] canPromote = {
		false, true, true, true, true,false, true, true,false,false,false,false,false,false,false,false,
		false, true, true, true, true,false, true, true,false,false,false,false,false,false,false,false 
	};


}
