using program.magic;
using program.monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace program {
    public class DeckMakeUI : MonoBehaviour {

        //各スクリプトを参照する場所
        ParameterGetter pr_ScriptsGetter;

        void Start() {
            //ScriptsGetterの初期化
            pr_ScriptsGetter = ParameterGetter.Instance;
        }

        //デッキ内の同じカードの最大数
        public const int CARD_COUNT_MAX=3;
        
        //デッキの最大数
        public const int DECK_MAX=30;

        //デッキリスト
        List<int> deck = new List<int>();

        //カード追加と削除
        public void AddCard(int id) { deck.Add(id); }
        public void RemoveCard(int id) { deck.Remove(id); }

        //デッキリストボタンの親
        [SerializeField] GameObject deck_btn_parent;

        //デッキ内容表示用のUI
        [SerializeField] GameObject deckList_panel;
        [SerializeField] Text deckList_text;
        [SerializeField] Text analyze_text;

        //デッキリストボタンのリスト
        List<GameObject> deck_btn_list = new List<GameObject>();

        //デッキリストからボタンを生成
        public void DisplayDeckList() {

            deck.Sort();

            //デッキリストボタンの初期化
            for(int i = 0; i < deck_btn_list.Count; i++) { Destroy(deck_btn_list[i]); }
            deck_btn_list.Clear();

            //デッキ内容の色の変更
            if (deck.Count > DECK_MAX) analyze_text.color = Color.red;
            else if (deck.Count < DECK_MAX) analyze_text.color = Color.blue;
            else analyze_text.color = Color.black;

            int mns_count = 0;
            int mgc_count = 0;

            foreach (int id in deck) {

                //IDがモンスターなら
                MonsterData monster = pr_ScriptsGetter.pr_CardList.MonsterInID(id);
                if (monster != null) {

                    //ボタンの生成とプログラムの取得
                    GameObject btn = Instantiate(pr_ScriptsGetter.pr_UIButtonListMaker.mns_btn_prefab);
                    MonsterButtonData mns_btn_data = null;
                    mns_btn_data = btn.GetComponent<MonsterButtonData>();
                    if (mns_btn_data == null) { Debug.LogError("Monster Button does not have MonsterButtonData!"); return; }

                    //リストに追加
                    deck_btn_list.Add(btn);

                    //親を設定
                    btn.transform.SetParent(deck_btn_parent.transform);

                    //MonsterButtonDataのセットアップ
                    mns_btn_data.SetUP(monster);
                    mns_btn_data.SetUP_DeckMode();

                    mns_count++;

                    continue;
                }
                
                //IDが魔法なら
                MagicData magic = pr_ScriptsGetter.pr_CardList.MagicInID(id);
                if (magic != null) {

                    //ボタンの生成とプログラムの取得
                    GameObject btn = Instantiate(pr_ScriptsGetter.pr_UIButtonListMaker.mgc_btn_prefab);
                    MagicButtonData mgc_btn_data = null;
                    mgc_btn_data = btn.GetComponent<MagicButtonData>();
                    if (mgc_btn_data == null) { Debug.LogError("Magic Button does not have MagicButtonData!"); return; }

                    //リストに追加
                    deck_btn_list.Add(btn);

                    //親を設定
                    btn.transform.SetParent(deck_btn_parent.transform);

                    //MagicButtonDataのセットアップ
                    mgc_btn_data.SetUP(magic);
                    mgc_btn_data.SetUP_DeckMode();

                    mgc_count++;

                    continue;
                }
            }

            analyze_text.text = "デッキ枚数: " + deck.Count + " 枚\r\nモンスター: " + mns_count + " 枚\r\n魔法: " + mgc_count + " 枚";


            //カード枚数の表示
            foreach (MonsterButtonData data in pr_ScriptsGetter.pr_UIButtonListMaker.mns_btn_list) {
                data.SetCountImage_InDeck();
            }
            foreach (MagicButtonData data in pr_ScriptsGetter.pr_UIButtonListMaker.mgc_btn_list) {
                data.SetCountImage_InDeck();
            }
        }

        //デッキ内容の表示
        public void OpenDeckListText() {

            deckList_text.text = string.Empty;
            int count = 0;

            //デッキ内容を表示
            foreach (int id in deck) {

                deckList_text.text += "[" + (count+1) + "]";
                if (count < 9) deckList_text.text += " ";
                deckList_text.text += ": " + pr_ScriptsGetter.pr_CardList.GetCardName(id) + "\n";

                count++;
            }

            //デッキ内容の色の変更
            if (deck.Count > DECK_MAX) deckList_text.color = Color.red;
            else if (deck.Count < DECK_MAX) deckList_text.color = Color.blue;
            else deckList_text.color = Color.black;

            deckList_panel.SetActive(true);
        }

        //デッキ内のカードの数
        public int GetCardCount(int id) {
            int count = 0;
            foreach(int _id in deck) {
                if (_id == id) { count++;}
            }
            return count;
        }

    }
}