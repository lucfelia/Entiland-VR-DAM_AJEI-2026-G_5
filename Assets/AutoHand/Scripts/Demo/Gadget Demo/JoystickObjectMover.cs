using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Autohand.Demo{
    public class JoystickObjectMover : PhysicsGadgetJoystick
    {
        public Transform move;
        public float speed = 2;
        public float movementX;
        public float movementY;
        public bool isGameStarted = false;
        public bool IsMoving => isGameStarted && GetValue().magnitude > 0.1f;
        public Transform startPos;

        void Update()
        {
            if (!isGameStarted) return;

            var axis = GetValue();
            movementX = axis.x * Time.deltaTime * speed;
            movementY = axis.y * Time.deltaTime * speed;
            float x = 0.0f;
            float y = 0.0f;

            if (Math.Abs(movementX) > Math.Abs(movementY))
            {
                movementY = 0;
                x = (movementX > 0 ? 1.0f : -1.0f) * Time.deltaTime * speed;
            }
            if (Math.Abs(movementY) > Math.Abs(movementX))
            {
                movementX = 0;
                y = (movementY > 0 ? 1.0f : -1.0f) * Time.deltaTime * speed;
            }

            move.transform.localPosition += new Vector3(x, 0, y);

            Vector3 pos = move.transform.localPosition;
            Vector3 limit = startPos.localPosition;

            pos.x = Mathf.Clamp(pos.x, -Mathf.Abs(limit.x), Mathf.Abs(limit.x));
            pos.z = Mathf.Clamp(pos.z, -Mathf.Abs(limit.z), Mathf.Abs(limit.z));

            move.transform.localPosition = pos;
        }
    }
}
