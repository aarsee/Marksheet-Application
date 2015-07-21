package com.example.mobileapp;

import java.util.ArrayList;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteDatabase.CursorFactory;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

public class Databasecreate {
	
	
	
	String op1[][]=new String[25][7]; 
	public static final String opt1="opt1";
	public static final String opt2="opt2";
	public static final String opt3="opt3";
	public static final String opt4="opt4";
	public static final String opt5="opt5";
	public static final String opt6="opt6";
	public static final String opt7="opt7";
	
	private static final String DATABASE_NAME="testformula";
	private static final String DATABASE_TABLE="words";
	private static final int DATABASE_VERSION=2;
	
	private DbHelper ourHelper;
	private final Context ourContext;
	private SQLiteDatabase ourDatabase;
	private static class DbHelper extends SQLiteOpenHelper
	{

		public DbHelper(Context context) {
			super(context, DATABASE_NAME,null, DATABASE_VERSION);
			// TODO Auto-generated constructor stub
		}

		@Override
		public void onCreate(SQLiteDatabase db) {
			// TODO Auto-generated method stub
			db.execSQL("CREATE TABLE "+DATABASE_TABLE+"("+opt1+" VARCHAR (20),"+opt2+" VARCHAR (20),"+opt3+" VARCHAR (20),"+opt4+" VARCHAR (20),"+opt5+" VARCHAR (20),"+opt6+" VARCHAR (20),"+opt7+" VARCHAR(200));");
			Log.d("create database", "Yes yable is created");
		}

		@Override
		public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
			// TODO Auto-generated method stub
			db.execSQL("DROP TABLE IF EXISTS"+DATABASE_TABLE);
			onCreate(db);
		}
	}
	
	
	public Databasecreate(Context c)
	{
		ourContext=c;
	}
	
	public Databasecreate open() throws SQLException
	{
		ourHelper = new DbHelper(ourContext);
		ourDatabase = ourHelper.getWritableDatabase();
		return this;
	}
	public void close()
	{
		ourHelper.close();
	}
	public void createEntry()
	{
		op1[0][0]="CORPULENT";
		op1[0][1]="Lean";
		op1[0][2]="Gaunt";
		op1[0][3]="Emaciated";
		op1[0][4]="Obese";
		op1[0][5]="Obese";
		op1[0][6]=":Excessively fat";
		
		op1[1][0]="BRIEF";
		op1[1][1]="Limited";
		op1[1][2]="Small";
		op1[1][3]="Little";
		op1[1][4]="Short";
		op1[1][5]="Short";
		op1[1][6]=":Of short duration or distance, Concise and succinct";
		
		op1[2][0]="EMBEZZLE";
		op1[2][1]="Misappropriate";
		op1[2][2]="Balance";		
		op1[2][3]="Remunerate";
		op1[2][4]="Clear";
		op1[2][5]="Misappropriate";
		op1[2][6]=":Appropriate fraudulently to one's own use";
		
		op1[3][0]="VENT";
		op1[3][1]="Opening";
		op1[3][2]="Stodge";		
		op1[3][3]="End";
		op1[3][4]="Pasttenseofgo";
		op1[3][5]="Opening";
		op1[3][6]=":A holw for the escape of gas or air";
		
		op1[4][0]="AUGUST";
		op1[4][1]="Common";
		op1[4][2]="Ridiculous";		
		op1[4][3]="Dignified";
		op1[4][4]="Petty";
		op1[4][5]="Dignified";
		op1[4][6]=":of or befitting a lord";
		
		op1[5][0]="CANNY";
		op1[5][1]="Obstinate";
		op1[5][2]="Handsome";		
		op1[5][3]="Clever";
		op1[5][4]="Stout";
		op1[5][5]="Clever";
		op1[5][6]=":Showing self-interest and shrewsness in dealing with others";
		
		op1[6][0]="ALERT";
		op1[6][1]="Energetic";
		op1[6][2]="Observant";		
		op1[6][3]="Intelligent";
		op1[6][4]="Watchful";
		op1[6][5]="Watchful";
		op1[6][6]=":warn or arouse to a sense of danger or call to a state of preparedness";
		
		op1[7][0]="WARRIOR";
		op1[7][1]="Soldier";
		op1[7][2]="Sailor";		
		op1[7][3]="Pirate";
		op1[7][4]="Spy";
		op1[7][5]="Soldier";
		op1[7][6]=":someone engaged in or experienced in warfare";
		
		op1[8][0]="DISTANT";
		op1[8][1]="Far";
		op1[8][2]="Removed";		
		op1[8][3]="Reserved";
		op1[8][4]="Separate";
		op1[8][5]="Far";
		op1[8][6]=":The property created by the space between two objects or points";
		
		
		op1[9][0]="ADVERSITY";
		op1[9][1]="Failure";
		op1[9][2]="Helplessness";		
		op1[9][3]="Misfortune";
		op1[9][4]="Crisis";
		op1[9][5]="Misfortune";
		op1[9][6]=":A state of misfortune or affliction";
		
		
		ContentValues cv=new ContentValues();
		for(int i=0;i<10;i++)
		{
			cv.put(opt1,op1[i][0]);
			cv.put(opt2,op1[i][1]);
			cv.put(opt3,op1[i][2]);
			cv.put(opt4,op1[i][3]);
			cv.put(opt5,op1[i][4]);
			cv.put(opt6,op1[i][5]);
			cv.put(opt7,op1[i][6]);
			ourDatabase.insert(DATABASE_TABLE, null,cv);
		}
		
		
	}
	
	public ArrayList<String> getData()
	{
		String []columns=new String[]{opt1,opt2,opt3,opt4,opt5,opt6};
		Cursor c=ourDatabase.query(DATABASE_TABLE,columns, null,null,null, null,null);
		ArrayList<String> list = new ArrayList<String>();
		String result="";
		int iopt1=c.getColumnIndex(opt1);
		int iopt2=c.getColumnIndex(opt2);
		int iopt3=c.getColumnIndex(opt3);
		int iopt4=c.getColumnIndex(opt4);
		int iopt5=c.getColumnIndex(opt5);
		int iopt6=c.getColumnIndex(opt6);
		
		for(c.moveToFirst();!c.isAfterLast();c.moveToNext()){
			result=result+c.getString(iopt1)+" "+c.getString(iopt2)+" "+c.getString(iopt3)+" "+c.getString(iopt4)+" "+c.getString(iopt5)+" "+c.getString(iopt6)+"\n";
			list.add(result);
			result="";
		}
		return list;
	}
	
	public ArrayList<String> getWords()
	{
		String []columns=new String[]{opt1,opt7};
		Cursor c= ourDatabase.query(DATABASE_TABLE,columns,null,null,null,null,null);
		ArrayList<String> list = new ArrayList<String>();
		String result="";
		
		int iopt1=c.getColumnIndex(opt1);
		int iopt7=c.getColumnIndex(opt7);
		
		for(c.moveToFirst();!c.isAfterLast();c.moveToNext()){
			result=result+c.getString(iopt1)+" "+c.getString(iopt7);
			list.add(result);
			result="";
		}
		return list;
	}
}
