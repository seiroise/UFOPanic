using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFOPanic
{

	/// <summary>
	/// 引き寄せフィールド
	/// </summary>
	public class AttractorField : MonoBehaviour
	{

		/// <summary>
		/// 吸い込みの中心
		/// </summary>
		[SerializeField]
		Transform _gravity;

		/// <summary>
		/// 吸い込み力
		/// </summary>
		[SerializeField]
		int _power;

		public int power { get { return _power; } set { _power = value; } }

		/// <summary>
		/// 吸い込む速度
		/// </summary>
		[SerializeField]
		float _attractedSpeed;

		HashSet<AttractedObject> _attractedSet;

		#region MonoBehaviourイベント

		void Start()
		{
			_attractedSet = new HashSet<AttractedObject>();
		}

		void FixedUpdate()
		{
			var enu = _attractedSet.GetEnumerator();
			while (enu.MoveNext())
			{
				var cur = enu.Current;
				if (cur.attractedMass <= _power)
				{
					var diff = _gravity.position - cur.transform.position;
					var dir = diff - cur.rb.velocity;
					diff.x /= Mathf.Clamp01(diff.y * 0.1f) + 0.001f;
					// cur.rb.AddForce(diff.normalized * _attractedSpeed,  ForceMode.Impulse);
					// cur.rb.AddForce(dir.normalized * _attractedSpeed, ForceMode.Impulse);

					// y軸の差が小さいほどx,z軸方向を強める
					var f = new Vector3(diff.x, 0f, diff.z).normalized;
					f *= 10f / (Mathf.Abs(diff.y) + 0.1f);
					f.y = diff.y * 0.5f;

					cur.rb.AddForce(f, ForceMode.Impulse);

				}
			}
		}

		void OnTriggerEnter(Collider other)
		{
			var attracted = other.GetComponent<AttractedObject>();
			if (attracted)
			{
				if (!_attractedSet.Contains(attracted))
				{
					_attractedSet.Add(attracted);
				}
			}
		}

		void OnTriggerExit(Collider other)
		{
			var attracted = other.GetComponent<AttractedObject>();
			if (attracted)
			{
				if (_attractedSet.Contains(attracted))
				{
					_attractedSet.Remove(attracted);
				}
			}
		}

		#endregion

		#region 外部インタフェース

		public void RemoveAttracted(AttractedObject target)
		{
			if (_attractedSet.Contains(target))
			{
				_attractedSet.Remove(target);
			}
		}

		#endregion
	}
}