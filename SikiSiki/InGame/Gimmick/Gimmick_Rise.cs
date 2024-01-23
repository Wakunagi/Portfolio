using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace sikisiki.InGame.Gimmick {
    public class Gimmick_Rise : GimmickParent {
        [SerializeField] private GameObject[] SkinChangeObjList, charaObjList;
        [SerializeField]private GameObject rise_obj;
        [SerializeField] private Vector3 deltaPos;
        public bool isMoving { get; private set; }
        public bool isRise { get; private set; }
        [SerializeField] private float riseSpeed = 2;
        Vector3 firstPos;

        [SerializeField] private Gimmick_RiseMoveDecisioner moveDecisioner;
        PlayerController playerController;

        protected override void SabStart() {
            base.SabStart();

            SkinColorSetter(SkinChangeObjList);

            if (charaObjList.Length != 0) {
                foreach (GameObject obj in charaObjList) {

                    if (deltaPos.x != 0) obj.transform.eulerAngles = new Vector3(0, 0, -90 * (deltaPos.x / Mathf.Abs(deltaPos.x)));
                    else if (deltaPos.y < 0) obj.transform.eulerAngles = new Vector3(0, 0, 180);
                }
            }


            firstPos = rise_obj.transform.position;
            deltaPos += firstPos;


        }
        protected override void SabUpdate() {
            base.SabUpdate();
        }

        protected override void TrueColor() {
            base.TrueColor();
            MovingFanc(deltaPos);
        }
        protected override void FalseColor() {
            base.FalseColor();
            MovingFanc(firstPos);
        }

        void MovingFanc(Vector3 target) {
            if (rise_obj.transform.position != target) {
                if (!isMoving && playerController != null) PlayerHitMe();
                if (!moveDecisioner.canBack && target == firstPos) return;
                rise_obj.transform.position = Vector3.MoveTowards(rise_obj.transform.position, target, Time.deltaTime * riseSpeed);
                isMoving = true;
            }
            else {
                if (isMoving) {
                    if (playerController != null) {
                        playerController.gameObject.transform.SetParent(null);
                        playerController.SetExSpeed(1);
                    }
                }
                isMoving = false; isRise = false;
            }
        }

        void PlayerHitMe() {
            playerController.SetExSpeed(0);
            playerController.gameObject.transform.position = (rise_obj.transform.position + new Vector3(0, 1f, 0));
            playerController.gameObject.transform.SetParent(rise_obj.transform);
        }


        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.GetComponent<PlayerController>() != null) {
                playerController = collision.GetComponent<PlayerController>();
                if (isMoving) PlayerHitMe();
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.GetComponent<PlayerController>() != null) {
                playerController = null;
            }
        }

        //SkinColorSetter(GameObject[] objList) {}
        //ObjectHideFanc(GameObject[] objList) {}
        //ObjectShowFanc(GameObject[] objList) {}

    }
}