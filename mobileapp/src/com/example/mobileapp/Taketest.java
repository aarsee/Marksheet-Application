package com.example.mobileapp;

import junit.framework.Test;
import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;

public class Taketest extends Activity implements OnClickListener{
	Button strt;
	TextView incans,cans;
	@Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.test);
        Bundle b=getIntent().getExtras();
        int ians=b.getInt("Incorrect");
        int ans=b.getInt("Correct");
        
        incans=(TextView)findViewById(R.id.intincorrectans);
        cans=(TextView)findViewById(R.id.intcorrectans);
        incans.setText(ians+"");
        cans.setText(ans+"");
        
        strt=(Button)findViewById(R.id.button1);
        strt.setOnClickListener(this);
}
	@Override
	public void onClick(View arg0) {
		// TODO Auto-generated method stub
		Intent i=new Intent(Taketest.this,Test1.class);
		startActivity(i);
		
	}
	
}
