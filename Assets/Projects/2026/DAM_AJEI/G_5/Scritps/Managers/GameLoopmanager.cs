using UnityEngine;


namespace Autohand.Demo
{ 
    public class GameLoopManager : MonoBehaviour
    {
        public static GameLoopManager instance { get; private set; }

        public JoystickObjectMover joystcik;
        public bool isGameStarted = false;
        public TimerGame timer;
        public GameObject InitialPos;
        private bool isMoving = false;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

        }

        private void Update()
        {
            if (!isGameStarted) return;

            if (joystcik.IsMoving && !isMoving)
            {
                isMoving = true;
                SoundManager.instance.PlayMove();
            }
            else if (!joystcik.IsMoving && isMoving && HookDown.instance.hookState == HookDown.HookState.None)
            {
                isMoving = false;
                SoundManager.instance.StopMove();
                Debug.Log("Stop Move");
            }
        }

        public void StartMovement()
        {
            isGameStarted = true;
            joystcik.isGameStarted = isGameStarted;
        }
        public void EndGame()
        {
            isGameStarted = false;
            joystcik.isGameStarted = isGameStarted;
            joystcik.move.localPosition = InitialPos.transform.localPosition;
            timer.RestartGame = true;

        }
        public void ResetMoving()
        {
            isMoving = false;
        }
    }
}
