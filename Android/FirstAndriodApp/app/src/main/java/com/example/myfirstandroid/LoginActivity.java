package com.example.myfirstandroid;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;

public class LoginActivity extends AppCompatActivity {
    public static final String GLOBAL_USER = "";
    public static final String GLOBAL_PASS = "";
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
    }

    public void Login(View view)
    {

        Intent intent = new Intent(this, DisplayMessageActivity.class);
        EditText emailaddress = (EditText) findViewById(R.id.emailaddress);
        EditText password = (EditText) findViewById(R.id.EnterPassword) ;


        String user = emailaddress.getText().toString();
        String pass = password.getText().toString();
        intent.putExtra(GLOBAL_USER, user);
        intent.putExtra(GLOBAL_PASS, pass);
        startActivity(intent);
    }

    public void Cancel(View view)
    {
        Intent intent = new Intent(this, DisplayMessageActivity.class);
        String user = "";
        String pass = "";
        intent.putExtra(GLOBAL_USER, user);
        intent.putExtra(GLOBAL_PASS, pass);
        startActivity(intent);
    }
}
