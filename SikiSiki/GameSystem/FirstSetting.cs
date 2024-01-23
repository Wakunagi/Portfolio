using UnityEngine;

namespace sikisiki.GmaneSystem {
    public class FirstSetting : MonoBehaviour {

        // Start is called before the first frame update
        void Start() {
            Screen.SetResolution(1920, 1080, true);
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Application.Quit();
            }
        }
    }
}