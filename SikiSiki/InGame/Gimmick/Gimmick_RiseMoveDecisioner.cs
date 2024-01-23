using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sikisiki.InGame.Gimmick {
    public class Gimmick_RiseMoveDecisioner : MonoBehaviour {

        public bool canBack { private set; get; } = true;

        [SerializeField] int isInsideRigid = 0;

        private void OnTriggerEnter2D(Collider2D collision) {
            isInsideRigid++;
            canBack = false;
        }

        private void OnTriggerExit2D(Collider2D collision) {
            isInsideRigid--;
            if (isInsideRigid <= 0) canBack = true;
        }
    }
}