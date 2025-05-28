using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public enum SwipeDirection
    {
        None,  
        Left,
        Right,
        Up,
        Down
    }

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private Vector2 swipeDelta;

    private bool isSwiping = false;

    [SerializeField] private float swipeThreshold = 50f;
    public SwipeDirection currentSwipe = SwipeDirection.None;

    void Update()
    {
        DetectSwipe();
    }
    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isSwiping = true;
                    startTouchPosition = touch.position;
                    currentSwipe = SwipeDirection.None; // Đặt lại khi bắt đầu vuốt
                    break;

                case TouchPhase.Moved:
                    if (isSwiping)
                    {
                        endTouchPosition = touch.position;
                        swipeDelta = endTouchPosition - startTouchPosition;

                        if (swipeDelta.magnitude > swipeThreshold)
                        {
                            isSwiping = false;

                            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                            {
                                // Swipe theo hướng ngang
                                if (swipeDelta.x > 0)
                                {
                                    currentSwipe = SwipeDirection.Right;
                                    Debug.Log("Swipe phải");
                                }
                                else
                                {
                                    currentSwipe = SwipeDirection.Left;
                                    Debug.Log("Swipe trái");
                                }
                            }
                            else
                            {
                                // Swipe theo hướng dọc
                                if (swipeDelta.y > 0)
                                {
                                    currentSwipe = SwipeDirection.Up;
                                    Debug.Log("Swipe lên");
                                }
                                else
                                {
                                    currentSwipe = SwipeDirection.Down;
                                    Debug.Log("Swipe xuống");
                                }
                            }
                        }
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isSwiping = false;
                    break;
            }
        }
    }
    public SwipeDirection GetSwipeDirection()
    {
        return currentSwipe;
    }
}
