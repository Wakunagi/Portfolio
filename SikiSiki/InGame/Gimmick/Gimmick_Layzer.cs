using UnityEngine;

namespace sikisiki.InGame.Gimmick {
    public class Gimmick_Layzer : GimmickParent {
        [SerializeField] private GameObject[] SkinChangeObjList, HideObjList;
        [SerializeField] private GameObject layzerBody;
        [SerializeField] private Gimmick_LayzerBody prLayzerBody;
        float dtime, maxSize, dpos;

        protected override void SabStart() {
            base.SabStart();

            SkinColorSetter(SkinChangeObjList);
            maxSize = layzerBody.transform.localScale.y;
        }
        protected override void SabUpdate() {
            base.SabUpdate();
            dtime = Time.deltaTime * 10;
        }

        protected override void TrueColor() {
            base.TrueColor();

            ObjectHideFanc(HideObjList);

            Vector2 pos = layzerBody.transform.localPosition;
            Vector2 size = layzerBody.transform.localScale;

            dpos = 0;
            pos.y = 0;
            size.y = 0;
            layzerBody.transform.localPosition = pos;
            layzerBody.transform.localScale = size;

        }
        protected override void FalseColor() {
            base.FalseColor();

            ObjectShowFanc(HideObjList);
            if (prLayzerBody.isInsideGameObj <= 0) {
                Vector2 pos = layzerBody.transform.localPosition;
                Vector2 size = layzerBody.transform.localScale;
                if (layzerBody.transform.localScale.y + dtime <= maxSize) {
                    dpos += dtime * 0.5f;
                    pos.y = dpos + 0.5f;
                    size.y += dtime;
                }
                else {
                    pos.y = maxSize * 0.5f + 0.5f;
                    size.y = maxSize;
                    dpos = 0;
                }
                layzerBody.transform.localPosition = pos;
                layzerBody.transform.localScale = size;
            }
        }


        //SkinColorSetter(GameObject[] objList) {}
        //ObjectHideFanc(GameObject[] objList) {}
        //ObjectShowFanc(GameObject[] objList) {}

    }
}