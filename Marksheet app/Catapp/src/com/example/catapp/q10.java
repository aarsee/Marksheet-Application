package com.example.catapp;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.RadioGroup;
import android.widget.RadioGroup.OnCheckedChangeListener;

public class q10 extends Activity{
Button q10;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.q10);
		q10 = (Button)findViewById(R.id.q10);
q10.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				setContentView(R.layout.q11);
				
			}
		});
	}

}
