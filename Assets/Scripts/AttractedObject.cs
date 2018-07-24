using UnityEngine;

namespace UFOPanic
{
	
    public class AttractedObject : MonoBehaviour
    {

		[SerializeField]
		Rigidbody _rb;

		public Rigidbody rb { get { return _rb; } }

		/// <summary>
		/// Attract‚³‚ê‚é‚Æ‚«‚Ìmass
		/// </summary>
		[SerializeField]
		int _attractedMass = 1;

		public int attractedMass { get { return _attractedMass; } }

        [SerializeField]
        int _point;

        public int point { get { return _point; } }

		private void OnValidate()
		{
			if (Application.isPlaying)
			{
				return;
			}

			_rb = GetComponent<Rigidbody>();
		}
	}
}