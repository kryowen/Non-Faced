using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {
    protected int atk; // 공격력
    protected float atk_d; // 공격딜레이
    protected int brk; // 방어파괴?
    protected int def; // 방어력
    protected int hp; // 체력
    protected int mov; // 이동속도
    protected int shield_stamina; // 실드 스테미나

    protected bool isDead; // 죽음?
    protected bool isMoving; // 움직이는 중?

    // Init
    public Status() {
        atk = 0;
        brk = crash();
        // 생성자에서 호출 한 뒤, 그 값을 그대로 사용
        def = 0;
        hp = 0;
        mov = 0;

        isDead = false;
        isMoving = false;
    }

    private void FixedUpdate() {
        if (hp < 1) {
            setDead(true);
        }

        if (Dead()) {
            Debug.Log(name + "hp: " +  hp + ", Destroyed");
            GameObject.Destroy(this);
        }
    }

    /* 결과 >> 1    결과 >> 3
     ATK = 20;      ATK = 75;
     DEF = 10;      DEF = 30;
     HP = 100;      HP = 1000;
     MOV = 3;       MOV = 5;
    */
    protected int crash() {
        return 1; //((2 * mov + (int)Mathf.Sqrt(atk)) % def) / 4;
    } // 객체들의 세부적인 변수를 수정하면 그때 수식을 작성?

    // get()
    public int getAtk() {
        return atk;
    }

    public float getAtk_d() {
        return atk_d;
    }

    public int getBrk() {
        return brk;
    }

    public int getDef() {
        return def;
    }

    public int getHp() {
        return hp;
    }

    public int getMov() {
        return mov;
    }

    public int getShld_sta() {
        return shield_stamina;
    }

    public bool Dead() { // 객체 사망?
        return isDead;
    }

    public bool Move() { // 객체 움직이는 상태?
        return isMoving;
    }

    // set()
    public void setAtk(int atk) {
        this.atk = atk;
    }

    public void setAtk_d(float atk_d) {
        this.atk_d = atk_d;
    }

    public void setDef(int def) {
        this.def = def;
    }

    public void setHp(int hp) {
        this.hp = hp;
    }

    public void setMov(int mov) {
        this.mov = mov;
    }

    public void setShield_stamina(int shield_stamina) {
        this.shield_stamina = shield_stamina;
    }

    public void setDead(bool isDead) {
        this.isDead = isDead;
    }

    public void setMove(bool isMoving) {
        this.isMoving = isMoving;
    }
}