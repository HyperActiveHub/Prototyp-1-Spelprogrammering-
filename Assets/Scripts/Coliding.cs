using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Coliding : MonoBehaviour
{
    public static Coliding instance;
    Text extraText;
    public int textSize = 17;
    Vector2 Pos = new Vector2(00.0f, 00.0f);

    
    public int Y;
    public float Timer = 0;
    public bool Collide = false;
    // Start is called before the first frame update



    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
       
        extraText = GetComponent<Text>();
        if(extraText == null)
        {
            Debug.LogError("lol tis is wrong");
        }
        Timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Collide == true)
        {
            Timer += Time.deltaTime;
            textSize = 30;
            Pos = new Vector2(180, -150);
            extraText.color = Color.red;
            extraText.fontStyle = FontStyle.Bold;

        }

        if (Collide == false)
        {
            Timer = Mathf.Round(Timer);
            textSize = 17;
            Pos = new Vector2(-102, -125);
            extraText.color = Color.black;
            extraText.fontStyle = FontStyle.Normal;


        }
        extraText.GetComponent<RectTransform>().anchoredPosition = Pos;
        extraText.fontSize = textSize;
        extraText.text = "Extra Time: " + Mathf.Round(Timer) + "s";
    }
    /// <summary>
    /// kollar om man är på en platform
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {

        
        Collide = true;
    }
    /// <summary>
    /// Avrundar Tiden
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        //avrundar tiden
        Collide = false;


    }
    //void OnGUI()
    //{
    //    //prints text
    //    GUIStyle style = new GUIStyle();
    //    Font myFont = (Font)Resources.Load("COMIC", typeof(Font));
    //    style.font = myFont;
    //    style.fontSize = 25;
        
    //    GUI.Label(new Rect(70, 120, 150, 50), "Extra Time: " + Mathf.Round(Timer) + "s", style);
    //}
}
