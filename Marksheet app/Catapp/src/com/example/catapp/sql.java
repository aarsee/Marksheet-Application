package com.example.catapp;

import android.content.ContentValues;
import android.content.Context;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

public class sql {
public static final String ROW_ID = "_id";
public static final String ROW_NAME = "_name";
public static final String ROW_AGE = "_age";
public static final String ROW_GENDER = "_gender";
public static final String ROW_EMAIL = "_email";
public static final String ROW_PHONE = "_phone";
public static final String ROW_PASS = "_pass";
public static final String ROW_CPASS = "_cpass";

public static final String DATABASE_NAME = "Profile";
public static final String DATABASE_TABLE = "Table";
public static final int DATABASE_VERSION = 1;

private DbHelper ourHelper;
private final Context context;
private SQLiteDatabase ourData;

public static class DbHelper extends SQLiteOpenHelper
{
	public DbHelper(Context context) 
	{
		super(context, DATABASE_NAME, null, DATABASE_VERSION);
		// TODO Auto-generated constructor stub
	}
	@Override
	public void onCreate(SQLiteDatabase db) {
		db.execSQL("CREATE TABLE" + DATABASE_TABLE + " (" +
	ROW_ID + " INTEGER PRIMARY KEY AUTOINCREMENT," + 
	ROW_NAME + " TEXT NOT NULL," + 
	ROW_AGE + " TEXT NOT NULL," + 
	ROW_GENDER + " TEXT NOT NULL," + 
	ROW_EMAIL + " TEXT NOT NULL," + 
	ROW_PHONE + " TEXT NOT NULL," + 
	ROW_PASS + " TEXT NOT NULL," + 
	ROW_CPASS + " TEXT NOT NULL);" );
		
	}
	@Override
	public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
		db.execSQL("DROP TABLE IF EXISTS" + DATABASE_TABLE);
		onCreate(db);
		
	}
}
public sql(Context c)
{
	context = c;
}
public sql open() throws SQLException
{
	ourHelper = new DbHelper(context);
	ourData = ourHelper.getWritableDatabase();
	return this;
}
public void close()
{
	ourHelper.close();
}
public long createEntry(String name, String age, String gender, String email,String phone, String pass, String cpass) {
	ContentValues cv = new ContentValues();
	cv.put(ROW_NAME, name);
	cv.put(ROW_AGE, age);
	cv.put(ROW_GENDER, gender);
	cv.put(ROW_PHONE, phone);
	cv.put(ROW_EMAIL, email);
	cv.put(ROW_PASS, pass);
	cv.put(ROW_CPASS, cpass);
	return ourData.insert(DATABASE_TABLE, null, cv);
	
}
}
