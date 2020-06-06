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

/*
 * It would be better to use other already created camera movement plugins
 * Like Cinemachine made by Unity itself
 * But For the sake of Learning Everything Would Be done by Hand
 */

namespace Game_1
{
    public class CameraMovement : MonoBehaviour
    {
        /*
         * Always try to give the private variables (which have [SerializeField] in front)  some initial value,
         * because if not the Console pop ups the warning message about:
         * "warning CS0649: Field (smth) never assigned to, and will always have its default value null".
         * if you dont want to do give values and instated want to simply disable this warning
         * you can do it by typing:
         * #pragma warning disable CS0649
         * But I wouldn't recommend this, warnings are for a reason.
         */

        /*
         * prefer using SerializeField over public,
         * because if I don't want the variable to be visible to other
         * It better not be accessible by other
         * and SerializeField still gives the opportunity to be visible and accessible for inspector
         */

        [SerializeField] private Transform target = null;
        [SerializeField] private Vector3 positionOfCamera = Vector3.zero;
        [SerializeField] private float smoothSpeed = 0.1f;
        [SerializeField] private float minDistanceBetweenCameraAndTarget = 2f,
            pushDistance = 3f;//If the minDistanceBetweenCameraAndTarget is breached how far to push the camera back

        private Quaternion cameraRotation;

        private float horizontalRotation = 0.5f, verticalRotation = 0.5f;

        // Awake is called before Start() and it occurs only ones in the script instance life
        private void Awake()
        {
            transform.position = target.position - positionOfCamera;

            //Cursor Lock down Disappear mouse curse during gameplay
            Cursor.lockState = CursorLockMode.Locked;
        }

        /*
         * If You dont need the unity define methods like Awake, Start, Update, etc;
         * It would be better if you delete theme, because it may not be anything substantial for memory
         * but they are still called (mostly talking about Update here) every frame and it still takes
         * a minuscule chuck of memory (of course if the script is attached to the active gameObject)
         */

        // // Start is called before the first frame update
        // private void Start()
        // {
        // }

        private void Update()
        {
            Rotation();
        }

        // LateUpdate is called once per frame, but only after every other update was done
        private void LateUpdate()
        {
            Moving();
        }

        private void Rotation()
        {
            /*
             * Arc of the circle is calculated by
             * Arc = degree * π / 180 * r
             * In our case
             * degree = Rotation of the target
             * π = Mathf.PI
             * r = distance between target and the camera (positionOfCamera)
             */

            /*
             * In The beginning i want to make the care move using the arc of the circle
             * It should have been like with the cameraRotation of the target the camera would move up the
             * calculated arc trajectory, however because of my insufficient knowledge
             * I couldn't do it.
             * If Anyone can do it Please upload it would be appreciated.
             */

            /*
             * One way of doing that was by giving the target gamObject a child
             * with transform where I wanted for the camera to be.
             * Of course it was working because (if it isn't coded with other logic) by default the child object
             * works as the part of the parent (like a limb). So if parent goes somewhere it goes with it
             * But the values of cameraRotation and distance relative to the parent object is always maintained
             * (again it is default only, with code you can change it)
             * The same principle applies to the cameraRotation.
             * Child object cameraRotation always equal to local cameraRotation with parent
             * and if parent rotates child goes with it
             * (in this case child changes not only cameraRotation but the position, too, relative to World space not localSpace)
             * and if you want to do that kind of thing in the Moving() method make distance = phantomCamera.position
             * (phantomCamera being the reference to the child gameObject transform). 
             */

            /*
             * In the end I simply used the mouse input as the cameraRotation tool for the camera
             * and if user wants to see the with cameraRotation it would already be users problem
             * how to make the correct angle of view :D
             */

            verticalRotation += Input.GetAxisRaw("Mouse X");
            horizontalRotation += Input.GetAxisRaw("Mouse Y");
            cameraRotation = Quaternion.Euler(horizontalRotation, verticalRotation, 0f).normalized;
        }

        private void Moving()
        {
            /*
             * Vector3.Lerp is moving one object toward another (positionNow, desiredPosition, time)
             * time in this case is from 0 to 1.
             * 0 means that it is more close to the positionNow
             * 1 means that it is more close to the desiredPosition
             * 0.5f means it is in between
             * all of this means that higher the number the faster the movement
             */

            /*
             * The '?' does same job as the if else statement with one condition in each
             * The below code may be written like this too:
             * 
             * Vector3 cameraPosition;
             * if (Input.GetKey(KeyCode.F))
             * {
             *     cameraPosition = Quaternion.Euler(target.eulerAngles) * positionOfCamera;
             * }
             * else
             * {
             *     cameraPosition = cameraRotation * positionOfCamera;
             * }
             * 
             * But with '?" it is more cleaner.
             * If you want to know that '?' does check:
             * Null-conditional operators documentation
             */

            /*
             * Fun fact: During writing this piece of code
             * I learned that you can't multiply Vector3 on Quaternion
             * positionOfCamera * Quaternion.Euler(target.eulerAngles) -> gave me Error
             * But You can multiply Quaternion on Vector3
             * Quaternion.Euler(target.eulerAngles) * positionOfCamera -> As you see below No Error :D
             */

            var cameraPosition = Input.GetKey(KeyCode.F)
                ? Quaternion.Euler(target.eulerAngles) * positionOfCamera
                : cameraRotation * positionOfCamera;

            var distance = target.position - cameraPosition;

            var newPosition = Vector3.Lerp(transform.position, distance,
                smoothSpeed * Time.deltaTime);

            /*
             * Vector3.Distance(a, b) it returns the distance (float) between
             * vector3 a and vector3 b
             */

            if (Vector3.Distance(newPosition, target.position) <= minDistanceBetweenCameraAndTarget)
            {
                newPosition += -transform.forward * (pushDistance * Time.deltaTime);
            }

            transform.position = newPosition;
            transform.LookAt(target);
        }

        #region Not Used Stuff

        // private const float ForArc = Mathf.PI / 180;

        // [SerializeField] private Transform phantomCamera;

        #endregion
    }
}