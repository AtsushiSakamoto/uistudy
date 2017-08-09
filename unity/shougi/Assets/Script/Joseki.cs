using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class Joseki{


	//	Joseki child = null;
	byte[][] josekiData;
	int numJoseki;
	Kyokumenn josekiKyokumenn;
	public string path;



	public Joseki(string josekiPath){
		
		/*
		if(josekiFileName.IndexOf(",") >= 0){

			//「子」の定跡として,”,”以降のファイルを読み込む
			child = new Joseki(josekiFileName.Substring(josekiFileName.IndexOf(",") + 1));

			//自分自身の読み込む定跡ファイルは,の前までのファイル名
			josekiFileName = josekiFileName.Substring(0,josekiFileName.IndexOf(",") - 1);
		}
*/
		//ファイルから定跡データを読み込む
		path = josekiPath;
		FileStream f = new FileStream(path,FileMode.Open,FileAccess.Read);

		try{


			numJoseki = (int)(f.Length/512);

			josekiData = new byte[numJoseki][];
			for(int i = 0;i < numJoseki;i++){
				josekiData[i] = new byte[512];
			}

			for(int i = 0;i < numJoseki && f.Read(josekiData[i],0,512) > 0;i++){

			}

		}catch(Exception){

			numJoseki = 0;
			Debug.Log("定跡来てないよ〜(●・▽・●)");

		}finally{

			f.Close ();
		}
	}


	public Te josekiByteToTe(byte toByte,byte fromByte,Kyokumenn k){

		int f = ((int)fromByte) & 0xff;
		int t = ((int)toByte) & 0xff;
		int koma = 0;
		int fs,fd,ts,td;
		bool promote = false;


		if (f > 100) {
			//fが100以上なら、持ち駒を打つ手
			if (k.turn % 2 == 1) {
				koma = (f - 100);
			} else {
				koma = (f - 100) + 16;
			}
			fs = 0;
			fd = 1;
		} else {

			//fをこのプログラムの中で使う座標の方式へ変換
			fs = 10 - ((f - 1) % 9 + 1);               //筋
			fd = (f + 8) / 9;                          //段

			koma = k.banKoma [(fd - 1) * 9 + fs];
		}

		//tが100以上なら成り手
		if(t > 100){
			promote = true;
			t -= 100;
		}
		ts = 10 - ((t - 1) % 9 + 1);               //筋
		td = (t + 8) / 9;                          //段
		Te te = new Te();
		te.koma = koma;

		te.from = (fd - 1) * 9 + fs;
		te.to = (td - 1) * 9 + ts;
		te.promote = promote;
		te.capture = k.banKoma [te.to];
		return te;
	}

	public Te fromjoseki(Kyokumenn k,int tesu){
		//tesuには実際の手数が渡されるが、定跡データは0手目から始まるので1ずらしておく
		tesu = tesu - 1;
		//定跡にあった候補手を入れる
		List <Te> teList = new List<Te>();
		//定跡で進めた局面を作り、渡された局面と比較する
		Kyokumenn josekiKyokumenn = new Kyokumenn();

		for (int i = 0; i < numJoseki; i++) {
			//平手で初期化
			josekiKyokumenn.BanShokika();

			for (int j = 0; j < tesu; j++) {

				if (josekiData [i][ j * 2] == (byte)0 || josekiData [i][ j * 2] == (byte)0xff) {
					break;
				}
				Te te = josekiByteToTe (josekiData [i][ j * 2], josekiData [i][j * 2 + 1], josekiKyokumenn);
				josekiKyokumenn.Move (te);
				josekiKyokumenn.turn += 1;
			}
			//局面が一致するか
			if(josekiKyokumenn.equals(k)){
				if (josekiData [i][tesu * 2] == (byte)0 || josekiData [i][tesu * 2] == (byte)0xff) {
					continue;
				}
				//候補手を生成
				Te te = josekiByteToTe(josekiData[i][tesu*2],josekiData[i][tesu*2+1],k);
				teList.Add (te);

			}
		}


		if (teList.Count == 0) {
			//候補手がない場合
			/*
			if(child != null){
				//子定跡がある時は、その結果を返す
				return child.fromjoseki(k,tesu);
			}
*/
			//候補手がなかったのでnullを返す
			return null;
		}else{
			//候補手の中からランダム
			Debug.Log("定石通り！(●・▽・●)");
			//候補手がない場合
			return teList[UnityEngine.Random.Range(0, teList.Count)];
		}


	}



	// Update is called once per frame
	void Update () {

	}
}
