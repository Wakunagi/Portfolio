using UnityEngine;
using UnityEngine.UI;

namespace sikisiki.InGame.UI {
    public class DamageUI : MonoBehaviour {

        [SerializeField] private Image damage_image;
        float alfa = 0;

        public void ColorAlphaChanger() {
            alfa = 1;
        }

        void Update() {
            if (alfa < 0) return;
            alfa-=Time.deltaTime;

            damage_image.color = new Color(1, 1, 1, alfa);
        }
    }
}