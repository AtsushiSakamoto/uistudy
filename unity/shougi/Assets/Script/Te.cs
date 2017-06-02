using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Te {

	public int koma;
	public int from_dan;
	public int from_suji;
	public int to_dan;
	public int to_suji;
	public bool promote;


	public object Clone()                            // シャローコピーになります。
	{
		return MemberwiseClone();
	}

	public Te ShallowCopy()                          //シャローコピー
	{
		return (Te)this.Clone();
	}

	public Te DeepCopy()                             //ディープコピー
	{
		Te obj = (Te)this.Clone();
		obj.koma = (int)this.koma;       //参照型は全てインスタンスをコピーする
		obj.from_dan = (int)this.from_dan;
		obj.from_suji = (int)this.from_suji;
		obj.to_dan = (int)this.to_dan;
		obj.to_suji = (int)this.to_suji;
		obj.promote = (bool)this.promote;

		return (Te)Clone();
	}





	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
