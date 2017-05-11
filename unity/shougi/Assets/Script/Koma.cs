using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koma{

		// 駒の種類の定義
		public static int EMPTY=0;          // 「空」
		public static int EMP=EMPTY;        // 「空」の別名。
//		public static int PROMOTE=8;        // 「成り」フラグ

		public static int FU= 1;            // 「歩」
		public static int KY= 2;            // 「香車」
		public static int KE= 3;            // 「桂馬」
		public static int GI= 4;            // 「銀」
		public static int KI= 5;            // 「金」
		public static int KA= 6;            // 「角」
		public static int HI= 7;            // 「飛車」
		public static int OU= 8;            // 「玉将」
		public static int TO= 9;    // 「と金」＝「歩」＋成り
		public static int NY= 10;    // 「成り香」＝「香車」＋成り
		public static int NK= 11;    // 「成り桂」＝「桂馬」＋成り
		public static int NG= 12;    // 「成り銀」＝「銀」＋成り
		public static int UM= 13;    // 「馬」＝「角」＋成り
		public static int RY= 14;    // 「竜」＝「飛車」＋成り


		public static int WALL=64;          // 盤の外を表すための定数

		// 先手の駒かどうかの判定
/*		static public boolean isSente(int koma) {
			return (koma & SENTE)!=0;
		}

		// 後手の駒かどうかの判定
		static public boolean isGote(int koma) {
			return (koma & GOTE)!=0;
		}

		// 手番から見て自分の駒かどうか判定
		static public boolean isSelf(int teban,int koma) {
			if (teban==SENTE) {
				return isSente(koma);
			} else {
				return isGote(koma);
			}
		}

		// 手番から見て相手の駒かどうか判定
		static public boolean isEnemy(int teban,int koma) {
			if (teban==SENTE) {
				return isGote(koma);
			} else {
				return isSente(koma);
			}
		}

		// 駒の種類の取得
		static public int getKomashu(int koma) {
			// 先手後手のフラグをビット演算でなくせば良い。
			return koma & 0x0f;
		}


		// 駒が成れるかどうかを表す
		public static final boolean canPromote[]={
			false,false,false,false,false,false,false,false,// 先手でも後手でもない駒
			false,false,false,false,false,false,false,false,// 先手でも後手でもない駒
			false, true, true, true, true,false, true, true,// 空、先手の歩香桂銀金角飛
			false,false,false,false,false,false,false,false,// 先手の王、と杏圭全　馬竜
			false, true, true, true, true,false, true, true,// 空、後手の歩香桂銀金角飛
			false,false,false,false,false,false,false,false // 後手の王、と杏圭全　馬竜
		};

		static public boolean canPromote(int koma) {
			return canPromote[koma];
		}
	}
*/
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
