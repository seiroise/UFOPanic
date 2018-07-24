using UnityEngine;

namespace UFOPanic
{

    public class PlayerAttractor : MonoBehaviour
    {

		[SerializeField]
		PlayerLevelManager _levelManager;

		[SerializeField]
		AttractorField _attractorField;

        void OnTriggerEnter(Collider collider)
        {
            var attracted = collider.GetComponent<AttractedObject>();
            if (attracted)
            {
				_levelManager.AddPoint(attracted.point);
				_attractorField.RemoveAttracted(attracted);
                Destroy(attracted.gameObject);
            }
        }
    }
}