using UnityEngine;

namespace UFOPanic
{
    public class PlayerCamera : MonoBehaviour
    {

        [SerializeField]
        Transform _target;

        [SerializeField]
        Vector3 _offset;

        void Update()
        {
            if (_target)
            {
                transform.position = _target.position + _offset;
            }
        }
    }
}