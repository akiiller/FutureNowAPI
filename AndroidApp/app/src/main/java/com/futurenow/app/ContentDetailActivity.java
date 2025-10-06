package com.futurenow.app;

import android.os.Bundle;
import android.widget.TextView;
import androidx.appcompat.app.AppCompatActivity;

public class ContentDetailActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_content_detail);

        TextView titleView = findViewById(R.id.detailTitle);
        TextView descriptionView = findViewById(R.id.detailDescription);
        TextView urlView = findViewById(R.id.detailUrl);

        String title = getIntent().getStringExtra("title");
        String description = getIntent().getStringExtra("description");
        String url = getIntent().getStringExtra("url");

        titleView.setText(title);
        descriptionView.setText(description);
        urlView.setText("URL: " + url);
    }
}
