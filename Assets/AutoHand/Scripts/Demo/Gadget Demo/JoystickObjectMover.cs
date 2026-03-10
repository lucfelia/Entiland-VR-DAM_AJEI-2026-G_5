using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Autohand.Demo{
    public class JoystickObjectMover : PhysicsGadgetJoystick{
        public Transform move;
        public float speed = 2;
        public float movementX;
        public float movementY;
        public bool isGameStarted =false;
        
        void Update(){
            if(!isGameStarted)return;
            var axis = GetValue();
            movementX= axis.x * Time.deltaTime * speed;
            movementY= axis.y * Time.deltaTime * speed;

            float x = 0.0f;
            float y = 0.0f;
            //var moveAxis = new Vector3(axis.x*Time.deltaTime*speed, 0, axis.y*Time.deltaTime*speed);
            
            if (Math.Abs(movementX) > Math.Abs(movementY)) {
                movementY = 0;
                if (movementX > 0)
                {
                    x = 1.0f * Time.deltaTime * speed;
                }
                else if (movementX < 0)
                {
                    x = -1.0f * Time.deltaTime * speed;
                }
            }
            if (Math.Abs(movementY) > Math.Abs(movementX)) {
                movementX = 0;
                if (movementY > 0)
                {
                    y = 1.0f * Time.deltaTime * speed;
                }
                else if (movementY < 0)
                {
                    y = -1.0f * Time.deltaTime * speed;
                }
            }
            var moveAxis = new Vector3(x, 0, y);
            move.transform.localPosition += moveAxis;
        }
    }
}
