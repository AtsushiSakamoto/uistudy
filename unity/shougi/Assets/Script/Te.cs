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
	public int capture;


	public int from;
	public int to;

	public Te(){
	}

	public Te(int  _koma,int _from, int _to,bool _promote){
		koma = _koma;
		from = _from;
		to= _to;
		promote = _promote;
	}

	public Te(int  _koma,int _from, int _to,bool _promote,int _capture){
		koma = _koma;
		from = _from;
		to= _to;
		promote = _promote;
		capture = _capture;
	}


	public Te(int _koma,int _from_dan,int _from_suji,int _to_dan,int _to_suji,bool _promote){

		koma = _koma;
		from_dan = _from_dan;
		from_suji = _from_suji;
		to_dan = _to_dan;
		to_suji = _to_suji;
		promote = _promote;

	}


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