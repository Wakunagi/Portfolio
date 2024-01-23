using UnityEngine;

public class HideLoadingUI : MonoBehaviour {

    [SerializeField] GameObject loading_panel;

    int count = 0;

    private void Awake() {
        loading_panel.SetActive(true);
    }

    public void SetCount() {
        count++;
    }

   public void SetEnd() {
        count--;

        if(count == 0 ) { loading_panel.SetActive(false); }
    }
}
