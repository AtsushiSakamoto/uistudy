using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hasami : MonoBehaviour {
/*
	public void PushHasami(){

		Debug.Log ("HASAMI化！");
		for (int dan = 1; dan <= 9; dan++) {                  //初期盤面を入れる
			for (int suji = 1; suji <= 9; suji++) {
				k.banKoma[dan,suji]= Kyokumenn.HASAMI_BAN[dan-1,suji-1];
				int num = (dan - 1) * 9 + suji - 1;
				masu [num].GetComponent<Image>().sprite = komaPicture[k.banKoma[dan,suji]];
			}
		}
	}

	//マス目を選択する
	void SelectMasu(int dan,int suji){
		//駒を選択していない場合fromに選択したマス目と駒を入れる
		if (isSelectKoma == 0) {
			int koma = k.banKoma [dan, suji];
			if(turn % 2 == 0 && koma <= 14 && 1<=koma || turn % 2 == 1 && koma<26 && 15<= koma){

				te.from_dan = dan;
				te.from_suji = suji;
				te.koma = koma;
				int num = (dan - 1) * 9 + suji - 1;
				masu [num].GetComponent<Image> ().color = Color.red;
				isSelectKoma = 1;

			}
		} else {                                          //駒選択済みの場合toのマス目に入れる
			
  if (isSelectKoma == 2) {
				int num = (te.to_dan - 1) * 9 + te.to_suji - 1;
				masu [num].GetComponent<Image> ().color = Color.yellow;
			}

			te.to_dan = dan;
			te.to_suji = suji;
			int num2 = (dan - 1) * 9 + suji - 1;
			//			masu [num2].GetComponent<Image>().color = Color.red;

			//選択中の駒をタップで選択を外す
			if (te.from_dan == te.to_dan && te.from_suji == te.to_suji) {
				masu [num2].GetComponent<Image>().color = Color.yellow;
				isSelectKoma = 0;
			}

			//手が生成されていれば駒を動かす
			if(isSelectKoma == 1){
				//合法手ならば移動
				if (LegalMoveHasami (te)) {
					Put (te.from_dan, te.from_suji, 0);
					Put (te.to_dan, te.to_suji, te.koma);
					isSelectKoma = 0;

					//移動後に挟めている駒があったらとる
					TakeKoma(te.to_dan,te.to_suji,te.koma);

					//手番を変える
					this.ChangeTurn();
				}
			}
		}

	}

	//駒を挟んで、または囲んでとる
	void TakeKoma(int dan,int suji,int koma){
		//移動後の駒の下に相手の駒がある場合
		if (this.banKoma [dan + 1, suji] != koma && this.banKoma [dan + 1, suji] != 0) {

			int i = 2;     //指した手と探索する位置の差
			//駒がある時、段数を増やして探索し挟んでとる
			while(this.banKoma [dan + i, suji] != 0) {

				if (this.banKoma[dan + i, suji] == koma){
					for(int j = 1; j < i; j++){
						banKoma[dan + j,suji] = 0;
						masu [(dan + j - 1) * 9 + suji - 1].GetComponent<Image>().sprite = komaPicture[0];
					}
					break;
				}
				i = i + 1;
			}

			//囲んでとる

		}


		//移動後の駒の上に相手の駒がある場合
		if (this.banKoma [dan - 1, suji] != koma && this.banKoma [dan - 1, suji] != 0) {

			int i =  2;     //指した手と探索する位置の差
			//駒がある時、段数を増やして探索する
			while(this.banKoma [dan - i, suji] != 0) {

				if (this.banKoma[dan - i, suji] == koma){
					for(int j = 1; j < i; j++){
						banKoma[dan - j,suji] = 0;
						masu [(dan - j - 1) * 9 + suji - 1].GetComponent<Image>().sprite = komaPicture[0];
					}
					break;
				}
				i = i + 1;
			}
		}
		//移動後の駒の右に相手の駒がある場合
		if (this.banKoma [dan , suji + 1] != koma && this.banKoma [dan, suji + 1] != 0) {

			int i =  2;     //指した手と探索する位置の差
			//駒がある時、段数を増やして探索する
			while(this.banKoma [dan , suji + i] != 0) {

				if (this.banKoma[dan , suji + i] == koma){
					for(int j = 1; j < i; j++){
						banKoma[dan ,suji + j] = 0;
						masu [(dan  - 1) * 9 + suji + j - 1].GetComponent<Image>().sprite = komaPicture[0];
					}
					break;
				}
				i = i + 1;
			}
		}
		//移動後の駒の左に相手の駒がある場合
		if (this.banKoma [dan , suji - 1] != koma && this.banKoma [dan, suji - 1] != 0) {

			int i =  2;     //指した手と探索する位置の差
			//駒がある時、段数を増やして探索する
			while(this.banKoma [dan , suji - i] != 0) {

				if (this.banKoma[dan , suji - i] == koma){
					for(int j = 1; j < i; j++){
						banKoma[dan ,suji - j] = 0;
						masu [(dan  - 1) * 9 + suji - j - 1].GetComponent<Image>().sprite = komaPicture[0];
					}
					break;
				}
				i = i + 1;
			}
		}



	}


//挟み将棋手番を変える関数
	public void ChangeTurnHasami(){
		
		turn = turn + 1;
		seKoma = 0;
		goKoma = 0;

		//勝利条件を満たしていれば終了
		for (int i = 1;i <= 9 ; i++){
			for (int j = 1; j <= 9; j++) {
				if (k.banKoma [i, j] == 1) {
					seKoma++;
				}
				if (k.banKoma [i, j] == 14) {
					goKoma++;
				}
			}
		}
		if (seKoma <= 5 || goKoma - seKoma >= 3 ) {
			Debug.Log("後手の勝利です");

			for (int i = 1;i <= 9 ; i++){
				for (int j = 1; j <= 9; j++) {
					int num = (i - 1) * 9 + j - 1;
					masu [num].GetComponent<Button> ().interactable = false;
				}
			}
		} 
		if (goKoma <= 5 || seKoma - goKoma >= 3) {
			Debug.Log("先手の勝利です");

			for (int i = 1;i <= 9 ; i++){
				for (int j = 1; j <= 9; j++) {
					int num = (i - 1) * 9 + j - 1;
					masu [num].GetComponent<Button> ().interactable = false;
				}
			}

		}

	}


	//挟み将棋で合法手かどうか調べる関数
	public bool LegalMoveHasami(Te te){

//		int move;                                       //駒の動きの種類
		int danMin;                                     //段移動前後の小さい方
		int danMax;                                     //段移動前後の大きい方
		int sujiMin;                                    //筋移動前後の小さい方
		int sujiMax;                                    //筋移動前後の大きい方
		int komaTo = k.banKoma[te.to_dan,te.to_suji];

		if (te.from_dan != te.to_dan && te.from_suji != te.to_suji) {
			return false;
		}

		if (komaTo != 0) {
			return false;
		}

		if (turn % 2 == 0) {
			
			if (komaTo <= 13 && 1 <= komaTo) {
				return false;
			}
		} else {
			if (komaTo >= 14) {
				return false;
			}

		}


	//move1 縦方向の移動の場合
	if (te.from_dan != te.to_dan && te.from_suji == te.to_suji) {
		//			move = 1;
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
			if (k.banKoma [i, te.from_suji] != 0) {
				Debug.Log("不適な手");
				return false;
			}
		}
	}

	//move2 横方向の移動の場合
	if (te.from_dan == te.to_dan && te.from_suji != te.to_suji) {
		//			move = 2;
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
			if (k.banKoma [te.to_dan,i] != 0) {
				Debug.Log("不適な手");
				return false;
			}
		}
	}

	return true;
}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

*/
}
