using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masu : MonoBehaviour {

	public int koma;
	public Sprite hu;
	public Sprite sekyo;
	public Sprite empty;
	private int select = 0;

	public void move(){


		if (select == 1) {
			Debug.Log("move");
			select = 0;
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();

			switch (koma) {
			case 0:
				renderer.sprite = empty;
				break;
			case 1:
				renderer.sprite = hu;
				break;
			default:
				renderer.sprite = sekyo;
				break;
			}
		} else {
			Debug.Log("mo");
			koma = 0;
			select = 1;
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();
			renderer.sprite = empty;
		}
	}

	public void test(){
		Debug.Log ("test");
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
		renderer.sprite = hu;
	}

	// Use this for initialization
	void Start () {
		koma = 2;
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
		renderer.sprite = hu;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
