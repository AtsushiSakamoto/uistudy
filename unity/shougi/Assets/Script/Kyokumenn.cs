using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Kyokumenn {


	public int[,] banKoma = new int[11,11];                  //盤の駒
	public  int turn = new int();                             //現在の手番
	public List<List<int>> hand = new List<List<int>>{new List<int>{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},new List<int>{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};



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

	public static int[,] SHOKI_BAN =  new int[9,9]{           //初期の盤面
		{18,19,20,21,24,21,20,19,18},
		{0,23,0,0,0,0,0,22,0},
		{17,17,17,17,17,17,17,17,17},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{1,1,1,1,1,1,1,1,1},
		{0,6,0,0,0,0,0,7,0},
		{2,3,4,5,8,5,4,3,2}
	};

	public void BanShokika(){

		for (int dan = 1; dan <= 9; dan++) {                  //初期盤面を入れる
			for (int suji = 1; suji <= 9; suji++) {
				this.banKoma[dan,suji]= Kyokumenn.SHOKI_BAN[dan-1,suji-1];
			}
		}
	}

	public object CloneKyokumenn()                            // シャローコピー
	{
		return MemberwiseClone();
	}

	public Kyokumenn ShallowCopyKyokumenn()                          //シャローコピー
	{
		return (Kyokumenn)this.CloneKyokumenn();
	}

	public Kyokumenn DeepCopyKyokumenn()                             //ディープコピー
	{
		Kyokumenn obj = new Kyokumenn ();
		//参照型は全てインスタンスをコピーする
		obj.turn = (int)this.turn;

		for (int i = 0; i < 32; i++) {
			obj.hand [0][i] = this.hand [0] [i];
			obj.hand [1] [i] = (int)this.hand [1] [i];
		}
		for (int i = 1; i <= 9; i++) {
			for (int j = 1; j <= 9; j++) {
				obj.banKoma [i, j] = (int)this.banKoma [i, j];
			}
		}

		return obj;
	} 

	public void Sub(ref int diffDan,int diffSuji){
		diffDan = -diffDan;
		diffSuji = -diffSuji;
	}
		
	public void SearchGyoku(ref int danGyoku,ref int sujiGyoku,int turn){
		//探す駒はturn側の玉
		for(int dan = 1;dan <= 9;dan++){
			for (int suji = 1; suji <= 9; suji++) {
				if (this.banKoma [dan, suji] == 8 && turn % 2 == 1 || this.banKoma [dan, suji] == 24 && turn % 2 == 0) {
					danGyoku = dan;
					sujiGyoku = suji;
				}
			}
		}
	}



	public void Put(int dan,int suji,int koma){
		this.banKoma [dan, suji] = koma;
	}

	public void Move(Te te){
		//駒の行き先に駒があったなら持ち駒にする
		int toKoma = this.banKoma[te.to_dan,te.to_suji];
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
		if (te.from_dan == 0) {
			//使った持ち駒を減らす
			this.hand [this.turn % 2] [te.koma] -= 1;
			this.Put (te.to_dan, te.to_suji, te.koma);
		} else {
			//盤上の駒を進めた
			//移動前は空白に
			this.Put(te.from_dan,te.from_suji,0);
			//移動後は成る場合は成った駒をならない場合はそのままの駒をおく
			if (te.promote) {
				this.Put (te.to_dan, te.to_suji, te.koma + 8);
			} else {
				this.Put (te.to_dan, te.to_suji, te.koma);
			}
		}
	}
	//打ち歩詰めの判定関数
	public bool IsUtifuDume(Te te){
		//歩(1,17)で打たれた時(段が0)に相手に合法手がなければ打ち歩詰め
		if (te.koma == 1 || te.koma == 17) {
			if (te.from_dan == 0) {
				
				Kyokumenn temp = this.DeepCopyKyokumenn ();

				temp.Move (te);
				temp.turn += 1;

				if (temp.GenerateLegalMoves ().Count == 0) {
					return true;
				}
			}
		}
		return false;
	} 


	public List<Te> GenerateLegalMoves(){
		//合法手を格納する変数
		List<Te> teList = new List<Te>();
		List<Te> removed = new List<Te>();
		Te te = new Te ();

		for (int dan = 1; dan <= 9; dan++) {                  
			for (int suji = 1; suji <= 9; suji++) {
				
				int koma = this.banKoma [dan, suji];
				//探索する駒が手番の駒かどうか
				if ((this.turn % 2 == 1 && koma >= 1 && koma <= 16) || (this.turn % 2 == 0 && koma >= 17)) {
					
					//各方向に移動する手を生成
					for(int direct = 0;direct < 12;direct++){
						if (KomaMoves.canMove [direct, koma]) {


							te.to_dan = dan + KomaMoves.diffDan [direct];
							te.to_suji = suji + KomaMoves.diffSuji [direct];
							te.from_dan = dan;
							te.from_suji = suji;
							te.koma = koma;
							//移動先は盤内か
							if(1 <= te.to_dan && te.to_dan <= 9 && 1 <= te.to_suji  && te.to_suji <= 9){
								
								//移動先に自分の駒はないか
								int toKoma = this.banKoma[te.to_dan,te.to_suji];
								if ((this.turn % 2 == 1 && (toKoma == 0 || toKoma >= 17)) || (this.turn % 2 == 0 && toKoma <= 16)) {
									//桂馬は敵二段目まで、歩は一段目までで成らずはできない
									if ((te.koma != 3 || te.to_dan > 2) && (te.koma != 19 || te.to_dan < 8) && (te.koma != 1 || te.to_dan != 1) && (te.koma != 17 || te.to_dan != 9)) {
										te.promote = false;
										teList.Add (te.DeepCopy ());
									}
									//移動先が敵陣
									if((te.to_dan <= 3 && this.turn % 2 == 1) || (te.to_dan >= 7 && this.turn % 2 == 0)){

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
							
							for (int i = 1; i < 9; i++) {
								
								//移動先を生成
								te.to_dan = dan + (KomaMoves.diffDan [direct] * i);
								te.to_suji = suji + (KomaMoves.diffSuji [direct] * i);
								te.from_dan = dan;
								te.from_suji = suji;
								te.koma = koma;

	

								//移動先は盤内か
								if(1 <= te.to_dan && te.to_dan <= 9 && 1 <= te.to_suji  && te.to_suji <= 9){
									//移動先に自分の駒はないか
									int toKoma = this.banKoma[te.to_dan,te.to_suji];
									if ((this.turn % 2 == 1 && (toKoma == 0 || toKoma >= 17)) || (this.turn % 2 == 0 && toKoma <= 16)) {
										//香車は敵の一段目まで行って成らずは不可
										if ((te.koma != 2 || te.to_dan != 1) && (te.koma != 18 || te.to_dan != 9)) {
											te.promote = false;
											teList.Add (te.DeepCopy ());
										}
										//移動先が敵陣
										if((te.to_dan <= 3 && this.turn % 2 == 1) || (te.to_dan >= 7 && this.turn % 2 == 0)){

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
		}

		//持ち駒からだす手を追加(銀、金、角、飛車)
		if (this.turn % 2 == 1) {                              //先手の場合
			//銀~飛車までループ
			for (int i = 4; i <= 7; i++) {
				//探索中の駒を持っているか
				if (this.hand [1] [i] >= 1) {
					te.koma = i;

					for (int dan = 1; dan <= 9; dan++) {                  
						for (int suji = 1; suji <= 9; suji++) {
							//駒は(0,0)点からとおく
							te.to_dan = dan;
							te.to_suji = suji;
							te.from_dan = 0;
							te.from_suji = 0;
							te.promote = false;

							if (this.banKoma [dan, suji] == 0) {
								teList.Add (te.DeepCopy ());
							}
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

					for (int dan = 1; dan <= 9; dan++) {                  
						for (int suji = 1; suji <= 9; suji++) {
							//駒は(0,0)点からとおく
							te.to_dan = dan;
							te.to_suji = suji;
							te.from_dan = 0;
							te.from_suji = 0;
							te.promote = false;

							if (this.banKoma [dan, suji] == 0) {
								teList.Add (te.DeepCopy ());
							}
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
						if (this.banKoma [dan, suji] == 1) {
							isNifu = true;
						}

					}
					if (isNifu) {
						continue;

					}
					//これ以上進めない1段目を除き、駒がなければ歩を出す手を追加する
					for (int dan = 2; dan <= 9; dan++) {                  


						//駒は(0,0)点からとおく
						te.to_dan = dan;
						te.to_suji = suji;
						te.from_dan = 0;
						te.from_suji = 0;
						te.promote = false;

						if (this.banKoma [dan, suji] == 0) {
							teList.Add (te.DeepCopy ());
						}
					}
				}

			}


			//先手の持ち駒に香車がある
			if (this.hand [1] [2] >= 1) {
				te.koma = 2;


				for (int suji = 1; suji <= 9; suji++) {
					
					//これ以上進めない1段目を除き、駒がなければ香車を出す手を追加する
					for (int dan = 2; dan <= 9; dan++) {                  

						//駒は(0,0)点からとおく
						te.to_dan = dan;
						te.to_suji = suji;
						te.from_dan = 0;
						te.from_suji = 0;
						te.promote = false;

						if (this.banKoma [dan, suji] == 0) {
							teList.Add (te.DeepCopy ());
						}
					}
				}

			}

			//先手の持ち駒に香車がある
			if (this.hand [1] [3] >= 1) {
				te.koma = 3;


				for (int suji = 1; suji <= 9; suji++) {

					//これ以上進めない1,2段目を除き、駒がなければ桂馬を出す手を追加する
					for (int dan = 3; dan <= 9; dan++) {                  

						//駒は(0,0)点からとおく
						te.to_dan = dan;
						te.to_suji = suji;
						te.from_dan = 0;
						te.from_suji = 0;
						te.promote = false;

						if (this.banKoma [dan, suji] == 0) {
							teList.Add (te.DeepCopy ());
						}
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
						if (this.banKoma [dan, suji] == 17) {
							isNifu = true;
						}

					}
					if (isNifu) {
						continue;
					}
					//これ以上進めない1段目を除き、駒がなければ歩を出す手を追加する
					for (int dan = 1; dan <= 8; dan++) {                  

						//駒は(0,0)点からとおく
						te.to_dan = dan;
						te.to_suji = suji;
						te.from_dan = 0;
						te.from_suji = 0;
						te.promote = false;

						if (this.banKoma [dan, suji] == 0) {
							teList.Add (te.DeepCopy ());
						}
					}
				}
			}


			//持ち駒に香車がある
			if (this.hand [0] [18] >= 1) {

				te.koma = 18;

				for (int suji = 1; suji <= 9; suji++) {
					
					//これ以上進めない1段目を除き、駒がなければ香車を出す手を追加する
					for (int dan = 1; dan <= 8; dan++) {                  

						//駒は(0,0)点からとおく
						te.to_dan = dan;
						te.to_suji = suji;
						te.from_dan = 0;
						te.from_suji = 0;
						te.promote = false;

						if (this.banKoma [dan, suji] == 0) {
							teList.Add (te.DeepCopy ());
						}
					}
				}
			}

			//持ち駒に桂馬がある
			if (this.hand [0] [19] >= 1) {

				te.koma = 19;

				for (int suji = 1; suji <= 9; suji++) {

					//これ以上進めない1段目を除き、駒がなければ桂馬を出す手を追加する
					for (int dan = 1; dan <= 7; dan++) {                  

						//駒は(0,0)点からとおく
						te.to_dan = dan;
						te.to_suji = suji;
						te.from_dan = 0;
						te.from_suji = 0;
						te.promote = false;

						if (this.banKoma [dan, suji] == 0) {
							teList.Add (te.DeepCopy ());
						}
					}
				}
			}
		}
			

		//王手を放置している手を抜く
		int danGyoku = 0;
		int sujiGyoku = 0;

		for (int i = 0; i < teList.Count; i++) {
			Kyokumenn temp = this.DeepCopyKyokumenn ();
			bool isOuteHouchi = false;
			Te teTest = teList [i].DeepCopy();
			temp.Move (teTest);
			temp.SearchGyoku (ref danGyoku,ref sujiGyoku, this.turn);

			// 玉の周辺（１２方向）から相手の駒が利いていたら、その手は取り除く
			for (int direct = 0; direct < 12 && !isOuteHouchi; direct++) {
				//方向の反対側にある駒の取得
				int danSearch = danGyoku - KomaMoves.diffDan[direct] ;
				int sujiSearch = sujiGyoku - KomaMoves.diffSuji [direct];
				if (1 <= danSearch && danSearch <= 9 && 1 <= sujiSearch && sujiSearch <= 9) {
					int koma = temp.banKoma [danSearch, sujiSearch];
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
				int danSearch = danGyoku - KomaMoves.diffDan[direct] ;
				int sujiSearch = sujiGyoku - KomaMoves.diffSuji [direct];

				// その方向にマスがある限り、駒を探す
				while (1 <= danSearch && danSearch <= 9 && 1 <= sujiSearch && sujiSearch <= 9) {
					int koma = temp.banKoma [danSearch, sujiSearch];
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
					danSearch -= KomaMoves.diffDan[direct] ;
					sujiSearch -= KomaMoves.diffSuji [direct];
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
