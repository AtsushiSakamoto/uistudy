using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class KyokumennArray {


	public int[] banKoma = new int[82];

	//	public List<int[,]> banMemory;
	public  int turn = new int();                             //現在の手番
	public List<List<int>> hand = new List<List<int>>{new List<int>{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},new List<int>{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};

	public bool josekiBool = true;


	public static int[,] HASAMI_BAN =  new int[9,9]{           //ハサミ将棋初期の盤面
		{17,17,17,17,17,17,17,17,17},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{1,1,1,1,1,1,1,1,1}
	};

	public static int[] SHOKI_BAN = new int[82]{
		0,
		18,19,20,21,24,21,20,19,18,
		0,23, 0, 0, 0, 0, 0,22, 0,
		17,17,17,17,17,17,17,17,17,
		0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0,
		1, 1, 1, 1, 1, 1, 1, 1, 1,
		0, 6, 0, 0, 0, 0, 0, 7, 0,
		2, 3, 4, 5, 8, 5, 4, 3, 2
	} ;

	public static int[] MotiKomaValue = new int[32]{
		0,100,600,700,1000,1200,1800,2000,10000,1200,1200,1200,1200,0,2000,2200,
		0,-100,-600,-700,-1000,-1200,-1800,-2000,-10000,-1200,-1200,-1200,-1200,0,-2000,-2200
	};

	public static int[] KomaValue = new int[32]{
		0,   100,   600,   700,  1000,  1200,  1800,  2000, 10000,  1200,  1200,  1200,  1200,0,  2000,  2200,
		0,  -100,  -600,  -700, -1000, -1200, -1800, -2000,-10000, -1200, -1200, -1200, -1200,0, -2000, -2200
	};



	public static int[,] DanKomaValue = new int[32,9]{

		//空
		{ 0,0,0,0,0,0,0,0,0},
		//歩
		{ 0,15,15,15,3,1, 0, 0, 0},
		//香
		{ 1,2,3,4,5,6,7,8,9},
		//桂
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//銀
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//金
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//角
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//飛
		{ 10,10,10, 0, 0, 0,  -5, 0, 0},
		//王
		{ 1200,1200,900,600,300,-10,0,0,0},
		//と
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//杏
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//圭
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//全
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//金
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//馬
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//龍
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//空
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//歩
		{ 0, 0, 0, -1, -3,-15,-15,-15, 0},
		//香
		{ -9,-8,-7, -6, -5, -4, -3, -2,-1},
		//桂
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//銀
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//金
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//角
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//飛
		{ 0, 0, 5, 0, 0, 0,-10,-10,-10},
		//王
		{ 0, 0, 0,10,-300,-600,-900,-1200,-1200},
		//と
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//杏
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//圭
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//全
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//金
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//馬
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//龍
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0}

	};

	public void BanShokika(){

		for (int i = 0; i < 82; i++) {
			this.banKoma [i] = KyokumennArray.SHOKI_BAN [i];
		}

		this.turn = 1;
		this.hand = new List<List<int>>{new List<int>{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},new List<int>{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};

	}

	public object CloneKyokumenn()                            // シャローコピー
	{
		return MemberwiseClone();
	}

	public KyokumennArray ShallowCopyKyokumenn()                          //シャローコピー
	{
		return (KyokumennArray)this.CloneKyokumenn();
	}

	public KyokumennArray DeepCopyKyokumenn()                             //ディープコピー
	{
		KyokumennArray obj = new KyokumennArray ();
		//参照型は全てインスタンスをコピーする
		obj.turn = (int)this.turn;

		for (int i = 0; i < 32; i++) {
			obj.hand [0][i] = this.hand [0] [i];
			obj.hand [1] [i] = (int)this.hand [1] [i];
		}


		for (int i = 0; i < 82; i++) {
			obj.banKoma [i] = (int)this.banKoma[i];
		}

		return obj;
	} 


	//局面を評価する関数
	public int evaluate(){

		int eval = 0;
		//盤面上の駒の価値を全部加算
		for (int i = 1; i < 82;i++) {                  

			int koma = this.banKoma [i];
			eval += KyokumennArray.DanKomaValue [koma,(i - 1) / 9];
			eval += KyokumennArray.KomaValue [koma];

		}
		//持ち駒の加算
		for(int i = 0;i < 2;i++){
			for (int j = 0; j < 32; j++) {
				eval += this.hand [i] [j] * KyokumennArray.MotiKomaValue [j];
			}
		}
		return eval;
	}



	public void Sub(ref int diffDan,int diffSuji){
		diffDan = -diffDan;
		diffSuji = -diffSuji;
	}



	public void SearchGyoku(ref int king,int turn){
		//探す駒はturn側の玉
		for(int i = 1;i <= 81;i++){

			if (this.banKoma [i] == 8 && turn % 2 == 1 || this.banKoma [i] == 24 && turn % 2 == 0) {
				king = i;
			}

		}
	}

	public void SortKyokumen(ref List<Te> teList){

		for (int i = 0; i < teList.Count - 1; i++) {
			for (int j = 0; j < teList.Count - 1; j++) {
				KyokumennArray mae = this.DeepCopyKyokumenn ();
				KyokumennArray ato = this.DeepCopyKyokumenn ();

				mae.Move (teList [i]);
				ato.Move (teList [i + 1]);

				if (mae.evaluate() > ato.evaluate ()) {
					Te tmp = teList [i].DeepCopy();
					teList [i] = teList [i + 1].DeepCopy();
					teList [i + 1] = tmp;

				}
			}	
		}

		return;
	}

	public void Put(int i,int koma){
		this.banKoma [i] = koma;
	}



	public void Move(Te te){

		//駒の行き先に駒があったなら持ち駒にする
		int toKoma = this.banKoma[te.to];
		if (8 < toKoma && toKoma <= 16 || 24 < toKoma && toKoma <= 32) {
			toKoma -= 8;
		}
		if(toKoma != 0){
			//先手の駒なら後手の持ち駒
			if (1 <= toKoma && toKoma < 16) {
				this.hand [0] [toKoma + 16] += 1;
			} else {
				this.hand [1] [toKoma - 16] += 1;
			}
		}
		//持ち駒を打った
		if (te.from == 0) {
			//使った持ち駒を減らす
			this.hand [this.turn % 2] [te.koma] -= 1;
			this.Put (te.to, te.koma);
		} else {
			//盤上の駒を進めた
			//移動前は空白に

			this.Put(te.from,0);
			//移動後は成る場合は成った駒をならない場合はそのままの駒をおく
			if (te.promote) {
				this.Put (te.to, te.koma + 8);
			} else {
				this.Put (te.to, te.koma);
			}
		}

	}

	//打ち歩詰めの判定関数
	public bool IsUtifuDume(Te te){
		//歩(1,17)で打たれた時(段が0)に相手に合法手がなければ打ち歩詰め
		if (te.koma == 1 || te.koma == 17) {
			if (te.from == 0) {

				KyokumennArray temp = this.DeepCopyKyokumenn ();

				temp.Move (te);
				temp.turn += 1;

				if (temp.GenerateLegalMoves ().Count == 0) {
					return true;
				}
			}
		}
		return false;
	} 




	//局面が同一かどうか
	public bool equals(KyokumennArray k){
		//手番の比較
		if(this.turn % 2 != k.turn % 2){
			return false;
		}

		//盤面の比較
		for(int i = 1; i <= 81; i++){

			if(this.banKoma[i] != k.banKoma[i]){
				return false;
			}

		}
		//持ち駒の比較
		for(int koma = 0;koma < 32;koma++){
			if (this.hand [0] [koma] !=  k.hand [0] [koma] || this.hand [1] [koma] !=  k.hand [1] [koma]) {
				return false;
			}
		} 

		return true;
	}

	public List<Te> GenerateLegalMoves(){
		//合法手を格納する変数
		List<Te> teList = new List<Te>();
		List<Te> removed = new List<Te>();
		Te te = new Te ();

		for (int masu = 1; masu <= 81; masu++) {        

			int koma = this.banKoma [masu];
			//探索する駒が手番の駒かどうか
			if ((this.turn % 2 == 1 && koma >= 1 && koma <= 16) || (this.turn % 2 == 0 && koma >= 17)) {

				//各方向に移動する手を生成
				for(int direct = 0;direct < 12;direct++){
					if (KomaMoves.canMove [direct, koma]) {

						te.koma = koma;
						te.to = masu + KomaMoves.diff [direct];
						te.from = masu;

						if ((masu % 9 == 0 && te.to % 9 == 1) || (masu % 9 == 1 && te.to % 9 == 0)) {
							continue;
						}

						//移動先は盤内か
						if(1 <= te.to && te.to <= 81){

							//移動先に自分の駒はないか
							int toKoma = this.banKoma[te.to];
							if ((this.turn % 2 == 1 && (toKoma == 0 || toKoma >= 17)) || (this.turn % 2 == 0 && toKoma <= 16)) {
								//桂馬は敵二段目まで、歩は一段目までで成らずはできない
								if ((te.koma != 3 || te.to  > 18) && (te.koma != 19 || te.to < 64) && (te.koma != 1 || te.to >= 10) && (te.koma != 17 || te.to <= 72)) {
									te.promote = false;
									teList.Add (te.DeepCopy ());
								}
								//移動先が敵陣
								if((te.to <= 27 && this.turn % 2 == 1) || (te.to >= 55 && this.turn % 2 == 0)){

									//成れる駒
									if(KomaMoves.canPromote[koma]){
										te.promote = true;
										teList.Add (te.DeepCopy());
									}
								}

								//移動元が敵陣
								if((te.from <= 27 && this.turn % 2 == 1) || (te.from >= 55 && this.turn % 2 == 0)){

									//成れる駒
									if(KomaMoves.canPromote[koma]){
										te.promote = true;
										teList.Add (te.DeepCopy());
									}
								}
							}
						}
					}
				}

				//各方向に「飛ぶ」手を生成
				for(int direct = 0;direct < 8;direct++){

					if (KomaMoves.canJump [direct, koma]) {

						te.from = masu;
						te.koma = koma;

						for (int i = 1; i < 9; i++) {

							//移動先を生成

							te.to = masu + KomaMoves.diff[direct] * i;

							if (((te.to % 9 == 0) && (((te.to - KomaMoves.diff[direct]) % 9) == 1)) || ((te.to % 9 == 1) && ((te.to - KomaMoves.diff[direct])  % 9 == 0))) {
								break;
							}

							//移動先は盤内か
							if(1 <= te.to && te.to <= 81){
								//移動先に自分の駒はないか
								int toKoma = this.banKoma[te.to];
								if ((this.turn % 2 == 1 && (toKoma == 0 || toKoma >= 17)) || (this.turn % 2 == 0 && toKoma <= 16)) {
									//香車は敵の一段目まで行って成らずは不可
									if ((te.koma != 2 || te.to > 9) && (te.koma != 18 || te.to < 73)) {
										te.promote = false;
										teList.Add (te.DeepCopy ());
									}
									//移動先が敵陣
									if((te.to <= 27 && this.turn % 2 == 1) || (te.to >= 64 && this.turn % 2 == 0)){

										//成れる駒
										if(KomaMoves.canPromote[koma]){
											te.promote = true;
											teList.Add (te.DeepCopy());
										}
									}
									//移動元が敵陣
									if((te.from <= 27 && this.turn % 2 == 1) || (te.from >= 64 && this.turn % 2 == 0)){

										//成れる駒
										if(KomaMoves.canPromote[koma]){
											te.promote = true;
											teList.Add (te.DeepCopy());
										}
									}

								}
								//空きマスでなければここでループ終わり
								if(toKoma != 0) break;
							}
						}
					}

				}
			}
		}

		//持ち駒からだす手を追加(銀、金、角、飛車)
		if (this.turn % 2 == 1) {                              //先手の場合
			//銀~飛車までループ
			for (int i = 4; i <= 7; i++) {
				//探索中の駒を持っているか
				if (this.hand [1] [i] >= 1) {
					te.koma = i;

					for (int masu = 1; masu <= 81; masu++) {                  

						//駒は(0,0)点からとおく
						te.to = masu;
						te.from = 0;
						te.promote = false;

						if (this.banKoma [masu] == 0) {
							teList.Add (te.DeepCopy ());
						}

					}
				}
			}
		} else {                                                //後手の場合
			//銀~飛車までループ
			for (int i = 20; i <= 23; i++) {
				//探索中の駒を持っているか
				if (this.hand [0] [i ] >= 1) {
					te.koma = i;

					for (int masu = 1; masu <= 81; masu++) {                  

						//駒は(0,0)点からとおく
						te.to = masu;
						te.from = 0;
						te.promote = false;

						if (this.banKoma [masu] == 0) {
							teList.Add (te.DeepCopy ());
						}
					}

				}
			}
		}

		if (this.turn % 2 == 1) {                              //先手の場合

			//先手の持ち駒に歩がある
			if (this.hand [1] [1] >= 1) {
				te.koma = 1;

				//二歩チェック
				for (int suji = 1; suji <= 9; suji++) {
					bool isNifu = false;                            //二歩チェック用変数
					for (int dan = 1; dan <= 9; dan++) {
						if (this.banKoma [(dan - 1) * 9 + suji] == 1) {
							isNifu = true;
						}

					}
					if (isNifu) {
						continue;

					}
					//これ以上進めない1段目を除き、駒がなければ歩を出す手を追加する
					for (int dan = 2; dan <= 9; dan++) {                  


						//駒は(0,0)点からとおく
						te.to = (dan - 1) * 9 + suji;
						te.from = 0;
						te.promote = false;

						if (this.banKoma [(dan - 1) * 9 + suji] == 0) {
							teList.Add (te.DeepCopy ());
						}
					}
				}

			}


			//先手の持ち駒に香車がある
			if (this.hand [1] [2] >= 1) {
				te.koma = 2;


				for (int masu = 10; masu <= 81; masu++) {

					//これ以上進めない1段目を除き、駒がなければ香車を出す手を追加する


					//駒は(0,0)点からとおく
					te.to = masu;
					te.from = 0;
					te.promote = false;

					if (this.banKoma [masu] == 0) {
						teList.Add (te.DeepCopy ());
					}
				}
			}

		

		//先手の持ち駒に桂馬がある
			if (this.hand [1] [3] >= 1) {
				te.koma = 3;


				for (int masu = 19; masu <= 81; masu++) {

					//これ以上進めない1,2段目を除き、駒がなければ桂馬を出す手を追加する


					//駒は(0,0)点からとおく
					te.to = masu;
					te.from = 0;
					te.promote = false;

					if (this.banKoma [masu] == 0) {
						teList.Add (te.DeepCopy ());
					}
				}

			}


		} else {                         //後手の場合

			//持ち駒に歩がある
			if (this.hand [0] [17] >= 1) {

				te.koma = 17;
				//二歩チェック
				for (int suji = 1; suji <= 9; suji++) {
					bool isNifu = false;                            //二歩チェック用変数
					for (int dan = 1; dan <= 9; dan++) {
						if (this.banKoma [(dan - 1) * 9 + suji] == 17) {
							isNifu = true;
						}

					}
					if (isNifu) {
						continue;
					}
					//これ以上進めない1段目を除き、駒がなければ歩を出す手を追加する
					for (int dan = 1; dan <= 8; dan++) {                  

						//駒は(0,0)点からとおく
						te.to = (dan - 1) * 9 + suji;
						te.from = 0;
						te.promote = false;

						if (this.banKoma [(dan - 1) * 9 + suji] == 0) {
							teList.Add (te.DeepCopy ());
						}
					}
				}
			}


			//持ち駒に香車がある
			if (this.hand [0] [18] >= 1) {

				te.koma = 18;

				for (int masu = 1; masu <= 72; masu++) {

					//これ以上進めない1段目を除き、駒がなければ香車を出す手を追加する


					//駒は(0,0)点からとおく
					te.to = masu;
					te.from = 0;
					te.promote = false;

					if (this.banKoma [masu] == 0) {
						teList.Add (te.DeepCopy ());
					}
				}

			}

			//持ち駒に桂馬がある
			if (this.hand [0] [19] >= 1) {

				te.koma = 19;

				for (int masu = 1; masu <= 63; masu++) {

					//これ以上進めない1段目を除き、駒がなければ桂馬を出す手を追加する


					//駒は(0,0)点からとおく
					te.to = masu;
					te.from = 0;
					te.promote = false;

					if (this.banKoma [masu] == 0) {
						teList.Add (te.DeepCopy ());
					}
				}

			}
		}


		//王手を放置している手を抜く
		int gyoku = 0;

		for (int i = 0; i < teList.Count; i++) {
			KyokumennArray temp = this.DeepCopyKyokumenn ();
			bool isOuteHouchi = false;
			Te teTest = teList [i].DeepCopy();
			temp.Move (teTest);
			temp.SearchGyoku (ref gyoku, this.turn);

			// 玉の周辺（１２方向）から相手の駒が利いていたら、その手は取り除く
			for (int direct = 0; direct < 12 && !isOuteHouchi; direct++) {
				//方向の反対側にある駒の取得

				int masuSerch = gyoku - KomaMoves.diff[direct];

				if (((masuSerch % 9 == 0) && (((masuSerch + KomaMoves.diff[direct]) % 9) == 1)) || ((masuSerch % 9 == 1) && ((masuSerch + KomaMoves.diff[direct])  % 9 == 0))) {
					continue;
				}

				if (1 <= masuSerch && masuSerch <= 81) {
					int koma = temp.banKoma [masuSerch];
					//その駒が敵の駒で手番の玉を取れるか
					if (this.turn % 2 == 1 && 17 <= koma || this.turn % 2 == 0 && koma <= 16 && 1 <= koma) {
						//動けるなら、この手は王手を放置しているので追加しない
						if (KomaMoves.canMove [direct, koma]) {
							isOuteHouchi = true;
							break;
						}
					}
				}
			}
			// 玉の周り（８方向）から相手の駒の飛び利きがあるなら、その手は取り除く
			for (int direct = 0; direct < 8 && !isOuteHouchi; direct++) {

				// 方向の反対方向にある駒を取得
				int masuSerch = gyoku - KomaMoves.diff[direct];



				// その方向にマスがある限り、駒を探す
				while (1 <= masuSerch && masuSerch <= 81) {

					if (((masuSerch % 9 == 0) && (((masuSerch + KomaMoves.diff[direct]) % 9) == 1)) || (((masuSerch % 9 == 1) && ((masuSerch + KomaMoves.diff[direct])  % 9) == 0))) {
						break;
					}

					int koma = temp.banKoma [masuSerch];
					// 味方駒で利きが遮られているなら、チェック終わり
					if(this.turn % 2 == 1 && koma <= 16 && 1 <= koma || this.turn % 2 == 0 && 17 <= koma){
						break;
					}
					// 遮られていない相手の駒の利きがあるなら、王手がかかっている。
					if (this.turn % 2 == 1 && 17 <= koma || this.turn % 2 == 0 && koma <= 16 && 1 <= koma) {
						//動けるなら、この手は王手を放置しているので追加しない
						if (KomaMoves.canJump [direct, koma]) {
							isOuteHouchi = true;
							break;
						} else {
							// 敵駒で利きが遮られているから、チェック終わり
							break;
						}
					}
					//玉から一つ離してループ
					masuSerch -= KomaMoves.diff[direct];
				}

			}


			if (!isOuteHouchi) {
				removed.Add (teList [i]);
			}
		}

		return removed;
	}





	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}