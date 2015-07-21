package com.example.mobileapp;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;

public class SharedPre extends Activity implements OnClickListener{
		
	
	EditText email,username,password;
	SharedPreferences somedata;
	public static String filename="mss";
	@Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.registration);
        setupVariables();
        somedata=getSharedPreferences(filename,0);
	}
	private void setupVariables()
	{
		Button submit=(Button) findViewById(R.id.button1);
		email=(EditText) findViewById(R.id.editText1);
		username=(EditText)findViewById(R.id.editText3);
		password=(EditText)findViewById(R.id.editText2);
		submit.setOnClickListener(this);
		
	}
	
	
	
	@Override
	public void onClick(View arg0) {
		// TODO Auto-generated method stub
		String a=email.getText().toString();
		String b=username.getText().toString();
		String c=password.getText().toString();
		SharedPreferences.Editor editor=somedata.edit();
		editor.putString("email",a);
		
		editor.putString("uname",b);
	
		editor.putString("pass",c);
		editor.commit();
		
		Intent i=new Intent(SharedPre.this,Homepage.class);
		startActivity(i);
	}
	
	

}
