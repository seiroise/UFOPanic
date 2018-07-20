using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UFOPanic
{

    public class PlayerInput : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {

        [SerializeField]
        Player _player;

        Vector2 _start;
        Vector2 _current;

        bool _isDragging = false;

        void Update()
        {
            if (_player && _isDragging)
            {
                _player.SetMoveDirection(_current - _start);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            _current = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _start = _current = Vector2.zero;
            _isDragging = false;
            _player.isMove = false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _start = eventData.position;
            _isDragging = true;
            _player.isMove = true;
        }
    }
}