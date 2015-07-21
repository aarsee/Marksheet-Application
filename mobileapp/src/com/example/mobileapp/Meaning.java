package com.example.mobileapp;

import android.app.Activity;
import android.os.Bundle;
import android.widget.TextView;

public class Meaning extends Activity{
	TextView t1,t2;
	
	@Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.meaning);
        
        Bundle b=getIntent().getExtras();
        String a=b.getString("meaning");
    
        String c[]=a.split(":");
        
       t1=(TextView)findViewById(R.id.textView1);
       t2=(TextView)findViewById(R.id.textView2);
       
       t1.setText(c[0]);
       t2.setText(c[1]);
	}
}
