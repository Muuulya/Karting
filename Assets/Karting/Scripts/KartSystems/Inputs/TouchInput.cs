using UnityEngine;
using UnityEngine.Serialization;

namespace KartGame.KartSystems
{
    public class TouchInput : BaseInput
    {
        public float ThrowMagnitute = 40f;
        private float leftBoard;
        private float rightBoard;
        
        public override InputData GenerateInput()
        {
            int touchCount = Input.touchCount;
            float turnInput = 0;

            if (touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                
                if (touch.phase is TouchPhase.Began)
                {
                    SaveNewBoards(touch);
                }

                turnInput = GetTurnInput(touch);

                float yDelta = touch.deltaPosition.y;
                if (yDelta >= ThrowMagnitute)
                {
                    Debug.Log("Бросок вперед");
                }
                else if (yDelta <= -ThrowMagnitute)
                {
                    Debug.Log("Бросок назад");
                }
            }

            return new InputData()
            {
                Accelerate = touchCount > 0,
                Brake = false,
                TurnInput = turnInput
            };
        }

        private float GetTurnInput(Touch touch)
        {
            float turnInput;
            Vector2 currentPosition = touch.position;
            float offset = Mathf.InverseLerp(leftBoard, rightBoard, currentPosition.x);
            turnInput = Mathf.Lerp(-1, 1, offset);
            return turnInput;
        }

        private void SaveNewBoards(Touch touch)
        {
            float halfScreenWidth = Screen.width / 2;
            leftBoard = touch.position.x - halfScreenWidth;
            rightBoard = touch.position.x + halfScreenWidth;
        }
    }
}