using UnityEngine;

namespace sikisiki.GmaneSystem {
    public class DisplayButtonUI : MonoBehaviour {
        [SerializeField] private GameObject[] displayObjects, hideObjects;

        public void OnClick_Display() {
            if (displayObjects.Length > 0) {
                foreach (GameObject go in displayObjects) {
                    go.SetActive(true);
                }
            }
        }

        public void OnClick_Hide() {
            if (hideObjects.Length > 0) {
                foreach (GameObject go in hideObjects) {
                    go.SetActive(false);
                }
            }
        }
    }
}