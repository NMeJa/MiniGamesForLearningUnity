using System.Timers;
using UnityEngine;

/*
 * Hello, The reason there are so many Comments is because As Already mentioned it is for learning purposes,
 * So I am leaving the comment where I believe some people may not know what it means.
 * EveryOne is welcome To add or edit new comments
 * If the Comment Starts With "TODO"
 * it means someone wanted to do something differently or simply couldn't do it
 * So if you see it please try to do your best to understand what the author meant
 * and try to challenge and do that "thing"
 * In addition, if you managed to do the "TODO" thing 
 * dont delete the comment but simply wrote in front "done" and point to the line from there the answer starts 
 * Small Explanation would be appreciated.
 * In addition, if you find a bug try to fix it or mention it :D.
 */

namespace Game_1
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movingSpeed = 50f,
            rotationSpeed = 50f;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            PlayerMoving();
            PlayerRotation();
        }

        private void PlayerMoving()
        {
            /*
             * Input.GetAxis is the "old" input system used by unity
             * There is new input system but this one won't be gone
             * anytime soon
             */
            
            /*
             * The way it works is by returning increasing float number from 0 to 1
             * GetAxis gets name of the Input Which is made in the
             * Project Settings -> Input Manager
             */
            
            /*
             * In Addition:
             * There Is Input.GetAxisRaw() which instead of the increasing number from 0 to 1
             * it returns 0 or 1 depending on the condition
             */
            
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            /*
             * Instead of calling transform component several times its better if it is called once
             * and using that local variable doing the logic given that
             * it still points to the transform which we want
             */

            var localTransform = transform;

            /*
             * Mathf.Abs -> return the Absolute of the value
             * It means that if the number is 5 it returns 5
             * And if it is (-5) it still returns 5
             */

            if (Mathf.Abs(horizontal) >= 0.1f)
                localTransform.position += localTransform.right * (horizontal * movingSpeed * Time.deltaTime);

            if (Mathf.Abs(vertical) >= 0.1f)
                localTransform.position += localTransform.forward * (vertical * movingSpeed * Time.deltaTime);
        }

        private void PlayerRotation()
        {
            var horizontal = Input.GetAxis("HorizontalRotation");
            var vertical = Input.GetAxis("VerticalRotation");

            var localTransform = transform;

            if (Mathf.Abs(horizontal) >= 0.1f)
                localTransform.Rotate(Vector3.up * (horizontal * rotationSpeed * Time.deltaTime));

            if (Mathf.Abs(vertical) >= 0.1f)
                localTransform.Rotate(Vector3.right * (vertical * rotationSpeed * Time.deltaTime));
        }
    }
}