using UnityEngine;

namespace UFOPanic
{

    public class PlayerAttractor : MonoBehaviour
    {

		[SerializeField]
		PlayerLevelManager _levelManager;

        void OnTriggerEnter(Collider collider)
        {
            var attracted = collider.GetComponent<AttractedObject>();
            if (attracted)
            {
				_levelManager.AddPoint(attracted.point);
                Destroy(attracted.gameObject);
            }
        }
    }
}