using UnityEngine;

namespace sikisiki.InGame.Gimmick {
    public class Gimmick_Weight : GimmickParent,ISpeedChanger {
        [SerializeField] private GameObject[] SkinChangeObjList;
        [SerializeField] private Rigidbody2D myRigid;
        private BoxCollider2D myCollider;
        public float backSpeed = 0;
        [SerializeField] private float bsChenger = 1;
        Vector3 firstPos;

        protected override void SabStart() {
            base.SabStart();

            SkinColorSetter(SkinChangeObjList);
            firstPos = transform.position;

            myRigid = gameObject.GetComponent<Rigidbody2D>();
            myCollider = gameObject.GetComponent<BoxCollider2D>();
        }
        protected override void SabUpdate() {
            base.SabUpdate();
        }

        private void FixedUpdate() {
            Vector3 pos = transform.position;
            pos.x += backSpeed * bsChenger * Time.deltaTime;
            myRigid.MovePosition(pos);

        }


        public void BackSpeedChange(float speed) {
            backSpeed = speed;
        }

        protected override void TrueColor() {
            base.TrueColor();
            if (!myRigid.isKinematic) myRigid.isKinematic = true;
            if (myRigid.simulated) myRigid.simulated = false;
            myRigid.constraints = RigidbodyConstraints2D.FreezePosition;
            if (!myCollider.isTrigger) myCollider.isTrigger = true;
        }
        protected override void FalseColor() {
            base.FalseColor();
            if (myRigid.isKinematic) myRigid.isKinematic = false;
            if (!myRigid.simulated) myRigid.simulated = true;
            myRigid.constraints = RigidbodyConstraints2D.None;
            myRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (myCollider.isTrigger) myCollider.isTrigger = false;
        }


        //SkinColorSetter(GameObject[] objList) {}
        //ObjectHideFanc(GameObject[] objList) {}
        //ObjectShowFanc(GameObject[] objList) {}

    }
}