using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace program {
    public class HideButton : MonoBehaviour {
        public void OnClick_Hide(GameObject go) { go.SetActive(false); }
        public void OnClick_Show(GameObject go) { go.SetActive(true); }
    }
}