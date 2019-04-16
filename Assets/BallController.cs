using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    //ボールが見える可能性のあるz軸の最大値
    private float visiblePosZ = -6.5f;

    //ゲームオーバーを表示するテキスト
    private GameObject gameoverText;

    //スコアを表示するテキスト
    private GameObject ScoreText;

    //スコア用の変数（初期値0）
    private int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        //シーン中のGameOverTextオブジェクトを取得
        this.gameoverText = GameObject.Find("GameOverText");

        //シーン中のScoreTextオブジェクトを取得
        this.ScoreText = GameObject.Find("ScoreText");

        //ScoreTextにスコア初期値を表示
        this.ScoreText.GetComponent<Text>().text = "SCORE :" + score;

    }

    // Update is called once per frame
    void Update()
    {
        //ボールが画面外に出た場合
        if(this.transform.position.z < this.visiblePosZ)
        {
            //GameOverTextにゲームオーバーを表示
            this.gameoverText.GetComponent<Text>().text = "Game Over";
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        //タグの情報を取得しcollisionに代入
        string TagName = collision.gameObject.tag;

        //タグに応じて点数加算
        if(TagName == "SmallStarTag")
        {
            AddScore(10);
        }
        else if(TagName == "LargeStarTag")
        {
            AddScore(100);
        }
        else if(TagName == "SmallCloudTag")
        {
            AddScore(50);
        }
        else if (TagName == "LargeCloudTag")
        {
            AddScore(150);
        }
    }

    //引数ぶんスコア加算する
    void AddScore(int point)
    {
        this.score += point;
        this.ScoreText.GetComponent<Text>().text = "SCORE :" + score;
    }

}
