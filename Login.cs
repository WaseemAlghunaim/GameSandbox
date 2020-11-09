using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginMenu : MonoBehaviour
{
    private var formNick = ""; //this is the field where the player will put the name to login

    function Start()
    {

        PlayerPrefs.DeleteAll();
    }
   
    function Login(id : int)
    {

        if (DoLogin)
        {

            GUILayout.BeginVertical();

            GUI.skin = guiSkin;

            GUILayout.Box(/* new Rect (Screen.width/2 - 250, Screen.height/2 - 150, 500, 400),*/"Login");

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Username:"); //text with your nick
            formNick = GUILayout.TextField(formNick, 15, GUILayout.Width(345), GUILayout.Height(35));
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Password:");
            formPassword = GUILayout.TextField(formPassword, 15, GUILayout.Width(345), GUILayout.Height(35)); //same as above, but for password
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(5);
            if (GUILayout.Button("Login"))
            { //just a button
                Action("Login");
            }
            if (GUILayout.Button("Sign Up"))
            { //just a button
                DoLogin = false;

            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

        }
        else
        {

            GUI.skin = guiSkin;

            GUILayout.Box("Register");

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Username:");
            RformNick = GUILayout.TextField(RformNick, 15, GUILayout.Width(300), GUILayout.Height(35));

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Password:");
            RformPassword = GUILayout.TextField(RformPassword, 15, GUILayout.Width(300), GUILayout.Height(35));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Password:");

            TRformPassword = GUILayout.TextField(TRformPassword, 15, GUILayout.Width(300), GUILayout.Height(35));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Email:");
            Remail = GUILayout.TextField(Remail, 75, GUILayout.Width(300), GUILayout.Height(35));
            GUILayout.EndHorizontal();


            if (GUILayout.Button("Finish"))
            { //just a button
                Action("Register");
            }
            if (GUILayout.Button("Back"))
            { //just a button
                DoLogin = true;

            }



        }


        //GUI.TextArea( textrect, formText );
    }



    function Action(Act : String)
    {


        var form = new WWWForm(); //here you create a new form connection
                      //add your hash code to the field myform_hash, check that this variable name is the same as in PHP file
        var tempURL : String;
        if (Act == "Login")
        {
            tempURL = URL + "?User=" + formNick + "&Pass=" + formPassword + "&Act=" + Act;
        }
        else
        {
            tempURL = URL + "?User=" + RformNick + "&Pass=" + RformPassword + "&Act=" + Act + "&Email=" + Remail;
        }
        var w = WWW(tempURL); //here we create a var called 'w' and we sync with our URL and the form
        yield w; //we wait for the form to check the PHP file, so our game dont just hang
        if (w.error != null)
        {
            //print(w.error); //if there is an error, tell us
        }
        else
        {
            if (w.data == "    Correct")
            {
                print("Logging In...");
                LoadOut = true;
                LoadOutText = "Signing In...";
                yield WaitForSeconds(5);
                PlayerPrefs.SetString("RegUser", formNick);

                Application.LoadLevel(1);
            }
            if (w.data == "    Wrong")
            {
                LoadOut = true;
                LoadOutText = "Wrong Password";
                yield WaitForSeconds(3);
                LoadOut = false;
            }
            if (w.data == "    No User")
            {
                LoadOut = true;
                LoadOutText = "No Registered User Found";
                yield WaitForSeconds(3);
                LoadOut = false;
            }
            if (w.data == "    ILLEGAL REQUEST")
            {
                LoadOut = true;
                LoadOutText = "Server Error";
                yield WaitForSeconds(3);
                LoadOut = false;
            }
            if (w.data == "    Registered")
            {
                print("Account Created. Logging In.");
                LoadOut = true;
                LoadOutText = "Creating Account  Logging In...";
                PlayerPrefs.SetString("RegUser", RformNick);
                yield WaitForSeconds(5);
                Application.LoadLevel(1);

            }
            if (w.data == "    ERROR")
            {
                LoadOut = true;
                LoadOutText = "Login Error. Restarting.";
                yield WaitForSeconds(3);
                Application.LoadLevel(0);
            }
            print(w.data);

            // formText = w.data; //here we return the data our PHP told us
            w.Dispose(); //clear our form in game
        }

        formNick = ""; //just clean our variables


    }
}
