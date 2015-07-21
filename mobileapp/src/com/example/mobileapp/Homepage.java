package com.example.mobileapp;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;

public class Homepage extends Activity implements OnClickListener
{

	Button taketest,solutions,cwu,vocabtips;
	TextView wod;
	@Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.homepage);
        vocabtips=(Button)findViewById(R.id.button1);
        taketest=(Button)findViewById(R.id.button2);
        solutions=(Button)findViewById(R.id.button3);
        vocabtips.setOnClickListener(this);
        cwu=(Button)findViewById(R.id.button4);
        taketest.setOnClickListener(this);
        solutions.setOnClickListener(this);
        wod=(TextView)findViewById(R.id.textView5);
        wod.setOnClickListener(this);
}
	@Override
	public void onClick(View arg0) {
		// TODO Auto-generated method stub
		switch(arg0.getId())
		{
		case R.id.button2:
			
			Intent i=new Intent(Homepage.this,Taketest.class);
			i.putExtra("Correct","00");
			i.putExtra("Incorrect","00");
		startActivity(i);
		break;
		case R.id.button3:
			Intent j=new Intent(Homepage.this,Solutions.class);
			startActivity(j);
			break;
		case R.id.button4:
			Intent k=new Intent(Homepage.this,Coffeewithus.class);
			startActivity(k);
			break;
			
		case R.id.button1:
			Intent i1=new Intent(Homepage.this,Wordslist.class);
			startActivity(i1);
			break;
			
		case R.id.textView5:
			Intent i2=new Intent(Homepage.this,Wordofday.class);
			startActivity(i2);
			break;
		}
		}
}
