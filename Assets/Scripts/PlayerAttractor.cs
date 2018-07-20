using UnityEngine;

namespace UFOPanic
{

    public class PlayerAttractor : MonoBehaviour
    {

        void OnTriggerEnter(Collider collider)
        {
            var attracted = collider.GetComponent<AttractedObject>();
            if (attracted)
            {
                Debug.Log(attracted.point);
                Destroy(attracted.gameObject);
            }
        }
    }
}