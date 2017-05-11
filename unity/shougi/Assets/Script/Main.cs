
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {


	public static Kyokumenn k;
	private Position from;
	private Position to;
	GameObject mainCamera;
	GameObject Button;
	Camera main;

	//	public static int[,] ban = new int[11,11];
	/*
	public static int[,] shokibanmen =  new int[9,9]{
		{2,3,4,5,8,5,4,3,2},
		{0,7,0,0,0,0,0,8,0},
		{1,1,1,1,1,1,1,1,1},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{1,1,1,1,1,1,1,1,1},
		{0,8,0,0,0,0,0,7,0},
		{2,3,4,5,8,5,4,3,2}
	};
*/
	/*
	//局面をlogに表示
	public void logKyokumen(){
		for (int dan = 1; dan <= 9; dan++) {
			for (int suji = 9; suji >= 1; suji--) {
				Debug.Log (ban [dan, suji]);
			}
		}
		Debug.Log (ban [7, 6]);
	}

	//ある位置に駒をおく
	public void put(Position p,int koma){
		ban [p.dan, p.suji] = koma;

	}

	//ある位置のコマを取得する。
	public int get(Position p){
		//磐外なら「磐外＝壁」を返す。
		if(p.suji<1||9<p.suji||p.dan<1||9<p.dan){
			return Koma.WALL;
		}
		return ban [p.dan,p.suji];
	}

	//与えられた手で一手すすめる。
	public void move(Te te){
		Debug.Log ("aaaaaaaaaaaaaaaaa");
		//元の位置をEMPTYに
		put(te.from,Koma.EMPTY);
		//移動先に進める
		put(te.to,te.koma);
		ban [6, 6] = 111;
		Debug.Log (ban [6, 6]);
	}

	*/
	//盤面に駒を表示する
	/*
	public loadBanmen(){


		for (int dan = 1; dan <= 9; dan++) {
			for (int suji = 9; suji >= 1; suji--) {
				if(ban[dan,suji] != 0){
					switch(ban[dan,suji]){

					case 1:GameObject.Instantiate(GUIText)
					}
				}
			}
		}
	}
*/
	public void tap(){


		Vector3 screenPos = Input.mousePosition;
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

		worldPos.x = worldPos.x + 2.8f;
		worldPos.y = worldPos.y - 2.8f;
		Debug.Log (worldPos);
/*
		if (selectKoma == 0) {
			switch (worldPos.x > )
			this.from.suji =
		}
*/
		/*
		RectTransform CanvasRect = Canvas.GetComponent<RectTransform>();
		Vector3 screenPos2 = Camera.main.WorldToViewportPoint (worldPos);

		Vector2 WorldObject_ScreenPosition = new Vector2(
			((screenPos2.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
			((screenPos2.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));
		
*/
	//	EventSystem.current.IsPointerOverGameObject ()
		// タッチ座標取得 (タッチ位置のうちのひとつ)


	//	Touch tch = imput.phase.GetTouch (0);
	//	if (Input.touchCount == 1) {

//			Debug.Log (Input.tch [0].position.x);
//}

	}

	public void logtest(){
		k.logKyokumen ();
	}

	public void test(){

		from = new Position(6,7);

		to = new Position (6,6);



		Te te = new Te(111,from,to,true);
		k.move(te);




	}


	// Use this for initialization
	void Start () {

		k = new Kyokumenn ();
		k.banshokika ();

		mainCamera = GameObject.Find ("Main Camera");
		Button = GameObject.Find ("96");


		/*
		//対局開始時盤面を入れる
		for (int dan = 1; dan <= 9; dan++) {
			for (int suji = 9; suji >= 1; suji--) {
				k.ban[dan,suji]= shokibanmen[dan - 1, 9 - suji];
			}
		}

		*/

	}

	// Update is called once per frame
	void Update () {
		

		/*
		Vector3 mousePos = main.ScreenToWorldPoint (Input.mousePosition);
		Collider2D col = Physics2D.OverlapPoint (mousePos);
		//タップ確認
		if (Input.GetMouseButtonDown (0)) {
			if (col == Button.GetComponent<Collider2D> ()) {
				//タップされた時の処理
			}
		}
		*/
	}

}


