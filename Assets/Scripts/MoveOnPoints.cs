using UnityEngine;
using System.Collections;
using System.Collections.Generic;   //Listを使用する為に必要

// 指定地点上を移動する
// MovePointのタグがついた子供の座標を順に移動する
public class MoveOnPoints : MonoBehaviour
{
	// 変数定義 ////////////////////////////////////////////////////////////////////////////////////////

	//移動速度
	[SerializeField]
	private float _moveSpeed = 10f;

	// 移動地点
	private Vector3[] _points;

	//各移動地点間の距離
	private float[] _pointsDistance;

	//現在の移動距離
	private float _currentDistance = 0f;

	//全移動距離
	private float _totalDistance = 0f;

	// Unity専用の関数 ////////////////////////////////////////////////////////////////////////////////

	// Start ゲーム開始時１度だけ呼ばれる.
	void Start()
	{
		//ポイントを取得
		_points = FindAllMovePoints();

		//各ポイント間の距離を取得
		_pointsDistance = new float[_points.Length];
		for( int i = 0; i < _points.Length; i++ )
			_pointsDistance[i] = Vector3.Distance( GetPoint( _points, i ), GetPoint( _points, i + 1 ) );

		//総距離を計算
		_totalDistance = 0f;
		foreach( var dist in _pointsDistance )
			_totalDistance += dist;

		_currentDistance = 0f;

		//ポイントを削除
		DeleteAllMovePoints();
	}


	// Update １フレームごとに呼ばれる
	void Update()
	{
		//移動距離を加算
		_currentDistance = Mathf.Repeat( _currentDistance + _moveSpeed * Time.deltaTime, _totalDistance );

		//移動距離から次の位置を計算し、移動させる
		transform.position = CalcPosition( _currentDistance );
	}

	// エディタ上でギズモを表示の際に呼ばれる
	void OnDrawGizmos()
	{
		//移動地点を線で表示
		var targets = FindAllMovePoints();

		if( targets.Length <= 1 )
			return;


		Gizmos.color = Color.magenta;

		for( int i = 0; i < targets.Length; i++ )
		{
			var p1 = GetPoint( targets, i );
			var p2 = GetPoint( targets, i + 1 );
			Gizmos.DrawLine( p1, p2 );
		}
		Gizmos.color = Color.white;
	}


	// クラス専用の関数 ////////////////////////////////////////////////////////////////////////////////

	//距離から移動地点を計算する
	Vector3 CalcPosition( float distance )
	{
		for( int i = 0; i < _pointsDistance.Length; i++ )
		{
			if( distance - _pointsDistance[i] <= 0f )
			{
				var p1 = GetPoint( _points, i );
				var p2 = GetPoint( _points, i + 1 );
				float dist = _pointsDistance[i];

				return Vector3.Lerp( p1, p2, (float)( distance / dist ) );
			}
			else
				distance -= _pointsDistance[i];
		}

		return Vector3.zero;
	}


	//ポイントを取得
	Vector3 GetPoint( Vector3[] source, int index )
	{
		return source[index % source.Length];
	}

	//全移動ポイントを取得
	Vector3[] FindAllMovePoints()
	{
		var points = new List<Vector3>();

		foreach( Transform ch in transform )
		{
			if( ch.tag == "MovePoint" )
				points.Add( ch.position );
		}

		return points.ToArray();
	}

	//移動ポイントを削除
	void DeleteAllMovePoints()
	{
		foreach( Transform ch in transform )
		{
			if( ch.tag == "MovePoint" )
				Destroy( ch.gameObject );
		}
	}

}
