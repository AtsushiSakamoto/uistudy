using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {



	public  GameObject[] masu = new GameObject[81];            //ボタン：マス
	public Sprite[] komaPicture = new Sprite[27];              //アイコン：駒


	private int[,] banKoma = new int[11,11];                  //盤の駒


	public struct Te{                                                //手の構造体
		public int koma;
		public int from_dan;
		public int from_suji;
		public int to_dan;
		public int to_suji;
	}

	private int turn = 0;                                    //ターンを管理
	private Te te;                                           //手を格納
	private int isSelectKoma;                                //駒を選択しているか

	private static int[,] HASAMI_BAN =  new int[9,9]{           //初期の盤面
		{14,14,14,14,14,14,14,14,14},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{1,1,1,1,1,1,1,1,1}
	};

	private static int[,] SHOKI_BAN =  new int[9,9]{           //初期の盤面
		{15,16,17,18,21,18,17,16,15},
		{0,20,0,0,0,0,0,19,0},
		{14,14,14,14,14,14,14,14,14},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{1,1,1,1,1,1,1,1,1},
		{0,6,0,0,0,0,0,7,0},
		{2,3,4,5,8,5,4,3,2}
	};

	// Use this for initialization
	void Start () {
		
		isSelectKoma = 0;                                 //スタート時は駒を選択していない

		for (int dan = 1; dan <= 9; dan++) {                  //初期盤面を入れる
			for (int suji = 1; suji <= 9; suji++) {
				banKoma[dan,suji]= SHOKI_BAN[dan-1,suji-1];
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PushHasami(){
	
		Debug.Log ("HASAMI化！");
		for (int dan = 1; dan <= 9; dan++) {                  //初期盤面を入れる
			for (int suji = 1; suji <= 9; suji++) {
				banKoma[dan,suji]= HASAMI_BAN[dan-1,suji-1];
			}
		}
	
	}
	//マス1−1をタップ
	public void PushButtonMasu11(){
		SelectMasu (1, 1);
	}
	//マス1−2をタップ
	public void PushButtonMasu12(){
		SelectMasu (1, 2);
	}
	//マス1−3をタップ
	public void PushButtonMasu13(){
		SelectMasu (1, 3);
	}
	//マス1−4をタップ
	public void PushButtonMasu14(){
		SelectMasu (1, 4);
	}
	//マス1−5をタップ
	public void PushButtonMasu15(){
		SelectMasu (1, 5);
	}
	//マス1−6をタップ
	public void PushButtonMasu16(){
		SelectMasu (1, 6);
	}
	//マス1−7をタップ
	public void PushButtonMasu17(){
		SelectMasu (1, 7);
	}
	//マス1−8をタップ
	public void PushButtonMasu18(){
		SelectMasu (1, 8);
	}
	//マス1−9をタップ
	public void PushButtonMasu19(){
		SelectMasu (1, 9);
	}
	//マス2−1をタップ
	public void PushButtonMasu21(){
		SelectMasu (2, 1);
	}
	//マス2−2をタップ
	public void PushButtonMasu22(){
		SelectMasu (2, 2);
	}
	//マス2−3をタップ
	public void PushButtonMasu23(){
		SelectMasu (2, 3);
	}
	//マス2−4をタップ
	public void PushButtonMasu24(){
		SelectMasu (2, 4);
	}
	//マス2−5をタップ
	public void PushButtonMasu25(){
		SelectMasu (2, 5);
	}
	//マス2−6をタップ
	public void PushButtonMasu26(){
		SelectMasu (2, 6);
	}
	//マス2−7をタップ
	public void PushButtonMasu27(){
		SelectMasu (2, 7);
	}
	//マス2−8をタップ
	public void PushButtonMasu28(){
		SelectMasu (2, 8);
	}
	//マス2−9をタップ
	public void PushButtonMasu29(){
		SelectMasu (2, 9);
	}
	//マス3−1をタップ
	public void PushButtonMasu31(){
		SelectMasu (3, 1);
	}
	//マス3−2をタップ
	public void PushButtonMasu32(){
		SelectMasu (3, 2);
	}
	//マス3−3をタップ
	public void PushButtonMasu33(){
		SelectMasu (3, 3);
	}
	//マス3−4をタップ
	public void PushButtonMasu34(){
		SelectMasu (3, 4);
	}
	//マス3−5をタップ
	public void PushButtonMasu35(){
		SelectMasu (3, 5);
	}
	//マス3−6をタップ
	public void PushButtonMasu36(){
		SelectMasu (3, 6);
	}
	//マス3−7をタップ
	public void PushButtonMasu37(){
		SelectMasu (3, 7);
	}
	//マス3−8をタップ
	public void PushButtonMasu38(){
		SelectMasu (3, 8);
	}
	//マス3−9をタップ
	public void PushButtonMasu39(){
		SelectMasu (3, 9);
	}
	//マス4−1をタップ
	public void PushButtonMasu41(){
		SelectMasu (4, 1);
	}
	//マス4−2をタップ
	public void PushButtonMasu42(){
		SelectMasu (4, 2);
	}
	//マス4−3をタップ
	public void PushButtonMasu43(){
		SelectMasu (4, 3);
	}
	//マス4−4をタップ
	public void PushButtonMasu44(){
		SelectMasu (4, 4);
	}
	//マス4−5をタップ
	public void PushButtonMasu45(){
		SelectMasu (4, 5);
	}
	//マス4−6をタップ
	public void PushButtonMasu46(){
		SelectMasu (4, 6);
	}
	//マス4−7をタップ
	public void PushButtonMasu47(){
		SelectMasu (4, 7);
	}
	//マス4−8をタップ
	public void PushButtonMasu48(){
		SelectMasu (4, 8);
	}
	//マス4−9をタップ
	public void PushButtonMasu49(){
		SelectMasu (4, 9);
	}
	//マス5−1をタップ
	public void PushButtonMasu51(){
		SelectMasu (5, 1);
	}
	//マス5−2をタップ
	public void PushButtonMasu52(){
		SelectMasu (5, 2);
	}
	//マス5−3をタップ
	public void PushButtonMasu53(){
		SelectMasu (5, 3);
	}
	//マス5−4をタップ
	public void PushButtonMasu54(){
		SelectMasu (5, 4);
	}
	//マス5−5をタップ
	public void PushButtonMasu55(){
		SelectMasu (5, 5);
	}
	//マス5−6をタップ
	public void PushButtonMasu56(){
		SelectMasu (5, 6);
	}
	//マス5−7をタップ
	public void PushButtonMasu57(){
		SelectMasu (5, 7);
	}
	//マス5−8をタップ
	public void PushButtonMasu58(){
		SelectMasu (5, 8);
	}
	//マス5−9をタップ
	public void PushButtonMasu59(){
		SelectMasu (5, 9);
	}
	//マス6−1をタップ
	public void PushButtonMasu61(){
		SelectMasu (6, 1);
	}
	//マス6−2をタップ
	public void PushButtonMasu62(){
		SelectMasu (6, 2);
	}
	//マス6−3をタップ
	public void PushButtonMasu63(){
		SelectMasu (6, 3);
	}
	//マス6−4をタップ
	public void PushButtonMasu64(){
		SelectMasu (6, 4);
	}
	//マス6−5をタップ
	public void PushButtonMasu65(){
		SelectMasu (6, 5);
	}
	//マス6−6をタップ
	public void PushButtonMasu66(){
		SelectMasu (6, 6);
	}
	//マス6−7をタップ
	public void PushButtonMasu67(){
		SelectMasu (6, 7);
	}
	//マス6−8をタップ
	public void PushButtonMasu68(){
		SelectMasu (6, 8);
	}
	//マス6−9をタップ
	public void PushButtonMasu69(){
		SelectMasu (6, 9);
	}
	//マス7−1をタップ
	public void PushButtonMasu71(){
		SelectMasu (7, 1);
	}
	//マス7−2をタップ
	public void PushButtonMasu72(){
		SelectMasu (7, 2);
	}
	//マス7−3をタップ
	public void PushButtonMasu73(){
		SelectMasu (7, 3);
	}
	//マス7−4をタップ
	public void PushButtonMasu74(){
		SelectMasu (7, 4);
	}
	//マス7−5をタップ
	public void PushButtonMasu75(){
		SelectMasu (7, 5);
	}
	//マス7−6をタップ
	public void PushButtonMasu76(){
		SelectMasu (7, 6);
	}
	//マス7−7をタップ
	public void PushButtonMasu77(){
		SelectMasu (7, 7);
	}
	//マス7−8をタップ
	public void PushButtonMasu78(){
		SelectMasu (7, 8);
	}
	//マス7−9をタップ
	public void PushButtonMasu79(){
		SelectMasu (7, 9);
	}
	//マス8−1をタップ
	public void PushButtonMasu81(){
		SelectMasu (8, 1);
	}
	//マス8−2をタップ
	public void PushButtonMasu82(){
		SelectMasu (8, 2);
	}
	//マス8−3をタップ
	public void PushButtonMasu83(){
		SelectMasu (8, 3);
	}
	//マス8−4をタップ
	public void PushButtonMasu84(){
		SelectMasu (8, 4);
	}
	//マス8−5をタップ
	public void PushButtonMasu85(){
		SelectMasu (8, 5);
	}
	//マス8−6をタップ
	public void PushButtonMasu86(){
		SelectMasu (8, 6);
	}
	//マス8−7をタップ
	public void PushButtonMasu87(){
		SelectMasu (8, 7);
	}
	//マス8−8をタップ
	public void PushButtonMasu88(){
		SelectMasu (8, 8);
	}
	//マス8−9をタップ
	public void PushButtonMasu89(){
		SelectMasu (8, 9);
	}
	//マス9−1をタップ
	public void PushButtonMasu91(){
		SelectMasu (9, 1);
	}
	//マス9−2をタップ
	public void PushButtonMasu92(){
		SelectMasu (9, 2);
	}
	//マス9−3をタップ
	public void PushButtonMasu93(){
		SelectMasu (9, 3);
	}
	//マス9−4をタップ
	public void PushButtonMasu94(){
		SelectMasu (9, 4);
	}
	//マス9−5をタップ
	public void PushButtonMasu95(){
		SelectMasu (9, 5);
	}
	//マス9−6をタップ
	public void PushButtonMasu96(){
		SelectMasu (9, 6);
	}
	//マス9−7をタップ
	public void PushButtonMasu97(){
		SelectMasu (9, 7);
	}
	//マス9−8をタップ
	public void PushButtonMasu98(){
		SelectMasu (9, 8);
	}
	//マス9−9をタップ
	public void PushButtonMasu99(){
		SelectMasu (9, 9);
	}

/*
	public void PushButtonMove(){
		
		//手が生成されていれば駒を動かす
		if(isSelectKoma == 2){
			put (te.from_dan, te.from_suji, 0);
			put (te.to_dan, te.to_suji, te.koma);
			isSelectKoma = 0;
			turn = turn + 1;
		}

	}
*/
	public void PushButtonMove(){

		//手が生成されていれば駒を動かす
		if(isSelectKoma == 2){

			if (legalMove (te)) {
				put (te.from_dan, te.from_suji, 0);
				put (te.to_dan, te.to_suji, te.koma);
				isSelectKoma = 0;
				turn = turn + 1;
			}
		}

	}


	//マス目を選択する
	void SelectMasu(int dan,int suji){
		//駒を選択していない場合fromに選択したマス目と駒を入れる
		if (isSelectKoma == 0) {
			int koma = banKoma [dan, suji];
			if(turn % 2 == 0 && koma <= 13 && 1<=koma || turn % 2 == 1 && koma<26 && 14<= koma){

				te.from_dan = dan;
				te.from_suji = suji;
				te.koma = koma;
				int num = (dan - 1) * 9 + suji - 1;
				masu [num].GetComponent<Image> ().color = Color.red;
				isSelectKoma = 1;

			}
		} else {                                          //駒選択済みの場合toのマス目に入れる
/*
 * if (isSelectKoma == 2) {
				int num = (te.to_dan - 1) * 9 + te.to_suji - 1;
				masu [num].GetComponent<Image> ().color = Color.yellow;
			}
*/
			te.to_dan = dan;
			te.to_suji = suji;
			isSelectKoma = 2;
			int num2 = (dan - 1) * 9 + suji - 1;
//			masu [num2].GetComponent<Image>().color = Color.red;

			//手が生成されていれば駒を動かす
			if(isSelectKoma == 2){

				if (legalMove (te)) {
					put (te.from_dan, te.from_suji, 0);
					put (te.to_dan, te.to_suji, te.koma);
					isSelectKoma = 0;
					turn = turn + 1;
				}
			}
		}

	}

	//駒を置く
	void put(int dan,int suji,int koma){
		banKoma [dan, suji] = koma;

		int num = (dan - 1) * 9 + suji - 1;
		masu [num].GetComponent<Image>().sprite = komaPicture[koma];   //駒画像を移動後に変える
		masu [num].GetComponent<Image>().color = Color.yellow;
	}

	public void log(){
		for (int dan = 1; dan <= 9; dan++) {
			for (int suji = 1; suji <= 9; suji++) {
				Debug.Log (banKoma [dan, suji]);
			}
		}
	}

	//合法手かどうか調べる関数
	public bool legalMove(Te te){

		int move;                                       //駒の動きの種類
		int danMin;                                     //段移動前後の小さい方
		int danMax;                                     //段移動前後の大きい方
		int sujiMin;                                    //筋移動前後の小さい方
		int sujiMax;                                    //筋移動前後の大きい方
		int komaTo = banKoma[te.to_dan,te.to_suji];

		if (te.from_dan != te.to_dan && te.from_suji != te.to_suji) {
			return false;
		}

		if (komaTo != 0) {
			return false;
		}
/*
		if (turn % 2 == 0) {
			
			if (komaTo <= 13 && 1 <= komaTo) {
				return false;
			}
		} else {
			if (komaTo >= 14) {
				return false;
			}

		}

*/
		//move1 縦方向の移動の場合
		if (te.from_dan != te.to_dan && te.from_suji == te.to_suji) {
			move = 1;
			//移動前後でどちらが大きいか
			if (te.from_dan > te.to_dan) {
				danMin = te.to_dan;
				danMax = te.from_dan;
			} else {
				danMax = te.to_dan;
				danMin = te.from_dan;
			}

			//間に駒があったらfalse
			for(int i = danMin + 1;i < danMax ;i++){
				if (banKoma [i, te.from_suji] != 0) {
					Debug.Log("不適な手");
					return false;
				}
			}
		}

		//move2 横方向の移動の場合
		if (te.from_dan == te.to_dan && te.from_suji != te.to_suji) {
			move = 2;
			//移動前後でどちらが大きいか
			if (te.from_suji >= te.to_suji) {
				sujiMin = te.to_suji;
				sujiMax = te.from_suji;
			} else {
				sujiMax = te.to_suji;
				sujiMin = te.from_suji;
			}

			//間に駒があったらfalse
			for(int i = sujiMin +1 ;i < sujiMax ;i++){
				if (banKoma [te.to_dan,i] != 0) {
					Debug.Log("不適な手");
					return false;
				}
			}
		}
			
		return true;
	}
}
