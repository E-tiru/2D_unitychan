using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 接触したオブジェクトを自身の子にする
// 動く床に乗った時などに使用
public class TriggerToChild : MonoBehaviour 
{

	//衝突相手と、元の親を登録
	Dictionary<GameObject, Transform> cache = new Dictionary<GameObject, Transform>();

	// Unity専用の関数 ////////////////////////////////////////////////////////////////////////////////


	// 何かが範囲に入った時に呼ばれる
	void OnTriggerEnter2D( Collider2D collision )
	{
		

		if( cache.ContainsKey( collision.gameObject ) )
			return;

		cache.Add( collision.gameObject, collision.gameObject.transform.parent );
		collision.gameObject.transform.SetParent( transform );
	}

	// 何かが範囲を出た時に呼ばれる
	void OnTriggerExit2D( Collider2D collision )
	{
		
			collision.gameObject.transform.SetParent( cache[collision.gameObject] );
			cache.Remove( collision.gameObject );
		
	}
}
