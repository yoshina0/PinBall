using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour
{
    //HingeJointコンポーネントを入れる
    private HingeJoint myHingeJoint;

    //初期の傾き
    private float defaultAngle = 20;
    //弾いた時の傾き
    private float flickAngle = -20;

    // Start is called before the first frame update
    void Start()
    {
        //HingeJointコンポーネント取得
        this.myHingeJoint = GetComponent<HingeJoint>();

        //フリッパーの傾きを設定
        SetAngle(this.defaultAngle);
    }

    // Update is called once per frame
    void Update()
    {
        //左矢印キーを押した時左フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.LeftArrow) && tag == "LeftFripperTag") { SetAngle(this.flickAngle); }

        //右矢印キーを押した時右フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.RightArrow) && tag == "RightFripperTag") { SetAngle(this.flickAngle); }

        //矢印キーが離された時フリッパーをもとに戻す
        if (Input.GetKeyUp(KeyCode.LeftArrow) && tag == "LeftFripperTag") { SetAngle(this.defaultAngle); }

        if (Input.GetKeyUp(KeyCode.RightArrow) && tag == "RightFripperTag") { SetAngle(this.defaultAngle); }


        //ここから発展課題

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            //マルチタッチを扱えるように配列の変数を用意
            Touch[] touches = Input.touches;

            for (int i = 0; i < Input.touchCount; i++)
            {
                //タッチした時
                if (touch.phase == TouchPhase.Began)
                {
                    if (touches[i].position.x < Screen.width / 2 && tag == "LeftFripperTag") { SetAngle(this.flickAngle); }

                    if (touches[i].position.x >= Screen.width / 2 && tag == "RightFripperTag") { SetAngle(this.flickAngle); }
                }

                //タッチした指が離された時
                if (touch.phase == TouchPhase.Ended)
                {
                    SetAngle(this.defaultAngle);
                }
            }
        }
    }

    //フリッパーの傾きを設定
    public void SetAngle (float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }
}
