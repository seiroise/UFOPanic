using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFOPanic
{

    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {

        [Header("移動")]

        [SerializeField]
        float _speed = 5f;

        public float speed { get { return _speed; } set { _speed = value; } }

        [SerializeField]
        float _accelerationFactor = 1f;

        [SerializeField]
        float _brakeFactor = 1f;

        [Header("吸い込み")]

        [SerializeField]
        float _attractorPower = 10f;

        public float attractPower { get { return _attractorPower; } set { _attractorPower = value; } }

        [Header("レイヤー関連")]

        [SerializeField]
        LayerMask _downRaycastLayer;

        [SerializeField]
        LayerMask _overlapLayer;

        [SerializeField]
        Vector3 _overlapHalfExtents = new Vector3(2f, 2f, 2f);

        [Header("マーカー関連")]

        [SerializeField]
        Material _material;

        [SerializeField]
        string _positionParam = "_MarkerPosition";

        Rigidbody _rb;
        Vector2 _moveDirection;
        bool _isMove = false;

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

            if (_material)
            {
                _material.SetVector(_positionParam, transform.position);
            }
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
            var diff = _speed - _rb.velocity.magnitude;
            var targetVel = new Vector3(_moveDirection.x, 0f, _moveDirection.y) * speed;
            var f = (targetVel - _rb.velocity) * _accelerationFactor;
            _rb.AddForce(f.x, 0f, f.z);
        }

        void Brake()
        {
            _rb.AddForce(-_rb.velocity * _brakeFactor);
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