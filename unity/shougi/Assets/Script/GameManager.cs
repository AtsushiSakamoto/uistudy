using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GameManager : MonoBehaviour {


	public  GameObject[] Masu = new GameObject[81];            //ボタン：マス
	public Sprite[] komaPicture = new Sprite[27];              //アイコン：駒
	public GameObject[] hand = new GameObject[16];             //テキスト:持ち駒数
	public GameObject[] motiGoma = new GameObject[16];         //ボタン：持ち駒
	//	private Kyokumenn k = new Kyokumenn();
	private KyokumennArray kk = new KyokumennArray ();
	private List<Te> kihu = new List<Te> ();
	static Te te = new Te();                                           //手を格納
	private int isSelectKoma;                                //駒を選択しているか
	private int isSelectMotigoma;                            //持ち駒を選択しているか
	bool vsCom = false;
	bool vsComGote = false;


	// Use this for initialization
	void Start () {
		//対局できるようにする
		Restart ();
	}

	// Update is called once per frame
	void Update () {

		//vsComがtrueならばAIが指す(先手)
		if (vsCom) {
			if (kk.turn % 2 == 0) {
				//後手ならボタンをオフ
				for (int i = 1;i <= 81 ; i++){
					Masu [i - 1].GetComponent<Button> ().interactable = false;
				}

				AlphaBeta ();

				//ボタンをオンに戻す
				for (int i = 1;i <= 81 ; i++){
					Masu [i - 1].GetComponent<Button> ().interactable = true;
				}
			}
		}
		//vsComがtrueならばAIが指す(後手)
		if (vsComGote) {
			if (kk.turn % 2 == 1) {
				//先手ならボタンをオフ
				for (int i = 1;i <= 81 ; i++){
					Masu [i - 1].GetComponent<Button> ().interactable = false;
				}

				AlphaBeta ();

				//ボタンをオンに戻す
				for (int i = 1;i <= 81 ; i++){
					Masu [i - 1].GetComponent<Button> ().interactable = true;
				}
			}
		}
	}

	//人対AlphaBeta
	public void VsCom(){
		//後手にAIが指すフラグを立たせる
		if (vsCom) {
			vsCom = false;
			Debug.Log ("vscom true> false");
		} else{
			vsCom = true;
			Debug.Log("vscom false> true");
		}
	}

	//AlphaBeta対人
	public void VsComGote(){
		//先手にAIが指すフラグを立たせる
		if (vsComGote) {
			vsComGote = false;
			Debug.Log ("vscom true> false");
		} else{
			vsComGote = true;
			Debug.Log("vscom false> true");
		}

	}

	//MinMax対一手読みTEST
	public void test(){
		while (kk.GenerateLegalMoves ().Count > 0) {
			this.AlphaBeta ();
			if (kk.GenerateLegalMoves ().Count == 0)
				break;
			this.AlphaBeta ();
		}
	}
	//TEST
	public void testKai(){
		while (kk.GenerateLegalMoves ().Count > 0) {
			this.AlphaBeta ();
			if (kk.GenerateLegalMoves ().Count == 0)
				break;
			this.AlphaBeta ();
		}
	}


	//コンピューターに打たせるAlphaBeta
	public void AlphaBeta(){

		//Sikouクラスから次の手をもらう
		Sikou s = new Sikou();
		Te te = s.getNextTeKai (kk,kk.turn);

		//合法手出ない場合は合法手の中からランダムに手を作る
		if (!LegalMove (te)) {

			Debug.Log ("Alphabetaが合法手を返さない");
			var teList = new List<Te>();
			teList = kk.GenerateLegalMoves();
			te = teList [Random.Range(0, teList.Count)];
		}


		if (te.from == 0) {

			//使った持ち駒を減らす
			kk.hand [kk.turn % 2] [te.koma] -= 1;

			//持ち駒数の表示を正しくし、タップマーカーを消す
			if (kk.turn % 2 == 1) {
				hand [te.koma].GetComponent<Text> ().text = kk.hand [1] [te.koma].ToString ();
				motiGoma [te.koma].GetComponent<Image> ().color =  new Color (255f / 255f, 255f / 255f, 255f/ 255f, 255f / 255f);
			} else {
				hand [te.koma - 8].GetComponent<Text> ().text = kk.hand [0] [te.koma].ToString ();
				motiGoma [te.koma - 8].GetComponent<Image> ().color  = new Color (255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
			}
			//行き先に駒をおく
			Put (te.to, te.koma);

			//駒、持ち駒の選択フラグを消す
			isSelectKoma = 0;
			isSelectMotigoma = 0;

			//手番を変える
			this.ChangeTurn (te);

		} else {

			//移動先に相手の駒があったらとる
			int toKoma = kk.banKoma [te.to];
			if (8 < toKoma && toKoma <= 16 || 24 < toKoma && toKoma <= 32) {
				toKoma -= 8;
			}
			//先手の駒なら後手に追加,後手の駒なら先手に追加
			if (0 != toKoma && toKoma <= 16) {

				kk.hand [0] [toKoma + 16] += 1;
				hand [toKoma + 8].GetComponent<Text> ().text = kk.hand [0] [toKoma + 16].ToString ();

			} else if (toKoma != 0 && 17 <= toKoma) {

				kk.hand [1] [toKoma - 16] += 1;
				hand [toKoma - 16].GetComponent<Text> ().text = kk.hand [1] [toKoma - 16].ToString ();
			}

			//駒があった場所を空にする
			Put (te.from, 0);
			//成る場合は成った駒を、ならない場合はそのままの駒を移動先におく
			if (te.promote) {
				Put (te.to, te.koma + 8);
			} else {
				Put (te.to, te.koma);
			}

			//手番を変える
			this.ChangeTurn (te);
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
		SelectMasu (1);
	}
	//マス1−2をタップ
	public void PushButtonMasu12(){
		SelectMasu (2);
	}
	//マス1−3をタップ
	public void PushButtonMasu13(){
		SelectMasu (3);
	}
	//マス1−4をタップ
	public void PushButtonMasu14(){
		SelectMasu (4);
	}
	//マス1−5をタップ
	public void PushButtonMasu15(){
		SelectMasu (5);
	}
	//マス1−6をタップ
	public void PushButtonMasu16(){
		SelectMasu (6);
	}
	//マス1−7をタップ
	public void PushButtonMasu17(){
		SelectMasu (7);
	}
	//マス1−8をタップ
	public void PushButtonMasu18(){
		SelectMasu (8);
	}
	//マス1−9をタップ
	public void PushButtonMasu19(){
		SelectMasu (9);
	}
	//マス2−1をタップ
	public void PushButtonMasu21(){
		SelectMasu (10);
	}
	//マス2−2をタップ
	public void PushButtonMasu22(){
		SelectMasu (11);
	}
	//マス2−3をタップ
	public void PushButtonMasu23(){
		SelectMasu (12);
	}
	//マス2−4をタップ
	public void PushButtonMasu24(){
		SelectMasu (13);
	}
	//マス2−5をタップ
	public void PushButtonMasu25(){
		SelectMasu (14);
	}
	//マス2−6をタップ
	public void PushButtonMasu26(){
		SelectMasu (15);
	}
	//マス2−7をタップ
	public void PushButtonMasu27(){
		SelectMasu (16);
	}
	//マス2−8をタップ
	public void PushButtonMasu28(){
		SelectMasu (17);
	}
	//マス2−9をタップ
	public void PushButtonMasu29(){
		SelectMasu (18);
	}
	//マス3−1をタップ
	public void PushButtonMasu31(){
		SelectMasu (19);
	}
	//マス3−2をタップ
	public void PushButtonMasu32(){
		SelectMasu (20);
	}
	//マス3−3をタップ
	public void PushButtonMasu33(){
		SelectMasu (21);
	}
	//マス3−4をタップ
	public void PushButtonMasu34(){
		SelectMasu (22);
	}
	//マス3−5をタップ
	public void PushButtonMasu35(){
		SelectMasu (23);
	}
	//マス3−6をタップ
	public void PushButtonMasu36(){
		SelectMasu (24);
	}
	//マス3−7をタップ
	public void PushButtonMasu37(){
		SelectMasu (25);
	}
	//マス3−8をタップ
	public void PushButtonMasu38(){
		SelectMasu (26);
	}
	//マス3−9をタップ
	public void PushButtonMasu39(){
		SelectMasu (27);
	}
	//マス4−1をタップ
	public void PushButtonMasu41(){
		SelectMasu (28);
	}
	//マス4−2をタップ
	public void PushButtonMasu42(){
		SelectMasu (29);
	}
	//マス4−3をタップ
	public void PushButtonMasu43(){
		SelectMasu (30);
	}
	//マス4−4をタップ
	public void PushButtonMasu44(){
		SelectMasu (31);
	}
	//マス4−5をタップ
	public void PushButtonMasu45(){
		SelectMasu (32);
	}
	//マス4−6をタップ
	public void PushButtonMasu46(){
		SelectMasu (33);
	}
	//マス4−7をタップ
	public void PushButtonMasu47(){
		SelectMasu (34);
	}
	//マス4−8をタップ
	public void PushButtonMasu48(){
		SelectMasu (35);
	}
	//マス4−9をタップ
	public void PushButtonMasu49(){
		SelectMasu (36);
	}
	//マス5−1をタップ
	public void PushButtonMasu51(){
		SelectMasu (37);
	}
	//マス5−2をタップ
	public void PushButtonMasu52(){
		SelectMasu (38);
	}
	//マス5−3をタップ
	public void PushButtonMasu53(){
		SelectMasu (39);
	}
	//マス5−4をタップ
	public void PushButtonMasu54(){
		SelectMasu (40);
	}
	//マス5−5をタップ
	public void PushButtonMasu55(){
		SelectMasu (41);
	}
	//マス5−6をタップ
	public void PushButtonMasu56(){
		SelectMasu (42);
	}
	//マス5−7をタップ
	public void PushButtonMasu57(){
		SelectMasu (43);
	}
	//マス5−8をタップ
	public void PushButtonMasu58(){
		SelectMasu (44);
	}
	//マス5−9をタップ
	public void PushButtonMasu59(){
		SelectMasu (45);
	}
	//マス6−1をタップ
	public void PushButtonMasu61(){
		SelectMasu (46);
	}
	//マス6−2をタップ
	public void PushButtonMasu62(){
		SelectMasu (47);
	}
	//マス6−3をタップ
	public void PushButtonMasu63(){
		SelectMasu (48);
	}
	//マス6−4をタップ
	public void PushButtonMasu64(){
		SelectMasu (49);
	}
	//マス6−5をタップ
	public void PushButtonMasu65(){
		SelectMasu (50);
	}
	//マス6−6をタップ
	public void PushButtonMasu66(){
		SelectMasu (51);
	}
	//マス6−7をタップ
	public void PushButtonMasu67(){
		SelectMasu (52);
	}
	//マス6−8をタップ
	public void PushButtonMasu68(){
		SelectMasu (53);
	}
	//マス6−9をタップ
	public void PushButtonMasu69(){
		SelectMasu (54);
	}
	//マス7−1をタップ
	public void PushButtonMasu71(){
		SelectMasu (55);
	}
	//マス7−2をタップ
	public void PushButtonMasu72(){
		SelectMasu (56);
	}
	//マス7−3をタップ
	public void PushButtonMasu73(){
		SelectMasu (57);
	}
	//マス7−4をタップ
	public void PushButtonMasu74(){
		SelectMasu (58);
	}
	//マス7−5をタップ
	public void PushButtonMasu75(){
		SelectMasu (59);
	}
	//マス7−6をタップ
	public void PushButtonMasu76(){
		SelectMasu (60);
	}
	//マス7−7をタップ
	public void PushButtonMasu77(){
		SelectMasu (61);
	}
	//マス7−8をタップ
	public void PushButtonMasu78(){
		SelectMasu (62);
	}
	//マス7−9をタップ
	public void PushButtonMasu79(){
		SelectMasu (63);
	}
	//マス8−1をタップ
	public void PushButtonMasu81(){
		SelectMasu (64);
	}
	//マス8−2をタップ
	public void PushButtonMasu82(){
		SelectMasu (65);
	}
	//マス8−3をタップ
	public void PushButtonMasu83(){
		SelectMasu (66);
	}
	//マス8−4をタップ
	public void PushButtonMasu84(){
		SelectMasu (67);
	}
	//マス8−5をタップ
	public void PushButtonMasu85(){
		SelectMasu (68);
	}
	//マス8−6をタップ
	public void PushButtonMasu86(){
		SelectMasu (69);
	}
	//マス8−7をタップ
	public void PushButtonMasu87(){
		SelectMasu (70);
	}
	//マス8−8をタップ
	public void PushButtonMasu88(){
		SelectMasu (71);
	}
	//マス8−9をタップ
	public void PushButtonMasu89(){
		SelectMasu (72);
	}
	//マス9−1をタップ
	public void PushButtonMasu91(){
		SelectMasu (73);
	}
	//マス9−2をタップ
	public void PushButtonMasu92(){
		SelectMasu (74);
	}
	//マス9−3をタップ
	public void PushButtonMasu93(){
		SelectMasu (75);
	}
	//マス9−4をタップ
	public void PushButtonMasu94(){
		SelectMasu (76);
	}
	//マス9−5をタップ
	public void PushButtonMasu95(){
		SelectMasu (77);
	}
	//マス9−6をタップ
	public void PushButtonMasu96(){
		SelectMasu (78);
	}
	//マス9−7をタップ
	public void PushButtonMasu97(){
		SelectMasu (79);
	}
	//マス9−8をタップ
	public void PushButtonMasu98(){
		SelectMasu (80);
	}
	//マス9−9をタップ
	public void PushButtonMasu99(){
		SelectMasu (81);
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
	void SelectMasu(int masu){

		//持ち駒がタップされている場合
		if(isSelectMotigoma == 1){

			//駒は(0,0)点から来るものとする
			te.to = masu;
			te.from = 0;
			te.promote = false;
			te.capture = 0;
			//合法手ならば移動
			if (LegalMove (te)) {
				//使った持ち駒を減らす
				kk.hand [kk.turn % 2] [te.koma] -= 1;

				//持ち駒数の表示を正しくし、タップマーカーを消す
				if (kk.turn % 2 == 1) {
					hand [te.koma].GetComponent<Text> ().text = kk.hand [1] [te.koma].ToString ();
					motiGoma [te.koma].GetComponent<Image> ().color =  new Color (255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
				} else {
					hand [te.koma - 8].GetComponent<Text> ().text = kk.hand [0] [te.koma].ToString ();
					motiGoma [te.koma - 8].GetComponent<Image> ().color  = new Color (255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
				}
				//行き先に駒をおく
				Put (te.to,te.koma);

				//駒、持ち駒の選択フラグを消す
				isSelectKoma = 0;
				isSelectMotigoma = 0;

				//手番を変える
				this.ChangeTurn (te);


			}
		} else if (isSelectKoma == 0) {
			//駒を選択していない場合fromに選択したマス目と駒を入れる

			te.koma =kk.banKoma [masu];
			if(kk.turn % 2 == 1 && te.koma <= 16 && 1 <= te.koma || kk.turn % 2 == 0 && te.koma < 32 && 17 <= te.koma){

				te.from = masu;
				Masu [masu - 1].GetComponent<Image> ().color = Color.red;
				isSelectKoma = 1;

			}
		} else {  

			te.to = masu;

			//			masu [num2].GetComponent<Image>().color = Color.red;

			//選択中の駒をタップで選択を外す
			if (te.from == te.to) {
				Masu [masu - 1].GetComponent<Image> ().color =  new Color (255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
				isSelectKoma = 0;
			}

			//手が生成されていれば駒を動かす
			if(isSelectKoma == 1){

				te.promote = false;
				//移動先か移動元が敵陣
				if((te.to <= 27 && kk.turn % 2 == 1) || (te.to >= 55 && kk.turn % 2 == 0) || (te.from <= 27 && kk.turn % 2 == 1) || (te.from >= 55 && kk.turn % 2 == 0)){
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
					int toKoma = kk.banKoma [te.to];
					te.capture = toKoma;
					if (8 < toKoma && toKoma <= 16 || 24 < toKoma && toKoma <= 32) {
						toKoma -= 8;
					}

					if (0 != toKoma && toKoma <= 16) {

						kk.hand [0] [toKoma + 16] += 1;                       //先手の駒なら後手に追加
						hand[toKoma + 8].GetComponent<Text>().text = kk.hand [0] [toKoma + 16].ToString();

					} else if(toKoma != 0 && 17 <= toKoma){

						kk.hand [1] [toKoma - 16] += 1;                         //後手の駒なら先手に追加
						hand[toKoma -16].GetComponent<Text>().text = kk.hand [1] [toKoma - 16].ToString();
					}

					//駒があった場所を空にする
					Put (te.from, 0);
					//成る場合は成った駒を、ならない場合はそのままの駒を移動先におく
					if (te.promote) {
						Put (te.to, te.koma + 8);
					} else {
						Put (te.to, te.koma);
					}



					isSelectKoma = 0;

					//手番を変える
					this.ChangeTurn(te);
				}
			}
		}

	}
	//持ち駒を選択する
	void SelectMotigoma(int turn,int koma){
		if (isSelectMotigoma == 0) {
			//タップした駒の持ち主とターンがあっていた場合
			if (kk.turn % 2 == turn) {

				if (kk.hand [kk.turn % 2] [koma] > 0) {


					te.koma = koma;
					isSelectMotigoma = 1;

					if (kk.turn % 2 == 1) {
						motiGoma [koma].GetComponent<Image> ().color = Color.red;
					} else {
						motiGoma [koma - 8].GetComponent<Image> ().color = Color.red;
					}
				}
			}
		}else{

			if (isSelectMotigoma == 1) {
				//タップした駒の持ち主とターンがあっていた場合
				if (kk.turn % 2 == turn) {
					//同じ持ち駒をタップした時、アンタップする
					if (te.koma == koma) {

						isSelectMotigoma = 0;
						if (kk.turn % 2 == 1) {
							motiGoma [koma].GetComponent<Image> ().color = new Color (255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
						} else {
							motiGoma [koma - 8].GetComponent<Image> ().color = new Color (255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
						}
					}
				}

			}
		}
	}

	//駒を置く
	void Put(int masu,int koma){
		kk.banKoma [masu] = koma;


		Masu [masu - 1].GetComponent<Image>().sprite = komaPicture[koma];   //駒画像を移動後に変える
		Masu [masu - 1].GetComponent<Image> ().color = new Color (255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);

	}


	//手番を進める関数
	void ChangeTurn(Te te){
		kk.turn = kk.turn + 1;
		kihu.Add (te.DeepCopy());

		//合法手が無くなったら
		if (kk.GenerateLegalMoves ().Count == 0) {

			vsCom = false;
			print (kk.turn);

			if (kk.turn % 2 == 1) {
				Debug.Log ("後手の勝ちです。");
			} else {
				Debug.Log ("先手の勝ちです。");
			}
			//勝敗がついたらボタンをオフに
			for (int i = 1;i <= 81 ; i++){

				Masu [i - 1].GetComponent<Button> ().interactable = false;
				Masu [i - 1].GetComponent<Image> ().color = new Color (255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);

			}

			vsCom = false;
			vsComGote = false;

		}
	}

	//ゲームを初めから
	public void Restart(){

		this.isSelectKoma = 0;                                 //スタート時は駒を選択していない
		kihu.Clear();
		kk.turn = 1;
		kk.BanShokika();
		kk.hand =new List<List<int>>{new List<int>{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},new List<int>{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};

		//初期盤面を入れる
		for (int masu = 0; masu <= 81; masu++) {

			//				k.banKoma[dan,suji]= Kyokumenn.SHOKI_BAN[dan-1,suji-1];
			kk.banKoma[masu]= KyokumennArray.SHOKI_BAN[masu];
		}

		//ボタンを有効にし、駒を正しく
		for (int i = 1;i <= 81 ; i++){
			Masu [i - 1].GetComponent<Button> ().interactable = true;
			Masu [i - 1].GetComponent<Image>().sprite = komaPicture[kk.banKoma[i]];
		}


		for(int koma = 1;koma < 8;koma++){
			hand[koma].GetComponent<Text>().text = kk.hand [1] [koma].ToString();
			hand[koma + 8].GetComponent<Text>().text = kk.hand [0] [koma + 16].ToString();
		}

	}


	//合法手を返す関数

	public bool LegalMove(Te te){



		//合法手を持ってくる関数を作成し、その中にteがあればtrueを返す
		var teList = new List<Te>();
		teList = kk.GenerateLegalMoves();

		//打ち歩詰めならfalseを返す
		if (kk.IsUtifuDume (te)) {
			return false;
		}

		for(int i = 0;i < teList.Count;i++){

			//合法手と一致すればtrue
			if (te.koma == teList [i].koma
				&& te.from == teList [i].from
				&& te.to == teList [i].to
				&& ((te.promote && teList [i].promote) || (!te.promote && !teList [i].promote))) {
				return true;
			}
			/*else {
				Debug.Log (te.koma);
				Debug.Log (te.from);
				Debug.Log (te.to);
				Debug.Log (te.promote);
			}
*/
		}
		Debug.Log ("NO!!!!");
		return false;
	}

	public void KihuPrint(){

		for (int tesu = 0; tesu < kihu.Count; tesu++) {

			Debug.Log (tesu.ToString() + ":" + kihu[tesu].from.ToString() + " " + kihu[tesu].to.ToString() + " " + kihu[tesu].koma.ToString());
		}

	}

	public void back(){

		if(kk.turn - 2 >= 0){
			
			Te teLast = kihu [kk.turn - 2];
			kihu.RemoveAt (kk.turn - 2);


			kk.Back (teLast);
			kk.turn -= 1;



			//ボタンを有効にし、駒を正しく
			for (int i = 1;i <= 81 ; i++){
				Masu [i - 1].GetComponent<Button> ().interactable = true;
				Masu [i - 1].GetComponent<Image>().sprite = komaPicture[kk.banKoma[i]];
			}


			for(int koma = 1;koma < 8;koma++){
				hand[koma].GetComponent<Text>().text = kk.hand [1] [koma].ToString();
				hand[koma + 8].GetComponent<Text>().text = kk.hand [0] [koma + 16].ToString();
			}

		}
	}
}