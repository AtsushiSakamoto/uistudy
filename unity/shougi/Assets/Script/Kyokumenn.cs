using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kyokumenn {


	public int[,] banKoma = new int[11,11];                  //盤の駒
	public  int turn = new int();                             //現在の手番
	public List<List<int>> hand = new List<List<int>>{new List<int>(),new List<int>()};


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

	public List<Te> GenerateLegalMoves(){
		//合法手を格納する変数
		List<Te> teList = new List<Te>();
		for (int dan = 1; dan <= 9; dan++) {                  
			for (int suji = 1; suji <= 9; suji++) {
				
				int koma = this.banKoma [dan, suji];
				//探索する駒が手番の駒かどうか
				if ((this.turn % 2 == 1 && koma >= 1 && koma <= 16) || (this.turn % 2 == 0 && koma >= 17)) {
					
					//各方向に移動する手を生成
					for(int direct = 0;direct < 12;direct++){
						if (KomaMoves.canMove [direct, koma]) {

							Te te = new Te ();
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
									te.promote = false;
									teList.Add (te.DeepCopy());

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
								Te te = new Te ();
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
										te.promote = false;
										teList.Add (te.DeepCopy());

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
		return teList;
	}





	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
