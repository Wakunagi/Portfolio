using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sikisiki.InGame.Gimmick {
    public class Gimmick_Move : GimmickParent {
        [SerializeField] private GameObject[] SkinChangeObjList, HideObjList;
        [SerializeField] private BoxCollider2D myCollider;
        [SerializeField] private List<GameObject> moveObjList = null;
        [SerializeField] private float dirSpeed = -1, waitTime;

        protected override void SabStart() {
            base.SabStart();

            SkinColorSetter(SkinChangeObjList);
        }
        protected override void SabUpdate() {
            base.SabUpdate();
            if (myCollider != null) myCollider.enabled = !isColorTrue;

            if (moveObjList.Count > 0) {
                foreach (GameObject obj in moveObjList) {
                    StartCoroutine(SpeedChange(obj, waitTime, 10, false));

                }
            }
        }

        protected override void TrueColor() {
            base.TrueColor();

            ObjectHideFanc(HideObjList);

        }
        protected override void FalseColor() {
            base.FalseColor();

            ObjectShowFanc(HideObjList);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (moveObjList != null) {
                foreach (GameObject obj in moveObjList) {
                    if (obj == collision.gameObject) return;
                }
            }
            moveObjList.Add(collision.gameObject);
        }
        private void OnTriggerExit2D(Collider2D collision) {
            if (moveObjList != null) {
                int i = 0;
                foreach (GameObject obj in moveObjList) {
                    if (obj == collision.gameObject) break;
                    i++;
                }
                moveObjList.RemoveAt(i);
            }
            StartCoroutine(SpeedChange(collision.gameObject, waitTime / 4, 0, true));
        }

        IEnumerator SpeedChange(GameObject obj, float wTime, float bs, bool isOut) {

            //ÉvÉåÉCÉÑÅ[ÇÃèÍçáÇæÇØè≠Çµë“Ç¬
            if (obj.tag == "Player") {
                for (float t = 0; t < wTime; t += Time.deltaTime) {
                    yield return null;
                    if (!isMoveFanc(obj) && !isOut) yield break;
                }
            }
            if (!isMoveFanc(obj) && !isOut) yield break;
            if (obj.GetComponent<ISpeedChanger>() != null) obj.GetComponent<ISpeedChanger>().BackSpeedChange(bs * dirSpeed);
        }

        bool isMoveFanc(GameObject moveObj) {
            bool hasObj = false;
            if (moveObjList != null) {
                foreach (GameObject obj in moveObjList) {
                    if (obj == moveObj) hasObj = true;
                }
            }
            return hasObj;
        }


        //SkinColorSetter(GameObject[] objList) {}
        //ObjectHideFanc(GameObject[] objList) {}
        //ObjectShowFanc(GameObject[] objList) {}

    }
}