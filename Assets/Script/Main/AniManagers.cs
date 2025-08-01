using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metaverse
{
    public enum targetDirectType
    {
        forward = 0,
        backward = 1
    }
    public enum AniType
    {
        idle = 0,
        walk = 1,
        run = 2,
        win = 3,
        lose = 4
    }
    public class AniManagers : MonoBehaviour
    {
        public float speedWalk = 2.5f;
        public float speedRun = 7f;
        public float moveXstep = 1;
        public float moveYstep = 0.7f;
        public LayerMask wallLayer;

        targetDirectType targetType;
        public Animator[] targetAnimators; //0- forward 1- backward
        bool isRun = false;
        bool isPose = false;

        void Start()
        {
            Restart();
        }
        void Restart()
        {
            SetAni(AniType.idle);
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                targetType = targetDirectType.forward;
                targetAnimators[(int)targetType].gameObject.SetActive(false);
                isPose = false;
                isRun = false;
                this.gameObject.transform.localPosition = new Vector3(11, -8, 0);
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);

            }
        }

        private void Update()
        {
            Restart();
            Move();
            Pose();
        }

        private void Move()
        {
            Vector3 moveVelocity = Vector3.zero;
            float speed = 0;
            bool isMove = false;

            if (!isPose)
                SetAni(AniType.idle);

            if (Input.GetKey(KeyCode.LeftControl)) //walk
            {
                isRun = false;
            }
            if (Input.GetKey(KeyCode.LeftShift)) //run
            {
                isRun = true;
            }

            if (Input.GetKey(KeyCode.A)) //SW //INIT Direction
            {
                if (isPose)  //pose animation reset.
                    targetAnimators[(int)targetType].gameObject.SetActive(false);
                moveVelocity = new Vector3(-moveXstep, -moveYstep, 0);
                targetType = targetDirectType.forward;
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
                isPose = false;
                isMove = true;
            }
            if (Input.GetKey(KeyCode.S)) //SE
            {
                if (isPose)  //pose animation reset.
                    targetAnimators[(int)targetType].gameObject.SetActive(false);
                moveVelocity = new Vector3(moveXstep, -moveYstep, 0);
                this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
                targetType = targetDirectType.forward;
                isPose = false;
                isMove = true;
            }
            if (Input.GetKey(KeyCode.D)) //NE
            {
                if (isPose)  //pose animation reset.
                    targetAnimators[(int)targetType].gameObject.SetActive(false);
                moveVelocity = new Vector3(moveXstep, moveYstep, 0);
                this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
                targetType = targetDirectType.backward;
                isPose = false;
                isMove = true;
            }
            if (Input.GetKey(KeyCode.W)) //NW
            {
                if (isPose)  //pose animation reset.
                    targetAnimators[(int)targetType].gameObject.SetActive(false);
                moveVelocity = new Vector3(-moveXstep, moveYstep, 0);
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
                targetType = targetDirectType.backward;
                isPose = false;
                isMove = true;
            }
            if (isMove)
            {
                if (isRun)
                {
                    speed = speedRun;
                    SetAni(AniType.run);
                }
                else
                {
                    speed = speedWalk;
                    SetAni(AniType.walk);
                }
            }

            Vector3 newPosition = transform.position + moveVelocity * speed * Time.deltaTime;
            transform.position = newPosition;
        }

        void Pose()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                isPose = true;
                targetType = targetDirectType.forward;
                SetAni(AniType.win);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                isPose = true;
                targetType = targetDirectType.forward;
                SetAni(AniType.lose);
            }
        }

        void SetAni(AniType aType)
        {
            //change tatget direction

            if (!targetAnimators[(int)targetType].isActiveAndEnabled)
            {
                targetAnimators[(int)targetType].gameObject.SetActive(true);
                if (targetType == targetDirectType.forward)
                    targetAnimators[(int)targetDirectType.backward].gameObject.SetActive(false);
                else
                    targetAnimators[(int)targetDirectType.forward].gameObject.SetActive(false);
            }
            targetAnimators[(int)targetType].SetInteger("aniInt", (int)aType);
        }

    
    }

    
}
