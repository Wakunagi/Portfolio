using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace program {
    public class GSSReader : MonoBehaviour {
        private string SHEET_ID = "1-k7N8CmIPGLa7cANLAr_nYx_E7nijkkEk7I_YCZximY";
        [SerializeField] private string SHEET_NAME = "";

        //各スクリプトを参照する場所
       protected ParameterGetter pr_ScriptsGetter;

        //https://docs.google.com/spreadsheets/d/1-k7N8CmIPGLa7cANLAr_nYx_E7nijkkEk7I_YCZximY

        void Start() {
            //ScriptsGetterの初期化
            pr_ScriptsGetter = ParameterGetter.Instance;

            pr_ScriptsGetter.pr_HideLoadingUI.SetCount();

            //画面サイズの変更
            Screen.SetResolution(540, 960, false);

            //スプレットシートの読み込み
            StartCoroutine(Method(SHEET_NAME));
        }

        IEnumerator Method(string _SHEET_NAME) {

            //スプレットシートを読み込む
            UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + SHEET_ID + "/gviz/tq?tqx=out:csv&sheet=" + _SHEET_NAME);
            
            //読み込めるまで待つ
            yield return request.SendWebRequest();

            //失敗
            if (request.isHttpError || request.isNetworkError) {
                Debug.Log(request.error);
            }

            //成功
            else {
                //読み込んだデータを加工
                CardSetting(request.downloadHandler.text + "\n");
                SetUp();

                pr_ScriptsGetter.pr_HideLoadingUI.SetEnd();
            }
        }

        protected virtual void SetUp() {

        }

        protected virtual void CardSetting(string data) {
            ResetData();

            string text = "";   //1行分のテキストデータ

            bool isReadStart = false;   //単語の読み込み中かどうか
            bool isFirstRead = true;    //最初の行かどうか

            for (int i = 0; i < data.Length; i++) {
                char c = data[i];

                //改行でかつ単語外（行の終わり）の場合
                if (c == '\n' && !isReadStart) {

                    //１行目なら何もしない
                    if (isFirstRead) { isFirstRead = false; }

                    //２行目以降はデータを挿入
                    else {
                        SetData(text);
                    }

                    //テキストをリセット
                    text = ""; continue;
                }

                //文字が"の場合
                if (c == '\"') {
                    //読み込み状態なら終了
                    //読んでいないなら読み始め
                    isReadStart = !isReadStart;

                    //"の代わりにタブに変更
                    if (!isReadStart) text += '\t';
                    continue;
                }

                //読み込み状態の場合
                if (isReadStart) text += c;


            }

        }

        protected virtual void ResetData() {

        }

        protected virtual void SetData(string text) {

        }

        protected string StringReader(string data, int num) {
            string myData = "";
            for (int i = 0; i < data.Length; i++) {
                char c = data[i];

                //num個目のタブが来るまではリセットする
                if (c == '\t') {
                    num--;
                    if (num == 0) break;

                    myData = "";
                    continue;
                }

                myData += c;
            }
            return myData;
        }
    }
}