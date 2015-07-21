package com.example.catapp;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.RadioGroup;
import android.widget.RadioGroup.OnCheckedChangeListener;

public class q5 extends Activity{
Button q5;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.q5);
		q5 = (Button)findViewById(R.id.q5);
q5.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				setContentView(R.layout.q6);
				
			}
		});
	}
}
