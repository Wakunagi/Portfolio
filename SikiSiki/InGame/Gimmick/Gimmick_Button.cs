using System.Collections.Generic;
using UnityEngine;

namespace sikisiki.InGame.Gimmick {
    public class Gimmick_Button : MonoBehaviour {
        [SerializeField] private GameObject[] objcts, skin;
        List<IsAffectedByButton> iABB = new List<IsAffectedByButton>();
        [SerializeField] private int isInsideObjcts = 0;
        bool swichB = false;

        void Start() {
            int i = 0;
            foreach (GameObject obj in objcts) {
                if (obj.GetComponent<IsAffectedByButton>() != null) iABB.Add(obj.GetComponent<IsAffectedByButton>());
                i++;
            }
        }

        void Update() {
            if (isInsideObjcts > 0 && swichB) isInsideFanc(true);
            if (isInsideObjcts <= 0 && !swichB) isInsideFanc(false);
        }


        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Weight") {
                isInsideObjcts++;
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Weight") {
                isInsideObjcts--;
            }
        }

        void isInsideFanc(bool b) {
            skin[0].gameObject.SetActive(b);
            skin[1].gameObject.SetActive(!b);
            int i = 0;
            foreach (GameObject obj in objcts) {
                iABB[i].isAffected = b;
                i++;
            }
            swichB = !swichB;
        }
    }
}