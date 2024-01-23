using UnityEngine;
using UnityEngine.SceneManagement;
using sikisiki.GmaneSystem;
using sikisiki.InGame.Gimmick;
using sikisiki.InGame.UI;
using UnityEngine.UI;

namespace sikisiki.InGame {
    public class PlayerController : MonoBehaviour, ISpeedChanger {

        Rigidbody2D myRigid;
        SpriteRenderer myRenderer;
        float dtime, exSpeed = 1;
        public float backSpeed = 0;

        public static int stageHP = -10;                //同ステージ内のHP
        public int hp { get; private set; } = 5;        //現在のHP
        public int oldHp { get; private set; } = 5;     //初期HP

        [SerializeField] private UIManager myUIpr;
        [SerializeField] private DamageUI myDamageUI;
        [SerializeField] private float speed;

        //----------ギミック関係----------

        //ゴール
        int isInsideGate = 0;
        Gimmick_Gate gateProg;

        //色空間
        int isInsideColorSpace = 0;
        [SerializeField] private Gimmick_ColorSpace[] colorSpaceProgs;
        [SerializeField] private CameraColor cameraColorProg;

        //------------------------------


        void Start() {
            if (GetComponent<Rigidbody2D>() != null) myRigid = GetComponent<Rigidbody2D>();
            if (GetComponent<SpriteRenderer>() != null) myRenderer = GetComponent<SpriteRenderer>();
            if (Camera.main.GetComponent<CameraColor>() != null) cameraColorProg = Camera.main.GetComponent<CameraColor>();
            colorSpaceProgs = new Gimmick_ColorSpace[2];
        }

        void OnEnable() {
            Debug.Log("HPisFirstLoad:" + ScenChanger.isFirstLoad);

            if (!ScenChanger.isFirstLoad) hp = stageHP;
            else {
                hp = oldHp;
                stageHP = hp;
            }
        }

        void Update() {
            dtime = Time.deltaTime;
            InsideObjectFanc();
        }

        void FixedUpdate() {
            MoveFanc();
        }

        public void BackSpeedChange(float speed) {
            backSpeed = speed;
        }

        public void SetExSpeed(float s) {
            exSpeed = s;
        }

        //各ギミック内にいるときの処理
        void InsideObjectFanc() {

            //ゴール
            if (Input.GetKeyDown(KeyCode.Return)
                && isInsideGate > 0
                && gateProg != null
                && gateProg.isOpen) {

                //ステージの最後か？
                if (gateProg.isEnd()) {
                    myUIpr.IsEnd(true);
                    StageMaker.StageNum_wave = 0;
                    stageHP = -10;
                }

                //ステージがまだ続く
                else {
                    StageMaker.StageNum_wave++;
                    stageHP = hp;
                    SceneManager.LoadScene("sc_main");
                }

            }
        }

        void MoveFanc() {

            float x, y = 0;

            if (backSpeed == 0)
                x = Input.GetAxis("Horizontal") * speed * exSpeed;
            else
                x = backSpeed * speed * exSpeed;    //勝手に移動する

            Vector2 pos = (Vector2)transform.position + new Vector2(x, y);
            pos.y = (int)(pos.y * 100) / 100f;

            //移動方向へ画像の反転
            if (x < 0) myRenderer.flipX = true;
            if (x > 0) myRenderer.flipX = false;

            if (exSpeed != 0) myRigid.MovePosition(pos);
        }

        public void DamageFanc(int damage) {
            myDamageUI.ColorAlphaChanger();
            hp -= damage;
            stageHP = hp;
            if (hp <= 0) {
                myUIpr.IsEnd(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {

            //ゲートの時の処理
            if (collision.tag == "Gate") {
                isInsideGate++;
                if (collision.GetComponent<Gimmick_Gate>() != null)
                    gateProg = collision.GetComponent<Gimmick_Gate>();
            }

            //カラースペースに入った時の処理
            if (collision.tag == "ColorSpace") {
                isInsideColorSpace++;

                //色空間を2つまで保存しておく
                Gimmick_ColorSpace gcs = null;
                gcs = collision.gameObject.GetComponent<Gimmick_ColorSpace>();
                if (gcs != null) {
                    if (colorSpaceProgs[0] != null) {
                        colorSpaceProgs[1] = colorSpaceProgs[0];
                        colorSpaceProgs[0] = gcs;
                    }
                    else colorSpaceProgs[0] = gcs;
                }

                //見えている色のサブカラーをカラースペースの色に変更
                if (colorSpaceProgs != null && cameraColorProg != null)
                    cameraColorProg.SetSabColorNum(colorSpaceProgs[0].myColor);
            }
        }
        private void OnTriggerExit2D(Collider2D collision) {

            //ゲートの時の処理
            if (collision.tag == "Gate") isInsideGate--;

            //カラースペースから出た時の処理
            if (collision.tag == "ColorSpace") {
                isInsideColorSpace--;

                //サブカラーを元に戻す
                if (isInsideColorSpace == 0) cameraColorProg.SetSabColorNum(0);

                else {
                    Gimmick_ColorSpace gcs = null;
                    gcs = collision.gameObject.GetComponent<Gimmick_ColorSpace>();
                    if (gcs != null 
                        && gcs == colorSpaceProgs[0] 
                        && colorSpaceProgs != null
                        && cameraColorProg != null) {

                        //前のカラースペースがあれば戻す
                        if (colorSpaceProgs[1] != null) cameraColorProg.SetSabColorNum(colorSpaceProgs[1].myColor);

                        //入れ替え
                        Gimmick_ColorSpace pocs = colorSpaceProgs[1];
                        colorSpaceProgs[1] = colorSpaceProgs[0];
                        colorSpaceProgs[0] = pocs;
                    }
                }
            }
        }
    }
}