using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : Status {

    //Status status;

    //private void Awake() {
    //    status = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Status>();
    //}
    // 다른 스크립트에서 이러한 형식으로 컴포넌트 접근


    // 개성 추가
    int obesity;
    int moral;

    // 위치 정보 추가
    public Vector2 playerPos;

    // 감각
    public bool eyesLost;
    public bool earsLost;
    public bool mouthLost;

    // Init
    public playerScript() {
        this.name = "Player";
        atk = 0;
        atk_d = 0;
        def = 0;
        max_hp = 0;
        curr_hp = 0;
        mov = 0;
        shield_stamina = 0;

        eyesLost = false;
        earsLost = false;
        mouthLost = false;
        // 여기에 관한 처리들 석상과 대화에서 컴포넌트로 받아서 처리
    }

    private void Update() {
        playerPos = this.gameObject.transform.position;
    }
}