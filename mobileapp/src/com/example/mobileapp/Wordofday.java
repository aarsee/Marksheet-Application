package com.example.mobileapp;

import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;
import java.util.HashMap;

import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserFactory;

import android.app.Activity;
import android.app.ListActivity;
import android.app.ProgressDialog;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;

public class Wordofday extends Activity {
	TextView t1,t2;
private HandleXML obj;
	 private ProgressDialog pDialog;
	 private String title = "title";
	 private String description = "description";
	 private String link = "link";
	 SimpleAdapter sd ;
	 ListView lv1;
	private XmlPullParserFactory xmlFactoryObject;
	 XmlPullParser myParser;
	// ArrayList<HashMap<String, String>> mylist = new ArrayList<HashMap<String, String>>();
	private String finalUrl="http://wordsmith.org/awad/rss1.xml";
	
	
	@Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.wordofday);
        new HandleXML().execute();
 
}
	
	class HandleXML extends AsyncTask<String, String, String>
	{
		@Override
        protected void onPreExecute() {

            super.onPreExecute();
            pDialog = new ProgressDialog(Wordofday.this);
            pDialog.setMessage("loading.");
            pDialog.setIndeterminate(false);
            pDialog.setCancelable(true);
            pDialog.show();
        }

		@Override
		protected String doInBackground(String... params) {
			// TODO Auto-generated method stub
			t1=(TextView)findViewById(R.id.textView1);
	          t2=(TextView)findViewById(R.id.textView2);
			 try{
	             URL url = new URL(finalUrl);
	             HttpURLConnection conn = (HttpURLConnection) url.openConnection();
	             conn.setReadTimeout(10000 );
	             conn.setConnectTimeout(15000);
	             conn.setRequestMethod("GET");
	             conn.setDoInput(true);
	             // Starts the query
	             conn.connect();
	             InputStream stream = conn.getInputStream();
	             xmlFactoryObject = XmlPullParserFactory.newInstance();
	             myParser = xmlFactoryObject.newPullParser();
	             myParser.setFeature(XmlPullParser.FEATURE_PROCESS_NAMESPACES, false);
	             myParser.setInput(stream, null);

	             
	          } catch (Exception e) {
	          }
			 int event;
	         // String text=null;
	          //HashMap<String, String> map;
	          boolean insideItem = false;
	          try {
	             event = myParser.getEventType();
	            while (event != XmlPullParser.END_DOCUMENT) {
	             String name=myParser.getName();
	            // map= new HashMap<String, String>();
	             if (event == XmlPullParser.START_TAG) {

	                 if (name.equalsIgnoreCase("item")) {
	                     insideItem = true;}
	                 
	                 else if (name.equalsIgnoreCase("title")) {
	                     if (insideItem){
	                     title=myParser.nextText();   
	                     Log.d("discription",title);
	                     }
	                 }
	                 else if (name.equalsIgnoreCase("description")) {
	                     if (insideItem)
	                        description=myParser.nextText();
	                     int k=description.lastIndexOf("/");
	                     description=description.substring(k+2,description.length());
	                 }
	                  
	             } else if (event == XmlPullParser.END_TAG
	                     && name.equalsIgnoreCase("item")) {
	            	// mylist.add(map);
	                 insideItem = false;
	             }
	            // map.put("tit", title);
	             //map.put("des", description);
	            
	            event = myParser.next(); 
	           }
	             
	          } catch (Exception e) {
	             e.printStackTrace();
	          }
			return null;
		}
		  protected void onPostExecute(String file_url) {
        	  t1.setText(title);
        	  t2.setText(description);   
        	 
        	  pDialog.dismiss();

 	    }
		
	}
}
