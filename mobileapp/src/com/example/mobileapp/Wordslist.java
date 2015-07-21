package com.example.mobileapp;

import java.util.ArrayList;

import java.util.Iterator;

import android.util.Log;
import android.view.View;
import android.widget.AdapterView.OnItemClickListener;
import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;

public class Wordslist extends Activity {
	  ArrayList<String> list1;
	ArrayAdapter<String> dataAdapter = null;
	EditText words;
	
	int j=0;
	@Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.vocab);


        
       words=(EditText) findViewById(R.id.editText1);
       
      list1=new ArrayList<String>();
       ArrayList<String> list2=new ArrayList<String>();
       Databasecreate fetchdata=new Databasecreate(Wordslist.this);
       fetchdata.open();
       list1=fetchdata.getWords();
       fetchdata.close();
      
       Log.d("Successful","");
       for (Iterator<String> it = list1.iterator(); it.hasNext();) {
    	  
           String a[]=it.next().split(":");
          
           list2.add(a[0]);
           j++;
       }
       
       
       
       
       dataAdapter = new ArrayAdapter<String>(this,
    		    R.layout.word_list, list2);
       
       ListView listview=(ListView)findViewById(R.id.listView1);
       listview.setAdapter(dataAdapter);
       listview.setTextFilterEnabled(true);
       
       words.addTextChangedListener(new TextWatcher(){
    	  @Override
    	   public void afterTextChanged(Editable s){
    		   
    	   }
    	   
		@Override
		public void beforeTextChanged(CharSequence s, int start, int count,
				int after) {
			// TODO Auto-generated method stub
			
		}
		@Override
		public void onTextChanged(CharSequence s, int start, int before,
				int count) {
			
			dataAdapter.getFilter().filter(s.toString());
			
			// TODO Auto-generated method stub
			
		}
    	   
       });
       
      listview.setOnItemClickListener(new OnItemClickListener() {
		@Override
		public void onItemClick(AdapterView<?> parent, View view, int position,
				long id) {
			// TODO Auto-generated method stub
		
		Intent i=new Intent(Wordslist.this,Meaning.class);
		String item=((TextView)view).getText().toString();
		String com="";
		for (Iterator<String> it = list1.iterator(); it.hasNext();)
		{
			String c[]=it.next().split(":");
			if(item.equals(c[0]))
			{
				com=c[0]+":"+c[1];
				Log.d("Comval",com);
				break;
			}
		}
		i.putExtra("meaning",com);
		startActivity(i);
		Log.d("Position",item+" ");
			
		}
    	  });
       
	}
}

