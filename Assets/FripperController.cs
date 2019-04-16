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

    //発展課題タップした指の区別のためfingerid用の変数を用意
    private int leftfingerID = 0;
    private int rightfingerID = 0;

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

            //マルチタッチを扱えるようにTouchクラスの配列のtouches変数を用意
            Touch[] touches = Input.touches;

            for (int i = 0; i < Input.touchCount; i++)
            {
            //タッチした時
            if (touches[i].phase == TouchPhase.Began)
            {
                if (touches[i].position.x < Screen.width / 2 && tag == "LeftFripperTag")
                {
                    this.leftfingerID = touches[i].fingerId;
                    SetAngle(this.flickAngle);
                }

                else if (touches[i].position.x >= Screen.width / 2 && tag == "RightFripperTag")
                {
                    this.rightfingerID = touches[i].fingerId;
                    SetAngle(this.flickAngle);
                }
            }

            //タッチした指が離された時
            else if (touches[i].phase == TouchPhase.Ended)
            {
                if (this.leftfingerID == touches[i].fingerId && tag == "LeftFripperTag")
                {
                    SetAngle(this.defaultAngle);
                }
                else if (this.rightfingerID == touches[i].fingerId && tag == "RightFripperTag")
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
