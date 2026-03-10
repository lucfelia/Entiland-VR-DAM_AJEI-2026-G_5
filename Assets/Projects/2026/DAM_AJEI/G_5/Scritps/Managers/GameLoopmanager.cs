using UnityEngine;


namespace Autohand.Demo
{ 
    public class GameLoopManager : MonoBehaviour
    {

        public JoystickObjectMover joystcik;
        public Timer timer;
        public GameObject InitialPos;

        private void Update()
        {
            if (joystcik.move.localPosition != InitialPos.transform.position) return;
            //if(timer.bIsFinished) return;
            //timer.RestartGame = true;
        }

        public void StartMovement()
        {
            joystcik.isGameStarted = true;
        }
        public void EndGame()
        {
            joystcik.isGameStarted = false;
            joystcik.move.localPosition = InitialPos.transform.localPosition;
            timer.RestartGame = true;

        }
    }
}
