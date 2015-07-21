package com.example.mobileapp;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Iterator;
import java.util.TimerTask;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;

public class Test1 extends Activity implements OnClickListener{
TextView t1,t2,sec,min;
Button next,pre,submit;
RadioGroup optiongrp;
RadioButton option1,option2,option3,option4,option5;
int k=0,j=0,correct=0,secVal=00,minVal=01;
String fetcheddata[][];
	@Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.strttest);
        
        Databasecreate fetchdata=new Databasecreate(Test1.this);
        
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
        submit=(Button)findViewById(R.id.button3);
        next=(Button)findViewById(R.id.button1);
        next.setOnClickListener(this);
        t1=(TextView)findViewById(R.id.textView1);
       t1.setText(fetcheddata[0][0].toString());
       option1=(RadioButton)findViewById(R.id.opt1);
       option1.setText(fetcheddata[0][1].toString());
       option2=(RadioButton)findViewById(R.id.opt2);
       option2.setText(fetcheddata[0][2].toString());
       option3=(RadioButton)findViewById(R.id.opt3);
       option3.setText(fetcheddata[0][3].toString());
       option4=(RadioButton)findViewById(R.id.opt4);
       option4.setText(fetcheddata[0][4].toString());
      sec=(TextView) findViewById(R.id.sec);
      min=(TextView) findViewById(R.id.min);
      minVal--;
      min.setText(minVal+"");
       pre=(Button)findViewById(R.id.button2);
       pre.setOnClickListener(this);
       submit.setOnClickListener(this);
       
       new Thread(new Runnable() {

           @Override
           public void run() {
               while (true) {
            	   secVal++;
            	 
            	   if(secVal==60)
            	   {
            		   secVal=0;
            		   if(minVal==0)
            		   		{
            			   minVal=0;
            			   secVal=60;
            			   }
            		   else
            		   {
            			   minVal--;
            		   }
            	   }
            	   
            	   if(minVal==0 && secVal==60)
            	   {
            		  
            		  Intent next=new Intent(Test1.this,Taketest.class);
            		  next.putExtra("Incorrect",10-correct);
  						next.putExtra("Correct",correct);
  						startActivity(next);
            		  
            	   }
                   updateTime(secVal,minVal);
                   try {
                       Thread.sleep(1000);
                   } catch (InterruptedException e) {
                       // TODO Auto-generated catch block
                       e.printStackTrace();
                   }
               }

           }
       }).start();
       	
}
	private void updateTime(final int secval,final int minval) {
        runOnUiThread(new Runnable() {

            @Override
            public void run() {
                sec.setText((60-secval)+"");
                min.setText(minval+"");
            }
        });
    }

	public void onClick(View v) {
		// TODO Auto-generated method stub
		switch(v.getId()){
		case R.id.button1:
			if(k<j){
				 int selectedId=optiongrp.getCheckedRadioButtonId();
				    option5=(RadioButton)findViewById(selectedId);
				   String temp1="";
				    if(option5==null){
				    	temp1=" ";
				    }
				    else
				    {
				    	temp1=option5.getText().toString().trim();
				    }
				    String temp2=fetcheddata[k][5].trim();
				    Log.d("temp1",temp1);
				    Log.d("temp2",temp2);
				   if(temp1.equals(temp2))
				   {
				    	correct=correct+1;
				    	Log.d("Correct :",correct+"");
				    	
				    }
				    //Log.d("Label field",option5.getText().toString()+"-- "+fetcheddata[k][5]);
				    optiongrp.clearCheck();
				
			if(k>0)
				pre.setEnabled(true);
			
			k++;
			
			t1.setText(fetcheddata[k][0].toString());
			option1.setText(fetcheddata[k][1].toString());
			option2.setText(fetcheddata[k][2].toString());
			option3.setText(fetcheddata[k][3].toString());
		    option4.setText(fetcheddata[k][4].toString());
			}
			else
			{
				
				next.setEnabled(false);
				pre.setEnabled(true);
			}
			
			break;
		case R.id.button2:
			optiongrp.clearCheck();
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
		    
		    int selectedId=optiongrp.getCheckedRadioButtonId();
		    option5=(RadioButton)findViewById(selectedId);
		 
			}
			else
			{
				pre.setEnabled(false);
				next.setEnabled(true);
			}
		    break;
		case R.id.button3:
			
			AlertDialog.Builder alert = new AlertDialog.Builder(this);
			alert.setTitle("TITLE HERE");
			alert.setMessage("Are you sure want to submit the Test ? ");

			alert.setPositiveButton("Ok", new DialogInterface.OnClickListener() {
			    public void onClick(DialogInterface dialog, int whichButton) {
			     //Do something here where "ok" clicked
			    	pre.setEnabled(false);
					next.setEnabled(false);
					Intent next=new Intent(Test1.this,Taketest.class);
					
					next.putExtra("Incorrect",(10-correct));
					next.putExtra("Correct",(correct));
					startActivity(next);
					//t2.setText("Correct: "+correct);
			    }
			});
			alert.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
			    public void onClick(DialogInterface dialog, int whichButton) {
			    //So sth here when "cancel" clicked.
			    	
			    }
			});
			alert.show();
			
			
			
		}
	}
	
	/*class Timer extends TimerTask {

		@Override
		public void run() {
			// TODO Auto-generated method stub
			
		}
		

		
	}*/
	
}
