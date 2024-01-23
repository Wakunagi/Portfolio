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

        public static int stageHP = -10;                //���X�e�[�W����HP
        public int hp { get; private set; } = 5;        //���݂�HP
        public int oldHp { get; private set; } = 5;     //����HP

        [SerializeField] private UIManager myUIpr;
        [SerializeField] private DamageUI myDamageUI;
        [SerializeField] private float speed;

        //----------�M�~�b�N�֌W----------

        //�S�[��
        int isInsideGate = 0;
        Gimmick_Gate gateProg;

        //�F���
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

        //�e�M�~�b�N���ɂ���Ƃ��̏���
        void InsideObjectFanc() {

            //�S�[��
            if (Input.GetKeyDown(KeyCode.Return)
                && isInsideGate > 0
                && gateProg != null
                && gateProg.isOpen) {

                //�X�e�[�W�̍Ōォ�H
                if (gateProg.isEnd()) {
                    myUIpr.IsEnd(true);
                    StageMaker.StageNum_wave = 0;
                    stageHP = -10;
                }

                //�X�e�[�W���܂�����
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
                x = backSpeed * speed * exSpeed;    //����Ɉړ�����

            Vector2 pos = (Vector2)transform.position + new Vector2(x, y);
            pos.y = (int)(pos.y * 100) / 100f;

            //�ړ������։摜�̔��]
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

            //�Q�[�g�̎��̏���
            if (collision.tag == "Gate") {
                isInsideGate++;
                if (collision.GetComponent<Gimmick_Gate>() != null)
                    gateProg = collision.GetComponent<Gimmick_Gate>();
            }

            //�J���[�X�y�[�X�ɓ��������̏���
            if (collision.tag == "ColorSpace") {
                isInsideColorSpace++;

                //�F��Ԃ�2�܂ŕۑ����Ă���
                Gimmick_ColorSpace gcs = null;
                gcs = collision.gameObject.GetComponent<Gimmick_ColorSpace>();
                if (gcs != null) {
                    if (colorSpaceProgs[0] != null) {
                        colorSpaceProgs[1] = colorSpaceProgs[0];
                        colorSpaceProgs[0] = gcs;
                    }
                    else colorSpaceProgs[0] = gcs;
                }

                //�����Ă���F�̃T�u�J���[���J���[�X�y�[�X�̐F�ɕύX
                if (colorSpaceProgs != null && cameraColorProg != null)
                    cameraColorProg.SetSabColorNum(colorSpaceProgs[0].myColor);
            }
        }
        private void OnTriggerExit2D(Collider2D collision) {

            //�Q�[�g�̎��̏���
            if (collision.tag == "Gate") isInsideGate--;

            //�J���[�X�y�[�X����o�����̏���
            if (collision.tag == "ColorSpace") {
                isInsideColorSpace--;

                //�T�u�J���[�����ɖ߂�
                if (isInsideColorSpace == 0) cameraColorProg.SetSabColorNum(0);

                else {
                    Gimmick_ColorSpace gcs = null;
                    gcs = collision.gameObject.GetComponent<Gimmick_ColorSpace>();
                    if (gcs != null 
                        && gcs == colorSpaceProgs[0] 
                        && colorSpaceProgs != null
                        && cameraColorProg != null) {

                        //�O�̃J���[�X�y�[�X������Ζ߂�
                        if (colorSpaceProgs[1] != null) cameraColorProg.SetSabColorNum(colorSpaceProgs[1].myColor);

                        //����ւ�
                        Gimmick_ColorSpace pocs = colorSpaceProgs[1];
                        colorSpaceProgs[1] = colorSpaceProgs[0];
                        colorSpaceProgs[0] = pocs;
                    }
                }
            }
        }
    }
}