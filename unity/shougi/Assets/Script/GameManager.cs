using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GameManager : MonoBehaviour {


	public  GameObject[] masu = new GameObject[81];            //ボタン：マス
	public Sprite[] komaPicture = new Sprite[27];              //アイコン：駒
	public GameObject[] hand = new GameObject[16];             //テキスト:持ち駒数
	public GameObject[] motiGoma = new GameObject[16];         //ボタン：持ち駒
	private Kyokumenn k = new Kyokumenn();

	static Te te = new Te();                                           //手を格納
	private int isSelectKoma;                                //駒を選択しているか
	private int isSelectMotigoma;                            //持ち駒を選択しているか
	bool vsCom = false;


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

		if (vsCom) {
			if (k.turn % 2 == 0) {
				//後手ならボタンをオフ
				for (int i = 1;i <= 9 ; i++){
					for (int j = 1; j <= 9; j++) {
						int num = (i - 1) * 9 + j - 1;
						masu [num].GetComponent<Button> ().interactable = false;
					}
				}

				AlphaBeta ();

				for (int i = 1;i <= 9 ; i++){
					for (int j = 1; j <= 9; j++) {
						int num = (i - 1) * 9 + j - 1;
						masu [num].GetComponent<Button> ().interactable = true;
					}
				}
			}

		}

	}

	//人対MinMax
	public void VsCom(){

		Restart ();
		vsCom = true;



	}


	//ランダム永久ループ
	public void rupe(){
		while (k.GenerateLegalMoves ().Count > 0) {
			this.random ();
		}
	}

	//MinMax対一手読み
	public void MinMaxVsItte(){
		while (k.GenerateLegalMoves ().Count > 0) {
			this.MinMax ();
			if (k.GenerateLegalMoves ().Count == 0)
				break;
			this.ItteYomi ();
		}
	}


	//コンピューターに打たせるAlphaBeta
	public void AlphaBeta(){

		//		Sikou s = new Sikou ();
		SikouAlphaBeta s = new SikouAlphaBeta();
		Te te = s.getNextTe (k);

		if (te.from_dan == 0) {

			//使った持ち駒を減らす
			k.hand [k.turn % 2] [te.koma] -= 1;

			//持ち駒数の表示を正しくし、タップマーカーを消す
			if (k.turn % 2 == 1) {
				hand [te.koma].GetComponent<Text> ().text = k.hand [1] [te.koma].ToString ();
				motiGoma [te.koma].GetComponent<Image> ().color = new Color (241f / 255f, 217f / 255f, 33f / 255f, 200f / 255f);
			} else {
				hand [te.koma - 8].GetComponent<Text> ().text = k.hand [0] [te.koma].ToString ();
				motiGoma [te.koma - 8].GetComponent<Image> ().color = new Color (241f / 255f, 217f / 255f, 33f / 255f, 200f / 255f);
			}
			//行き先に駒をおく
			Put (te.to_dan, te.to_suji, te.koma);

			//駒、持ち駒の選択フラグを消す
			isSelectKoma = 0;
			isSelectMotigoma = 0;

			//手番を変える
			this.ChangeTurn ();

		} else {

			//移動先に相手の駒があったらとる
			int toKoma = k.banKoma [te.to_dan, te.to_suji];
			if (8 < toKoma && toKoma <= 16 || 24 < toKoma && toKoma <= 32) {
				toKoma -= 8;
			}

			if (0 != toKoma && toKoma <= 16) {

				k.hand [0] [toKoma + 16] += 1;                       //先手の駒なら後手に追加
				hand [toKoma + 8].GetComponent<Text> ().text = k.hand [0] [toKoma + 16].ToString ();

			} else if (toKoma != 0 && 17 <= toKoma) {

				k.hand [1] [toKoma - 16] += 1;                         //後手の駒なら先手に追加
				hand [toKoma - 16].GetComponent<Text> ().text = k.hand [1] [toKoma - 16].ToString ();
			}

			//駒があった場所を空にする
			Put (te.from_dan, te.from_suji, 0);
			//成る場合は成った駒を、ならない場合はそのままの駒を移動先におく
			if (te.promote) {
				Put (te.to_dan, te.to_suji, te.koma + 8);
			} else {
				Put (te.to_dan, te.to_suji, te.koma);
			}



			isSelectKoma = 0;

			//手番を変える
			this.ChangeTurn ();
		}
	}



	//コンピューターに打たせるMinMax
	public void MinMax(){

//		Sikou s = new Sikou ();
		SikouMinMax s = new SikouMinMax();
		Te te = s.getNextTe (k);

		if (te.from_dan == 0) {

			//使った持ち駒を減らす
			k.hand [k.turn % 2] [te.koma] -= 1;

			//持ち駒数の表示を正しくし、タップマーカーを消す
			if (k.turn % 2 == 1) {
				hand [te.koma].GetComponent<Text> ().text = k.hand [1] [te.koma].ToString ();
				motiGoma [te.koma].GetComponent<Image> ().color = new Color (241f / 255f, 217f / 255f, 33f / 255f, 200f / 255f);
			} else {
				hand [te.koma - 8].GetComponent<Text> ().text = k.hand [0] [te.koma].ToString ();
				motiGoma [te.koma - 8].GetComponent<Image> ().color = new Color (241f / 255f, 217f / 255f, 33f / 255f, 200f / 255f);
			}
			//行き先に駒をおく
			Put (te.to_dan, te.to_suji, te.koma);

			//駒、持ち駒の選択フラグを消す
			isSelectKoma = 0;
			isSelectMotigoma = 0;

			//手番を変える
			this.ChangeTurn ();

		} else {

			//移動先に相手の駒があったらとる
			int toKoma = k.banKoma [te.to_dan, te.to_suji];
			if (8 < toKoma && toKoma <= 16 || 24 < toKoma && toKoma <= 32) {
				toKoma -= 8;
			}

			if (0 != toKoma && toKoma <= 16) {

				k.hand [0] [toKoma + 16] += 1;                       //先手の駒なら後手に追加
				hand [toKoma + 8].GetComponent<Text> ().text = k.hand [0] [toKoma + 16].ToString ();

			} else if (toKoma != 0 && 17 <= toKoma) {

				k.hand [1] [toKoma - 16] += 1;                         //後手の駒なら先手に追加
				hand [toKoma - 16].GetComponent<Text> ().text = k.hand [1] [toKoma - 16].ToString ();
			}

			//駒があった場所を空にする
			Put (te.from_dan, te.from_suji, 0);
			//成る場合は成った駒を、ならない場合はそのままの駒を移動先におく
			if (te.promote) {
				Put (te.to_dan, te.to_suji, te.koma + 8);
			} else {
				Put (te.to_dan, te.to_suji, te.koma);
			}



			isSelectKoma = 0;

			//手番を変える
			this.ChangeTurn ();
		}
	}
	//コンピューターに打たせる一手読み
	public void ItteYomi(){

		//		Sikou s = new Sikou ();
		SikouMinMax s = new SikouMinMax();
		Te te = s.getNextTe (k);

		if (te.from_dan == 0) {

			//使った持ち駒を減らす
			k.hand [k.turn % 2] [te.koma] -= 1;

			//持ち駒数の表示を正しくし、タップマーカーを消す
			if (k.turn % 2 == 1) {
				hand [te.koma].GetComponent<Text> ().text = k.hand [1] [te.koma].ToString ();
				motiGoma [te.koma].GetComponent<Image> ().color = new Color (241f / 255f, 217f / 255f, 33f / 255f, 200f / 255f);
			} else {
				hand [te.koma - 8].GetComponent<Text> ().text = k.hand [0] [te.koma].ToString ();
				motiGoma [te.koma - 8].GetComponent<Image> ().color = new Color (241f / 255f, 217f / 255f, 33f / 255f, 200f / 255f);
			}
			//行き先に駒をおく
			Put (te.to_dan, te.to_suji, te.koma);

			//駒、持ち駒の選択フラグを消す
			isSelectKoma = 0;
			isSelectMotigoma = 0;

			//手番を変える
			this.ChangeTurn ();

		} else {

			//移動先に相手の駒があったらとる
			int toKoma = k.banKoma [te.to_dan, te.to_suji];
			if (8 < toKoma && toKoma <= 16 || 24 < toKoma && toKoma <= 32) {
				toKoma -= 8;
			}

			if (0 != toKoma && toKoma <= 16) {

				k.hand [0] [toKoma + 16] += 1;                       //先手の駒なら後手に追加
				hand [toKoma + 8].GetComponent<Text> ().text = k.hand [0] [toKoma + 16].ToString ();

			} else if (toKoma != 0 && 17 <= toKoma) {

				k.hand [1] [toKoma - 16] += 1;                         //後手の駒なら先手に追加
				hand [toKoma - 16].GetComponent<Text> ().text = k.hand [1] [toKoma - 16].ToString ();
			}

			//駒があった場所を空にする
			Put (te.from_dan, te.from_suji, 0);
			//成る場合は成った駒を、ならない場合はそのままの駒を移動先におく
			if (te.promote) {
				Put (te.to_dan, te.to_suji, te.koma + 8);
			} else {
				Put (te.to_dan, te.to_suji, te.koma);
			}



			isSelectKoma = 0;

			//手番を変える
			this.ChangeTurn ();
		}
	}

	//コンピューターにランダムに打たせる
	public void random(){
		
		var teList = new List<Te>();
		teList = k.GenerateLegalMoves();

		Te te = teList [Random.Range(0, teList.Count)];

		if (te.from_dan == 0) {

			//使った持ち駒を減らす
			k.hand [k.turn % 2] [te.koma] -= 1;

			//持ち駒数の表示を正しくし、タップマーカーを消す
			if (k.turn % 2 == 1) {
				hand [te.koma].GetComponent<Text> ().text = k.hand [1] [te.koma].ToString ();
				motiGoma [te.koma].GetComponent<Image> ().color = new Color (241f / 255f, 217f / 255f, 33f / 255f, 200f / 255f);
			} else {
				hand [te.koma - 8].GetComponent<Text> ().text = k.hand [0] [te.koma].ToString ();
				motiGoma [te.koma - 8].GetComponent<Image> ().color = new Color (241f / 255f, 217f / 255f, 33f / 255f, 200f / 255f);
			}
			//行き先に駒をおく
			Put (te.to_dan, te.to_suji, te.koma);

			//駒、持ち駒の選択フラグを消す
			isSelectKoma = 0;
			isSelectMotigoma = 0;

			//手番を変える
			this.ChangeTurn ();

		} else {
			
			//移動先に相手の駒があったらとる
			int toKoma = k.banKoma [te.to_dan, te.to_suji];
			if (8 < toKoma && toKoma <= 16 || 24 < toKoma && toKoma <= 32) {
				toKoma -= 8;
			}

			if (0 != toKoma && toKoma <= 16) {

				k.hand [0] [toKoma + 16] += 1;                       //先手の駒なら後手に追加
				hand [toKoma + 8].GetComponent<Text> ().text = k.hand [0] [toKoma + 16].ToString ();

			} else if (toKoma != 0 && 17 <= toKoma) {

				k.hand [1] [toKoma - 16] += 1;                         //後手の駒なら先手に追加
				hand [toKoma - 16].GetComponent<Text> ().text = k.hand [1] [toKoma - 16].ToString ();
			}

			//駒があった場所を空にする
			Put (te.from_dan, te.from_suji, 0);
			//成る場合は成った駒を、ならない場合はそのままの駒を移動先におく
			if (te.promote) {
				Put (te.to_dan, te.to_suji, te.koma + 8);
			} else {
				Put (te.to_dan, te.to_suji, te.koma);
			}

		

			isSelectKoma = 0;

			//手番を変える
			this.ChangeTurn ();
		}
	}




	//持ち駒11をタップ
	public void PushButtonMotigoma11(){
		SelectMotigoma (1, 1);
	}
	//持ち駒12をタップ
	public void PushButtonMotigoma12(){
		SelectMotigoma (1, 2);
	}
	//持ち駒13をタップ
	public void PushButtonMotigoma13(){
		SelectMotigoma (1, 3);
	}
	//持ち駒14をタップ
	public void PushButtonMotigoma14(){
		SelectMotigoma (1, 4);
	}
	//持ち駒15をタップ
	public void PushButtonMotigoma15(){
		SelectMotigoma (1, 5);
	}
	//持ち駒16をタップ
	public void PushButtonMotigoma16(){
		SelectMotigoma (1, 6);
	}
	//持ち駒17をタップ
	public void PushButtonMotigoma17(){
		SelectMotigoma (1, 7);
	}
	//持ち駒01をタップ
	public void PushButtonMotigoma01(){
		SelectMotigoma (0, 17);
	}
	//持ち駒02をタップ
	public void PushButtonMotigoma02(){
		SelectMotigoma (0, 18);
	}
	//持ち駒03をタップ
	public void PushButtonMotigoma03(){
		SelectMotigoma (0, 19);
	}
	//持ち駒04をタップ
	public void PushButtonMotigoma04(){
		SelectMotigoma (0, 20);
	}
	//持ち駒05をタップ
	public void PushButtonMotigoma05(){
		SelectMotigoma (0, 21);
	}
	//持ち駒06をタップ
	public void PushButtonMotigoma06(){
		SelectMotigoma (0, 22);
	}
	//持ち駒07をタップ
	public void PushButtonMotigoma07(){
		SelectMotigoma (0, 23);
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

		//持ち駒がタップされている場合
		if(isSelectMotigoma == 1){

			//駒は(0,0)点から来るものとする
			te.to_dan = dan;
			te.to_suji = suji;
			te.from_dan = 0;
			te.from_suji = 0;
			te.promote = false;
			//合法手ならば移動
			if (LegalMove (te)) {
				//使った持ち駒を減らす
				k.hand [k.turn % 2] [te.koma] -= 1;

				//持ち駒数の表示を正しくし、タップマーカーを消す
				if (k.turn % 2 == 1) {
					hand [te.koma].GetComponent<Text> ().text = k.hand [1] [te.koma].ToString ();
					motiGoma [te.koma].GetComponent<Image> ().color = new Color (241f / 255f, 217f / 255f, 33f / 255f, 200f / 255f);
				} else {
					hand [te.koma - 8].GetComponent<Text> ().text = k.hand [0] [te.koma].ToString ();
					motiGoma [te.koma - 8].GetComponent<Image> ().color = new Color (241f / 255f, 217f / 255f, 33f / 255f, 200f / 255f);
				}
				//行き先に駒をおく
				Put (te.to_dan, te.to_suji, te.koma);

				//駒、持ち駒の選択フラグを消す
				isSelectKoma = 0;
				isSelectMotigoma = 0;

				//手番を変える
				this.ChangeTurn ();


			}
		} else if (isSelectKoma == 0) {
			//駒を選択していない場合fromに選択したマス目と駒を入れる

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
				masu [num2].GetComponent<Image> ().color = new Color (241f / 255f, 217f / 255f, 33f / 255f,200f/255f);
				isSelectKoma = 0;
			}

			//手が生成されていれば駒を動かす
			if(isSelectKoma == 1){

				te.promote = false;
				//移動先か移動元が敵陣
				if((te.to_dan <= 3 && k.turn % 2 == 1) || (te.to_dan >= 7 && k.turn % 2 == 0) || (te.from_dan <= 3 && k.turn % 2 == 1) || (te.from_dan >= 7 && k.turn % 2 == 0)){
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

					//移動先に相手の駒があったらとる
					int toKoma = k.banKoma [te.to_dan, te.to_suji];
					if (8 < toKoma && toKoma <= 16 || 24 < toKoma && toKoma <= 32) {
						toKoma -= 8;
					}

					if (0 != toKoma && toKoma <= 16) {

						k.hand [0] [toKoma + 16] += 1;                       //先手の駒なら後手に追加
						hand[toKoma + 8].GetComponent<Text>().text = k.hand [0] [toKoma + 16].ToString();

					} else if(toKoma != 0 && 17 <= toKoma){
						
						k.hand [1] [toKoma - 16] += 1;                         //後手の駒なら先手に追加
						hand[toKoma -16].GetComponent<Text>().text = k.hand [1] [toKoma - 16].ToString();
					}

					//駒があった場所を空にする
					Put (te.from_dan, te.from_suji, 0);
					//成る場合は成った駒を、ならない場合はそのままの駒を移動先におく
					if (te.promote) {
						Put (te.to_dan, te.to_suji, te.koma + 8);
					} else {
						Put (te.to_dan, te.to_suji, te.koma);
					}



					isSelectKoma = 0;

					//手番を変える
					this.ChangeTurn();
				}
			}
		}

	}
	//持ち駒を選択する
	void SelectMotigoma(int turn,int koma){
		if (isSelectMotigoma == 0) {
			//タップした駒の持ち主とターンがあっていた場合
			if (k.turn % 2 == turn) {
				
				if (k.hand [k.turn % 2] [koma] > 0) {
					

					te.koma = koma;
					isSelectMotigoma = 1;

					if (k.turn % 2 == 1) {
						motiGoma [koma].GetComponent<Image> ().color = Color.red;
					} else {
						motiGoma [koma - 8].GetComponent<Image> ().color = Color.red;
					}
				}
			}
		}else{

			if (isSelectMotigoma == 1) {
				//タップした駒の持ち主とターンがあっていた場合
				if (k.turn % 2 == turn) {
					//同じ持ち駒をタップした時、アンタップする
					if (te.koma == koma) {
						
						isSelectMotigoma = 0;
						if (k.turn % 2 == 1) {
							motiGoma [koma].GetComponent<Image> ().color = new Color (241f / 255f, 217f / 255f, 33f / 255f,200f/255f);
						} else {
							motiGoma [koma - 8].GetComponent<Image> ().color = new Color (241f / 255f, 217f / 255f, 33f / 255f,200f/255f);
						}
					}
				}

			}
		}
	}

	//駒を置く
	void Put(int dan,int suji,int koma){
		k.banKoma [dan, suji] = koma;

		int num = (dan - 1) * 9 + suji - 1;
		masu [num].GetComponent<Image>().sprite = komaPicture[koma];   //駒画像を移動後に変える
		masu [num].GetComponent<Image>().color = new Color (241f / 255f, 217f / 255f, 33f / 255f,200f/255f);
	}


	//手番を変える関数
	void ChangeTurn(){
		k.turn = k.turn + 1;

		//合法手が無くなったら
		if (k.GenerateLegalMoves ().Count == 0) {

			vsCom = false;
			print (k.turn);

			if (k.turn % 2 == 1) {
				Debug.Log ("後手の勝ちです。");
			} else {
				Debug.Log ("先手の勝ちです。");
			}
			//勝敗がついたらボタンをオフに
			for (int i = 1;i <= 9 ; i++){
				for (int j = 1; j <= 9; j++) {
					int num = (i - 1) * 9 + j - 1;
					masu [num].GetComponent<Button> ().interactable = false;
				}
			}
		}
	}

	//ゲームを初めから
	public void Restart(){

		this.isSelectKoma = 0;                                 //スタート時は駒を選択していない
		k.turn = 1;
		k.BanShokika();
		k.hand =new List<List<int>>{new List<int>{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},new List<int>{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};

		//初期盤面を入れる
		for (int dan = 1; dan <= 9; dan++) {
			for (int suji = 1; suji <= 9; suji++) {
				k.banKoma[dan,suji]= Kyokumenn.SHOKI_BAN[dan-1,suji-1];
			}
		}
		//ボタンを有効にし、駒を正しく
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

		//打ち歩詰めならfalseを返す
		if (k.IsUtifuDume (te)) {
			return false;
		}

		for(int i = 0;i < teList.Count;i++){
			
			//合法手と一致すればtrue
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
