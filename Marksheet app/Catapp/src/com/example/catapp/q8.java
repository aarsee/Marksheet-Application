package com.example.catapp;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.RadioGroup;
import android.widget.RadioGroup.OnCheckedChangeListener;

public class q8 extends Activity{
Button q8;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.q8);
		q8 = (Button)findViewById(R.id.q8);
q8.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				setContentView(R.layout.q9);
				
			}
		});
	}
}
