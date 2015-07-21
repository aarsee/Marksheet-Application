package com.example.mobileapp;

import java.util.ArrayList;
import java.util.Iterator;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;

import android.view.View.OnClickListener;
import android.content.Intent;
import android.graphics.Rect;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;

public class Solutions extends Activity implements OnClickListener{
	String fetcheddata[][];
	TextView t1,t2;
	Button next,pre;
	RadioGroup optiongrp;
	RadioButton option1,option2,option3,option4,option5;
	int k=0,j=0,correct=0,secVal=00,minVal=01;
	
	@Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.solutions);

Databasecreate fetchdata=new Databasecreate(Solutions.this);
        
        fetchdata.open();
        ArrayList<String> list = new ArrayList<String>();
        list=fetchdata.getData();
        fetchdata.close();
        int size=list.size();
        Log.d("Size of list",size+"");
        
        
         fetcheddata=new String[size][7];
        
        for (Iterator<String> it = list.iterator(); it.hasNext();) {
                 String a[]=it.next().split(" ");
                 fetcheddata[j][0]=a[0];
                 fetcheddata[j][1]=a[1];
                 fetcheddata[j][2]=a[2];
                 fetcheddata[j][3]=a[3];
                 fetcheddata[j][4]=a[4];
                 fetcheddata[j][5]=a[5];
                 
 j++;
        }
        t2=(TextView)findViewById(R.id.textView2);
        optiongrp=(RadioGroup) findViewById(R.id.option);
        //optiongrp.getChildAt(0);
        next=(Button)findViewById(R.id.button1);
        next.setOnClickListener(this);
        t1=(TextView)findViewById(R.id.textView1);
       t1.setText(fetcheddata[0][0].toString());
       
       option1=(RadioButton)findViewById(R.id.opt1);
       option1.setText(fetcheddata[0][1].toString());
      option1.setEnabled(false);
       
       option2=(RadioButton)findViewById(R.id.opt2);
       option2.setText(fetcheddata[0][2].toString());
      option2.setEnabled(false);
      
       option3=(RadioButton)findViewById(R.id.opt3);
       option3.setText(fetcheddata[0][3].toString());
      option3.setEnabled(false);
       
       option4=(RadioButton)findViewById(R.id.opt4);
       option4.setText(fetcheddata[0][4].toString());
       
       option4.setChecked(true);
     
       pre=(Button)findViewById(R.id.button2);
       pre.setOnClickListener(this);
       pre.setEnabled(false);
	}
	@SuppressLint("NewApi")
	public void onClick(View v) {
		// TODO Auto-generated method stub
		switch(v.getId()){
		case R.id.button1:
			if(k<j-1){
				k++;
				t1.setText(fetcheddata[k][0].toString());
				option1.setText(fetcheddata[k][1].toString());
				
				option2.setText(fetcheddata[k][2].toString());
				
				option3.setText(fetcheddata[k][3].toString());
				 
			    option4.setText(fetcheddata[k][4].toString());
			    
		
				
				  String temp2=fetcheddata[k][5].trim();
				 	
				for(int it=0;it<4;it++)
				{
				RadioButton b1=(RadioButton) optiongrp.getChildAt(it);
				String temp3=b1.getText().toString().trim();
				if(temp2.equals(temp3))
				{
					RadioButton v1=(RadioButton)optiongrp.getChildAt(it);
					v1.setChecked(true);
					v1.setEnabled(true);
					Log.d("check",temp3);
				}
				else
				{
					RadioButton v1=(RadioButton)optiongrp.getChildAt(it);
					v1.setEnabled(false);
				}
				
				}
				
			if(k>0)
				pre.setEnabled(true);
			
			}
			else
			{
				
				next.setEnabled(false);
				pre.setEnabled(true);
			}
			
			break;
		case R.id.button2:
			
			if(k>0)
			{
				if(k<j)
					next.setEnabled(true);
			k--;
			t1.setText(fetcheddata[k][0].toString());
			option1.setText(fetcheddata[k][1].toString());
			option2.setText(fetcheddata[k][2].toString());
			option3.setText(fetcheddata[k][3].toString());
		    option4.setText(fetcheddata[k][4].toString());
		   
		    String temp2=fetcheddata[k][5].trim();
		    for(int it=0;it<4;it++)
			{
			RadioButton b1=(RadioButton) optiongrp.getChildAt(it);
			String temp3=b1.getText().toString().trim();
			if(temp2.equals(temp3))
			{
				RadioButton v1=(RadioButton)optiongrp.getChildAt(it);
				v1.setChecked(true);
				v1.setEnabled(true);
				Log.d("check",temp3);
			}
			else
			{
				RadioButton v1=(RadioButton)optiongrp.getChildAt(it);
				v1.setEnabled(false);
			}
			
			}
			
		    
		 
			}
			else
			{
				pre.setEnabled(false);
				next.setEnabled(true);
			}
		    break;
			
			
		}
	}
	
	


}
