using UnityEngine;


namespace Autohand.Demo
{ 
    public class GameLoopManager : MonoBehaviour
    {
        public static GameLoopManager Instance { get; private set; }

        public JoystickObjectMover joystcik;
        public Timer timer;
        public GameObject InitialPos;
        public GameObject Hook;

        private void Update()
        {
            if (Hook.transform.position != InitialPos.transform.position) return;
            if(timer.bIsFinished) return;
            timer.bIsFinished = true;
        }

        public void StartMovement()
        {
            joystcik.isGameStarted = true;
        }
        public void EndGame()
        {
            joystcik.isGameStarted = false;
            Hook.transform.position = InitialPos.transform.position;
        }
    }
}
