using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UFOPanic
{
	public class PlayerCamera : MonoBehaviour
	{

		[SerializeField]
		Transform _target;

		[SerializeField]
		Vector3 _offset;

		[SerializeField]
		Vector3 _lookoffset;

		[SerializeField]
		float _angle = 30f;

		[SerializeField]
		float _distance = 10f;

		void Update()
		{
			SetPosition();
		}

		void SetPosition()
		{
			if (_target)
			{
				var rad = Mathf.Deg2Rad * _angle;
				transform.position = _target.position + new Vector3(0f, Mathf.Cos(rad), Mathf.Sin(rad)) * _distance;
			}
		}

#if UNITY_EDITOR

		[CustomEditor(typeof(PlayerCamera))]
		class InternalEditor : Editor
		{

			public override void OnInspectorGUI()
			{
				base.OnInspectorGUI();

				var self = target as PlayerCamera;
				
				if (GUILayout.Button("Set position"))
				{
					self.SetPosition();
				}
			}

		}
#endif
	}
}