using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeckRegister : MonoBehaviour {
    [SerializeField] private string accessKey;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            StartCoroutine(GetData());
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            string deck = "";
            for(int i = 0; i < 30; i++) { if (i > 0) deck += "_"; deck += "100001"; }
            StartCoroutine(PostData("aaa", "bbb", deck));
        }
    }

    private IEnumerator GetData() {
        Debug.Log("�f�[�^��M�J�n�E�E�E");
        var request = UnityWebRequest.Get("https://script.google.com/macros/s/" + accessKey + "/exec");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success) {
            if (request.responseCode == 200) {
                var records = JsonUtility.FromJson<DeckList>(request.downloadHandler.text).decks;
                Debug.Log("�f�[�^��M�����I");
                foreach (var record in records) {
                    Debug.Log("User�F" + record.user_id + "_Pass�F" + record.pass + "_Deck�F" + record.deck);
                }
            }
            else {
                Debug.LogError("�f�[�^��M���s�F" + request.responseCode);
            }
        }
        else {
            Debug.LogError("�f�[�^��M���s" + request.result);
        }
    }

    private IEnumerator PostData(string user, string pass, string deck) {
        Debug.Log("�f�[�^���M�J�n�E�E�E");
        var form = new WWWForm();
        form.AddField("user", user);
        form.AddField("pass", pass);
        form.AddField("deck", deck);

        var request = UnityWebRequest.Post("https://script.google.com/macros/s/" + accessKey + "/exec", form);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success) {
            if (request.responseCode == 200) {
                var records = JsonUtility.FromJson<DeckList>(request.downloadHandler.text).decks;
                Debug.Log("�f�[�^���M�����I");
                foreach (var record in records) {
                    Debug.Log("User�F" + record.user_id + "_Pass�F" + record.pass + "_Deck�F" + record.deck);
                }
            }
            else {
                Debug.LogError("�f�[�^���M���s" + request.responseCode);
            }
        }
        else {
            Debug.Log("�f�[�^���M���s" + request.result);
        }
    }
}
