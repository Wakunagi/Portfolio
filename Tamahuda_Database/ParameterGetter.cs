using program;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterGetter : MonoBehaviour {


    [field: HeaderAttribute("Scripts")]
    [field: SerializeField] public so_CardList pr_CardList { private set; get; }
    [field: SerializeField] public DeckMakeUI pr_DeckMakeUI { private set; get; }
    [field: SerializeField] public CardText_UI pr_CardText_UI { private set; get; }
    [field: SerializeField] public UIButtonListMaker pr_UIButtonListMaker { private set; get; }
    [field: SerializeField] public HideLoadingUI pr_HideLoadingUI { private set; get; }



    [field: HeaderAttribute("Panels")]
    [field: SerializeField] public GameObject search_panel { private set; get; }
    [field: SerializeField] public GameObject deck_panel { private set; get; }
    [field: SerializeField] public GameObject mns_panel { private set; get; }
    [field: SerializeField] public GameObject mgc_panel { private set; get; }


    //--------------------シングルトン化--------------------

    private static ParameterGetter instance;
    public static ParameterGetter Instance {
        get {
            if (instance == null) {
                Type t = typeof(ParameterGetter);

                instance = (ParameterGetter)FindObjectOfType(t);
                if (instance == null) {
                    Debug.LogError(t + " をアタッチしているGameObjectはありません");
                }
            }

            return instance;
        }
    }

    void Awake() {
        if (instance == null) { instance = this; }
        else if (Instance == this) { }
        else Destroy(this);
    }

}