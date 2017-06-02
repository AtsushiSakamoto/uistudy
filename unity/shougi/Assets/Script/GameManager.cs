using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GameManager : MonoBehaviour {

	public GameObject obj;

	public  GameObject[] masu = new GameObject[81];            //ボタン：マス
	public Sprite[] komaPicture = new Sprite[27];              //アイコン：駒
	private Kyokumenn k = new Kyokumenn();

	static Te te = new Te();                                           //手を格納
	private int isSelectKoma;                                //駒を選択しているか



	// Use this for initialization
	void Start () {

		this.isSelectKoma = 0;                                 //スタート時は駒を選択していない
		k.turn = 1;                                           //１ターン目

		k.BanShokika ();                                       //盤面を初期化

		for (int i = 1;i <= 9 ; i++){
			for (int j = 1; j <= 9; j++) {
				int num = (i - 1) * 9 + j - 1;
				masu [num].GetComponent<Button> ().interactable = false;
				masu [num].GetComponent<Image>().sprite = komaPicture[k.banKoma[i,j]];
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
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
/*
	public void PushButtonMove(){

		//手が生成されていれば駒を動かす
		if(isSelectKoma == 2){

			if (LegalMoveHasami (te)) {
				Put (te.from_dan, te.from_suji, 0);
				Put (te.to_dan, te.to_suji, te.koma);
				isSelectKoma = 0;
				turn = turn + 1;
			}
		}

	}
*/

	//マス目を選択する
	void SelectMasu(int dan,int suji){
		//駒を選択していない場合fromに選択したマス目と駒を入れる
		if (isSelectKoma == 0) {


			te.koma = k.banKoma [dan, suji];
			if(k.turn % 2 == 1 && te.koma <= 16 && 1 <= te.koma || k.turn % 2 == 0 && te.koma < 32 && 17 <= te.koma){

				te.from_dan = dan;
				te.from_suji = suji;
				int num = (dan - 1) * 9 + suji - 1;
				masu [num].GetComponent<Image> ().color = Color.red;
				isSelectKoma = 1;

			}
		} else {  
			
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

				te.promote = false;
				//移動先が敵陣
				if((te.to_dan <= 3 && k.turn % 2 == 1) || (te.to_dan >= 7 && k.turn % 2 == 0)){
					//成れる駒
					if(KomaMoves.canPromote[te.koma]){
						//メッセージボックス表示
						bool pro = EditorUtility.DisplayDialog("成りますか？", "", "Yes", "No");
						if (pro) {
							te.promote = true;
						}
					}

				}

				//合法手ならば移動
				if (LegalMove (te)) {
					//駒があった場所を空にする
					Put (te.from_dan, te.from_suji, 0);
					//成る場合は成った駒を、ならない場合はそのままの駒を移動先におく
					if (te.promote) {
						Put (te.to_dan, te.to_suji, te.koma + 8);
					} else {
						Put (te.to_dan, te.to_suji, te.koma);
					}
					/*
					//移動先に相手の駒があったらとる
					int toKoma = k.banKoma [te.to_dan, te.to_suji];
					if (0 < toKoma && toKoma <= 16) {
						
						k.hand [1].Add (toKoma);

					} else if(toKoma != 0 && 17 <= toKoma){
						
						k.hand [0].Add (toKoma);
					}
					*/
					isSelectKoma = 0;

					//手番を変える
					this.ChangeTurn();
				}
			}
		}

	}


	//駒を置く
	void Put(int dan,int suji,int koma){
		k.banKoma [dan, suji] = koma;

		int num = (dan - 1) * 9 + suji - 1;
		masu [num].GetComponent<Image>().sprite = komaPicture[koma];   //駒画像を移動後に変える
		masu [num].GetComponent<Image>().color = Color.yellow;
	}

	public void Log(){
		for (int dan = 1; dan <= 9; dan++) {
			for (int suji = 1; suji <= 9; suji++) {
				Debug.Log (k.banKoma [dan, suji]);
			}
		}
	}

	//手番を変える関数
	void ChangeTurn(){
		k.turn = k.turn + 1;
	}

	//ゲームを初めから
	public void Restart(){

		this.isSelectKoma = 0;                                 //スタート時は駒を選択していない
		k.turn = 1;
		k.BanShokika();

		for (int dan = 1; dan <= 9; dan++) {                  //初期盤面を入れる
			for (int suji = 1; suji <= 9; suji++) {
				k.banKoma[dan,suji]= Kyokumenn.SHOKI_BAN[dan-1,suji-1];
			}
		}
		for (int i = 1;i <= 9 ; i++){
			for (int j = 1; j <= 9; j++) {
				int num = (i - 1) * 9 + j - 1;
				masu [num].GetComponent<Button> ().interactable = true;
				masu [num].GetComponent<Image>().sprite = komaPicture[k.banKoma[i,j]];
			}
		}
	}


	//合法手を返す関数

	public bool LegalMove(Te te){

		//合法手を持ってくる関数を作成し、その中にteがあればtrueを返す
		var teList = new List<Te>();
		teList = k.GenerateLegalMoves();


		for(int i = 0;i < teList.Count;i++){

//			if(teList[i].koma == 3)
//			print (teList [i].promote);
			if(te.koma == teList [i].koma 
				&& te.from_dan == teList [i].from_dan 
				&& te.from_suji == teList [i].from_suji 
				&& te.to_dan == teList [i].to_dan 
				&& te.to_suji == teList [i].to_suji
				&& ((te.promote && teList[i].promote) || (!te.promote && !teList[i].promote))
				){
				return true;
			}
		}
		return false;
	}
}
