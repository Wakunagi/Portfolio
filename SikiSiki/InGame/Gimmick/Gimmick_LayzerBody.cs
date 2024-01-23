using UnityEngine;

namespace sikisiki.InGame.Gimmick {
    public class Gimmick_LayzerBody : MonoBehaviour {

        float betweenPos, maxSize;
        GameObject ParentObj;
        public int isInsideGameObj { private set; get; } = 0;

        float inLayzerTime = 0;
        PlayerController playerController;

        void Start() {
            ParentObj = transform.parent.gameObject;
            maxSize = transform.localScale.y;
        }

        void Update() {
            //レイザーに当たっているときの処理
            if (playerController != null) {
                if (inLayzerTime <= 0) {
                    playerController.DamageFanc(1);
                    inLayzerTime = 2;
                }
                else inLayzerTime -= Time.deltaTime;
            }
            else if (inLayzerTime > 0) inLayzerTime = 0;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            isInsideGameObj++;
            SetTransform(collision);

            if(collision.GetComponent<PlayerController>() != null ) 
                playerController = collision.GetComponent<PlayerController>();
        }

        private void OnTriggerStay2D(Collider2D collision) {
            SetTransform(collision);
        }

        //物に当たった時にレーザーを小さくする
        void SetTransform(Collider2D collision) {
            betweenPos = collision.gameObject.transform.position.y - ParentObj.transform.position.y;
            if (betweenPos < maxSize) {
                Vector2 pos = transform.localPosition;
                pos.y = betweenPos + 0.5f;
                Vector2 size = transform.localScale;
                size.y = betweenPos * 0.5f;

                transform.localPosition = pos * 0.5f;
                transform.localScale = size;
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            isInsideGameObj--;
            if (collision.GetComponent<PlayerController>() != null)
                playerController = null;
        }
    }
}