using UnityEngine;

namespace UFOPanic
{
    public class AttractedObject : MonoBehaviour
    {

        [SerializeField]
        int _point;

        public int point { get { return _point; } }
    }
}