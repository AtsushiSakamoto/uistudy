//評価関数差分計算


using System.Collections.Generic;
using UnityEngine;

public class Kyokumenn {


	public int[] banKoma = new int[82];

	public  int turn = new int();                             //現在の手番
	public bool josekiBool = true;                            //定跡の利用フラグ
	private int gyokuSente = 77;                                 //先手玉の位置
	private int gyokuGote = 5;                                 //後手玉の位置
	public int eval;                                 //評価値を確保し駒の移動時に増減
	public int sameKyokumenn;                                 //千日手のための同じ局面カウント

	public int[][] hand = {
		new int[32] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		new int[32] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
	};

	//public int senkei = 0;                //戦型0:不明,1:相掛かり,2;居飛車対振り飛車,3;振り飛車対居飛車,4;相振り


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
		0, 105 ,630, 735, 1050, 1260, 1910, 2100, 100000, 1200, 1200, 1200, 1200,0, 2000, 2200,
		0,-105,-630,-735,-1050,-1260,-1910,-2100,-100000,-1200,-1200,-1200,-1200,0,-2000,-2200
	};

	public static int[] KomaValue = new int[32]{
		0,   100,   600,   700,  1000,  1200,  1900,  2000, 10000,  1200,  1200,  1200,  1200,0,  2150,  2300,
		0,  -100,  -600,  -700, -1000, -1200, -1800, -2000,-10000, -1200, -1200, -1200, -1200,0, -2150, -2300
	};



	public static int[,] DanKomaValue = new int[32,9]{

		//空
		{ 0,0,0,0,0,0,0,0,0},
		//歩
		{ 0,10,10,10,3,1, 0, 0, 0},
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
		{ 10,10,10, 0, 0, 0,  -5, 0, 0},
		//空
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0},
		//歩
		{ 0, 0, 0, -1, -3,-10,-10,-10, 0},
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
		{ 0, 0, 5, 0, 0, 0,-10,-10,-10}

	};

	//銀の駒組み
	public static int[][][] komagumiGin = { 
		
		new int[][]
		{ // 不明
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10,  5, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -4,  0, -10 },
			new int[9]{ -10, -10,  0, -10, -10, -10, -4, -3, -10 },
			new int[9]{ -10, -5, -10, -5, -10, -10, -5, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 }
		},
		new int[][]
		{ // 相掛かり
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10,  0, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -5, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 }
		},
		new int[][]
		{ // IvsFURI 舟囲い、美濃、銀冠
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -7, -10, -10, -10, -10, -10,  7, -10 },
			new int[9]{ -10,  7, -8, -7, 10, -10, 10,  6, -10 },
			new int[9]{ -10, -2, -6, -5, -10,  6, -10, -10, -10 },
			new int[9]{ -10, -7,  0, -10, -10, -10, -10, -10, -10 }
		},
		new int[][]
		{ // FURIvsI 美濃囲い、銀冠
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -3, -7, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -7,  4,  6, -10, -10, -10,  6, -10 },
			new int[9]{ -10,  2,  3,  3, -10, -10,  4, -10, -10 },
			new int[9]{ -10, -10, -10,  0, -10, -10,  0, -10, -10 }
		},
		new int[][]
			{ // FURIvsFURI 矢倉（逆）、美濃、銀冠
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -7, -7, -10 },
			new int[9]{ -10, -10, -10, -10, -10,  5, 10, 10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10,  0, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -5, -10, -10 }
		}
	};

	//金の駒組み
	public static int[][][] komagumiKin = { 

		new int[][]
		{ // 不明
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{-10,-10,-10,-10,-10,-10,-10,-10,-10},
			new int[9]{-10,-10,-10,-10,-10,-10,-10,-10,-10},
			new int[9]{-10,-10,-10,-10,-10,-10,-10,-10,-10},
			new int[9]{-10,-10,  3,-10,  5,-10,-10,-10,-10},
			new int[9]{-10,-10,-10,  0,-10,  0,-10,-10,-10}
		},
		new int[][]
		{ // 相掛かり
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{-10,-10,-10,  6,-10,-10,-10,-10,-10},
			new int[9]{-10,-10, 10,-10,  3,-10,-10,-10,-10},
			new int[9]{-10,-10,-10,  0,-10,  0,-10,-10,-10}
		},
		new int[][]
		{ // IvsFURI 舟囲い、美濃、銀冠
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{-10,-10,-10,-10,-10,-10,-10,-10,-10},
			new int[9]{-10,-10,-10,-10,-10,-10,-10,-10,-10},
			new int[9]{-10,-10,-10,  1,  2,-10,-10,-10,-10},
			new int[9]{-10,-10,-10,  0,-10, -4,-10,-10,-10}
		},
		new int[][]
		{ // FURIvsI 美濃囲い、銀冠
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{-10,-10,-10,-10,-10,-10,-10,-10,-10},
			new int[9]{-10,-10,-10,-10,-10,  5,  1,-10,-10},
			new int[9]{-10,-10,-10,-10,  4,  3,  7, -3,-10},
			new int[9]{-10,-10,-10,  0,  1,  5,  2, -7,-10}
		},
		new int[][]
		{ // FURIvsFURI 矢倉（逆）、美濃、銀冠
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9] {-10,-10,-10,-10,-10,-10,-10,-10,-10},
			new int[9]{-10,-10,-10,-10,-10,  7, -3,-10,-10},
			new int[9]{-10,-10,-10,-10,  5,  3,  6,-10,-10},
			new int[9]      {-10,-10,-10,-10,-10,  5,  4,-10,-10}
		}
	};

	//玉の駒組み
	public static int[][][] komagumiGyoku = { 

		new int[][]
		{ // 不明
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{-10,-10,-10,-10,-10,-10,-10,-10,-10},
			new int[9]{-10,-10,-10,-10,-10,-10,-10,-10,-10},
			new int[9]{- 3, -4, -3,-10,-10,-10,-10,-10,-10},
			new int[9]{  6,  8,  0,- 4,-10,-10,-10,-10,-10},
			new int[9]{ 10,  6, -4,- 6,- 7,-10,-10,-10,-10}
		},
		new int[][]
		{ // 相掛かり
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{- 3, -4, -3,-10,-10,-10,-10,-10,-10},
			new int[9]{  6,  8,  0,- 4,-10,-10,-10,-10,-10},
			new int[9]{ 10,  6, -4,- 6,- 7,-10,-10,-10,-10}
		},
		new int[][]
		{ // IvsFURI 舟囲い、美濃、銀冠
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{-10,-10,-10,-10,-10,-10,-10,-10,-10},
			new int[9]{- 7,  9,-10,-10,-10,-10,-10,-10,-10},
			new int[9]{  5,  7,  8,  4,-10,-10,-10,-10,-10},
			new int[9]{ 10,  5,  3,-10,-10,-10,-10,-10,-10}
		},
		new int[][]
		{ // FURIvsI 美濃囲い、銀冠
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{-10,-10,-10,-10,-10,-10,-10,-10,-10},
			new int[9]{-10,-10,-10,-10,-10,-10,-10,-10,-10},
			new int[9]{10,-10,-10,-10,-10,  4,  6, 10,  6},
			new int[9]{-10,-10,-10,-10,-10,  4,  6,  5, 10}
		},
		new int[][]
		{ // FURIvsFURI 矢倉（逆）、美濃、銀冠
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]{ -10, -10, -10, -10, -10, -10, -10, -10, -10 },
			new int[9]      {-10,-10,-10,-10,-10,-10,-10,-10,-10},
			new int[9]{-10,-10,-10,-10,-10,-10,-10,-10,-10},
			new int[9]{-10,-10,-10,-10,-10,  4,  6, 10,  6},
			new int[9]{-10,-10,-10,-10,-10,  4,  6,  5, 10}
		}
	};



	public void BanShokika(){

		for (int i = 0; i < 82; i++) {
			this.banKoma [i] = Kyokumenn.SHOKI_BAN [i];
		}

		this.turn = 1;
		this.josekiBool = true;
		this.sameKyokumenn = 0;

		this.hand = new int[][]{
			new int[32]{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			new int[32]{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
		};
		//this.senkei = 0;
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


		for (int i = 0; i < 82; i++) {
			obj.banKoma [i] = (int)this.banKoma[i];
		}

		return obj;
	} 


	//局面を評価する関数
	public int evaluate(){

		int eval = 0;
		/*
		int hisyaSenteSuji = (SearchHisya (1) - 1 ) % 9 + 1;
		int hisyaGoteSuji = (SearchHisya (0) - 1) % 9 + 1;

		if (hisyaSenteSuji <= 5 && hisyaGoteSuji >= 5) {
			senkei = 4;
		}else if(hisyaSenteSuji <= 5 && hisyaGoteSuji < 5){
			senkei = 3;
		}else if(hisyaSenteSuji > 5 && hisyaGoteSuji >= 5){
			senkei = 2;
		}else if(hisyaSenteSuji > 5 && hisyaGoteSuji < 5){
			senkei = 1;
		}else{
			senkei = 0;
		}
*/

		//盤面上の駒の価値を全部加算
		for (int i = 1; i < 82;i++) {                  

			int koma = this.banKoma [i];
			eval += Kyokumenn.DanKomaValue [koma,(i - 1) / 9] + Kyokumenn.KomaValue [koma];
			/*
			switch (koma) {
			case 4:
				eval += Kyokumenn.komagumiGin [senkei] [(i - 1) / 9] [(i - 1) % 9];
				break;
			case 5:
				eval += Kyokumenn.komagumiKin [senkei] [(i - 1) / 9] [(i - 1) % 9];
				break;
			case 8:
				eval += Kyokumenn.komagumiGyoku [senkei] [(i - 1) / 9] [(i - 1) % 9];
				break;
			case 20:
				eval -= Kyokumenn.komagumiGin [senkei] [8 - ((i - 1) / 9)] [8- ((i - 1) % 9)];
				break;
			case 21:
				eval -= Kyokumenn.komagumiKin [senkei] [8 - ((i - 1) / 9)] [8- ((i - 1) % 9)];
				break;
			case 24:
				eval -= Kyokumenn.komagumiGyoku [senkei] [8 - ((i - 1) / 9)] [8 - ((i - 1) % 9)];
				break;
			default:
				eval += Kyokumenn.DanKomaValue [koma,(i - 1) / 9] + Kyokumenn.KomaValue [koma];
				break;
			}

*/
		}

		//持ち駒の加算

		for (int j = 0; j < 32; j++) {
			eval += this.hand [0] [j] * Kyokumenn.MotiKomaValue [j] + this.hand [1] [j] * Kyokumenn.MotiKomaValue [j];
		}


		return eval;
	}


	public void Sub(ref int diffDan,int diffSuji){
		diffDan = -diffDan;
		diffSuji = -diffSuji;
	}


	public int SearchHisya(int turn){
		//探す駒はturn側の飛車
		for(int i = 1;i <= 81;i++){

			if (this.banKoma [i] == 7 && turn % 2 == 1 || this.banKoma [i] == 23 && turn % 2 == 0) {
				return i;
			}

		}
		return 0;
	}

	public int SearchKaku(int turn){
		//探す駒はturn側の角
		for(int i = 1;i <= 81;i++){

			if (this.banKoma [i] == 6 && turn % 2 == 1 || this.banKoma [i] == 22 && turn % 2 == 0) {
				return i;
			}

		}
		return 0;
	}

	public void SortTe(ref List<Te> teList){
		
		for (int i = 0; i < teList.Count - 1; i++) {
			for (int j = 0; j < teList.Count - 1; j++) {
				
				this.Move(teList[i]);
				int evalS = this.evaluate ();
				this.Back (teList [i]);

				this.Move(teList[i + 1]);
				int evalL = this.evaluate ();
				this.Back (teList [i + 1]);

				if (this.turn % 2 == 1) {

					if (evalS < evalL) {
						Te tmp = teList [i].DeepCopy ();
						teList [i] = teList [i + 1].DeepCopy ();
						teList [i + 1] = tmp;

					}
				} else {
					
					if (evalS > evalL) {
						Te tmp = teList [i].DeepCopy ();
						teList [i] = teList [i + 1].DeepCopy ();
						teList [i + 1] = tmp;

					}
				}
			}	
		}

		return;
	}

	//指定したところに駒を置く
	public void Put(int i,int koma){
		
		if(koma == 8){
			gyokuSente = i;
		}
		if(koma == 24){
			gyokuGote = i;
		}

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

	public void Back(Te te){

		//取った駒を盤に戻す
		Put(te.to,te.capture);

		//取った駒がある時
		if(te.capture != 0){

			//持ち駒を減らす
			if (8 < te.capture && te.capture <= 16 || 24 < te.capture && te.capture <= 32) {
				//先手の駒か後手の駒か
				if (0 != te.capture && te.capture <= 16) {

					this.hand [0] [te.capture + 8] -= 1;       

				} else if (te.capture != 0 && 17 <= te.capture) {

					this.hand [1] [te.capture - 24] -= 1;                      
				}

			} else {
				
				if (0 != te.capture && te.capture <= 16) {

					this.hand [0] [te.capture + 16] -= 1;       

				} else if (te.capture != 0 && 17 <= te.capture) {

					this.hand [1] [te.capture - 16] -= 1;                      
				}
			}
		}

		if (te.from == 0) {
			//駒打ちなので元の位置に戻す
			//先手の駒か後手の駒か
			if (0 != te.koma && te.koma <= 16) {

				this.hand [1] [te.koma] += 1;       

			} else if (te.koma != 0 && 17 <= te.koma) {

				this.hand [0] [te.koma] += 1;                      
			}
		} else {
			//盤上の駒を動かしたので、その駒を元に戻す
			Put(te.from,te.koma);

		}



	}

	//打ち歩詰めの判定関数
	public bool IsUtifuDume(Te te){
		//歩(1,17)で打たれた時(段が0)に相手に合法手がなければ打ち歩詰め
		if (te.koma == 1 || te.koma == 17) {
			if (te.from == 0) {

				this.Move (te.DeepCopy());
				this.turn += 1;

				if (this.GenerateLegalMoves ().Count == 0) {
					return true;
				}
				this.Back (te.DeepCopy ());
				this.turn -= 1;
			}
		}
		return false;
	} 




	//局面が同一かどうか
	public bool equals(Kyokumenn k){
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
							te.capture = this.banKoma [te.to];
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
								te.capture = this.banKoma [te.to];
								if ((this.turn % 2 == 1 && (toKoma == 0 || toKoma >= 17)) || (this.turn % 2 == 0 && toKoma <= 16)) {
									//香車は敵の一段目まで行って成らずは不可
									if ((te.koma != 2 || te.to > 9) && (te.koma != 18 || te.to < 73)) {
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
						te.capture = this.banKoma [te.to];

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
						te.capture = this.banKoma [te.to];

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
						te.capture = this.banKoma [te.to];

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
					te.capture = this.banKoma [te.to];

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
					te.capture = this.banKoma [te.to];

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
						te.capture = this.banKoma [te.to];

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
					te.capture = this.banKoma [te.to];

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
					te.capture = this.banKoma [te.to];

					if (this.banKoma [masu] == 0) {
						teList.Add (te.DeepCopy ());
					}
				}

			}
		}


		//王手を放置している手を抜く
		int gyoku = 0;
			

		for (int i = 0; i < teList.Count; i++) {
			bool isOuteHouchi = false;
			Te teTest = teList [i];
			this.Move (teTest);

			if(this.turn % 2 == 1){
				gyoku = gyokuSente;
			}else{
				gyoku = gyokuGote;
			}

			// 玉の周辺（１２方向）から相手の駒が利いていたら、その手は取り除く
			for (int direct = 0; direct < 12 && !isOuteHouchi; direct++) {
				//方向の反対側にある駒の取得

				int masuSerch = gyoku - KomaMoves.diff[direct];

				if (((masuSerch % 9 == 0) && (((masuSerch + KomaMoves.diff[direct]) % 9) == 1)) || ((masuSerch % 9 == 1) && ((masuSerch + KomaMoves.diff[direct])  % 9 == 0))) {
					continue;
				}

				if (1 <= masuSerch && masuSerch <= 81) {
					int koma = this.banKoma [masuSerch];
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

					int koma = this.banKoma [masuSerch];
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

			this.Back (teTest);

			if (!isOuteHouchi) {
				removed.Add (teList [i]);
			}
		}

		return removed;
	}



}