using UnityEngine;

namespace sikisiki.InGame.Gimmick {
    public class Gimmick_Gate : GimmickParent {
        [SerializeField] private GameObject[] SkinChangeObjList;
        [SerializeField] private GameObject text;
        [SerializeField] private Vector3 ObjPos;
        private Vector3[] deltaPos = new Vector3[2];
        public bool isOpen { get; private set; } = false;
        [SerializeField] private bool isEndSet;
        public bool isEnd() { return isEndSet; }
        int IsPlayerInside = 0;

        protected override void SabStart() {
            base.SabStart();
            SkinColorSetter(SkinChangeObjList);
            ObjPos = transform.position;
            deltaPos[0] = ObjPos + new Vector3(0.4f, 0, 0);
            deltaPos[1] = ObjPos - new Vector3(0.4f, 0, 0);
        }
        protected override void SabUpdate() {
            base.SabUpdate();


            text.gameObject.SetActive(isOpen);

        }

        void Open() {
            isOpen = true;
            if (deltaPos[0] != SkinChangeObjList[0].transform.position) {
                SkinChangeObjList[0].transform.position = Vector3.MoveTowards(SkinChangeObjList[0].transform.position, deltaPos[0], Time.deltaTime * 4);
                SkinChangeObjList[1].transform.position = Vector3.MoveTowards(SkinChangeObjList[1].transform.position, deltaPos[1], Time.deltaTime * 4);
            }
        }
        void Close() {
            isOpen = false;
            if (ObjPos != SkinChangeObjList[0].transform.position) {
                SkinChangeObjList[0].transform.position = Vector3.MoveTowards(SkinChangeObjList[0].transform.position, ObjPos, Time.deltaTime * 4);
                SkinChangeObjList[1].transform.position = Vector3.MoveTowards(SkinChangeObjList[1].transform.position, ObjPos, Time.deltaTime * 4);
            }
        }

        protected override void TrueColor() {
            base.TrueColor();
            if (IsPlayerInside > 0) Open();
            else if (myIABB.isAffected) Open();
            else Close();
        }
        protected override void FalseColor() {
            base.FalseColor();
            Close();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.name == "player") IsPlayerInside++;
        }
        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.name == "player") IsPlayerInside--;
        }


        //SkinColorSetter(GameObject[] objList) {}
        //ObjectHideFanc(GameObject[] objList) {}
        //ObjectShowFanc(GameObject[] objList) {}

    }
}