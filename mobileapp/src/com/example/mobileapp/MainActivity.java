package com.example.mobileapp;

import android.os.AsyncTask;
import android.os.Bundle;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteDatabase.CursorFactory;
import android.database.sqlite.SQLiteOpenHelper;
import android.view.Menu;

public class MainActivity extends Activity {

	public static final String DATABASE_NAME="testformula";
	public static final String DATABASE_TABLE="words";
	private static final int DATABASE_VERSION=2;
	
	public static final String WORD="word";
	public static final String opt1="opt1";
	public static final String opt2="opt2";
	public static final String opt3="opt3";
	public static final String opt4="opt4";
	public static final String opt5="opt5";
	
	
	boolean temp=false;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        
        try{
        	Thread.sleep(2000);
        }
        catch(Exception e)
        {
        }
        
        SharedPreferences somedata=getSharedPreferences("mss",0);
		String datare=somedata.getString("email","false");
		if(datare.equals("false"))
		{
			temp=false;
		}
		else
		{
			temp=true;
		}
		if(temp==true)
        {
        Intent i=new Intent(MainActivity.this,Homepage.class);	
        startActivity(i);
        }
        else
        {	
        try{
        Databasecreate dbc=new Databasecreate(MainActivity.this);
        dbc.open();
        dbc.createEntry();
        dbc.close();
        }
        catch(Exception e){
        	
        }
        Intent i=new Intent(MainActivity.this,SharedPre.class);
        startActivity(i);
        } 
    }

        
}