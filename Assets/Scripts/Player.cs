using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFOPanic
{

    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {

        [SerializeField]
        float _maxSpeed = 5f;

        [SerializeField]
        LayerMask _downRaycastLayer;

        [SerializeField]
        LayerMask _overlapLayer;

        [SerializeField]
        Vector3 _overlapHalfExtents = new Vector3(2f, 2f, 2f);

        [SerializeField]
        float _attractorPower = 10f;

        Rigidbody _rb;
        Vector2 _moveDirection;
        bool _isMove;

        public bool isMove { get { return _isMove; } set { _isMove = value; } }

        // Use this for initialization
        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            if (_isMove)
            {
                Move();
            }
            else
            {
                Brake();
            }

            AttractDown();
        }

        #region 外部インタフェース

        public void SetMoveDirection(Vector2 direction)
        {
            _moveDirection = direction.normalized;
        }

        #endregion

        #region 内部処理

        void Move()
        {
            var diff = _maxSpeed - _rb.velocity.magnitude;
            var m = _moveDirection * diff;
            _rb.AddForce(m.x, 0f, m.y);
        }

        void Brake()
        {
            _rb.AddForce(-_rb.velocity);
        }

        /// <summary>
        /// 下方向から吸引する
        /// </summary>
        void AttractDown()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f, _downRaycastLayer))
            {
                var colliders = Physics.OverlapBox(hit.point, _overlapHalfExtents);
                for (var i = 0; i < colliders.Length; ++i)
                {
                    var rb = colliders[i].GetComponent<Rigidbody>();
                    if (rb)
                    {
                        rb.AddForce(Vector3.up * _attractorPower);
                    }
                }
            }
        }

        #endregion
    }
}