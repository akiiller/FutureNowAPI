package com.futurenow.app;

import android.os.Bundle;
import android.widget.Toast;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import com.futurenow.app.adapters.MusicAdapter;
import com.futurenow.app.adapters.VideoAdapter;
import com.futurenow.app.adapters.PodcastAdapter;
import com.futurenow.app.api.ApiClient;
import com.futurenow.app.models.Music;
import com.futurenow.app.models.Video;
import com.futurenow.app.models.Podcast;
import com.google.android.material.tabs.TabLayout;
import java.util.List;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends AppCompatActivity {

    private RecyclerView recyclerView;
    private MusicAdapter musicAdapter;
    private VideoAdapter videoAdapter;
    private PodcastAdapter podcastAdapter;
    private TabLayout tabLayout;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        recyclerView = findViewById(R.id.recyclerView);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));

        tabLayout = findViewById(R.id.tabLayout);
        
        tabLayout.addTab(tabLayout.newTab().setText("Music"));
        tabLayout.addTab(tabLayout.newTab().setText("Videos"));
        tabLayout.addTab(tabLayout.newTab().setText("Podcasts"));

        setupAdapters();
        
        tabLayout.addOnTabSelectedListener(new TabLayout.OnTabSelectedListener() {
            @Override
            public void onTabSelected(TabLayout.Tab tab) {
                switch (tab.getPosition()) {
                    case 0:
                        loadMusic();
                        break;
                    case 1:
                        loadVideos();
                        break;
                    case 2:
                        loadPodcasts();
                        break;
                }
            }

            @Override
            public void onTabUnselected(TabLayout.Tab tab) {}

            @Override
            public void onTabReselected(TabLayout.Tab tab) {}
        });

        loadMusic();
    }

    private void setupAdapters() {
        musicAdapter = new MusicAdapter();
        musicAdapter.setOnItemClickListener(music -> 
            Toast.makeText(this, "Playing: " + music.getTitle(), Toast.LENGTH_SHORT).show()
        );

        videoAdapter = new VideoAdapter();
        videoAdapter.setOnItemClickListener(video -> 
            Toast.makeText(this, "Playing: " + video.getTitle(), Toast.LENGTH_SHORT).show()
        );

        podcastAdapter = new PodcastAdapter();
        podcastAdapter.setOnItemClickListener(podcast -> 
            Toast.makeText(this, "Playing: " + podcast.getTitle(), Toast.LENGTH_SHORT).show()
        );
    }

    private void loadMusic() {
        recyclerView.setAdapter(musicAdapter);
        
        ApiClient.getApiService().getAllMusic().enqueue(new Callback<List<Music>>() {
            @Override
            public void onResponse(Call<List<Music>> call, Response<List<Music>> response) {
                if (response.isSuccessful() && response.body() != null) {
                    musicAdapter.setMusicList(response.body());
                } else {
                    showError("Failed to load music");
                }
            }

            @Override
            public void onFailure(Call<List<Music>> call, Throwable t) {
                showError("Error: " + t.getMessage());
            }
        });
    }

    private void loadVideos() {
        recyclerView.setAdapter(videoAdapter);
        
        ApiClient.getApiService().getAllVideos().enqueue(new Callback<List<Video>>() {
            @Override
            public void onResponse(Call<List<Video>> call, Response<List<Video>> response) {
                if (response.isSuccessful() && response.body() != null) {
                    videoAdapter.setVideoList(response.body());
                } else {
                    showError("Failed to load videos");
                }
            }

            @Override
            public void onFailure(Call<List<Video>> call, Throwable t) {
                showError("Error: " + t.getMessage());
            }
        });
    }

    private void loadPodcasts() {
        recyclerView.setAdapter(podcastAdapter);
        
        ApiClient.getApiService().getAllPodcasts().enqueue(new Callback<List<Podcast>>() {
            @Override
            public void onResponse(Call<List<Podcast>> call, Response<List<Podcast>> response) {
                if (response.isSuccessful() && response.body() != null) {
                    podcastAdapter.setPodcastList(response.body());
                } else {
                    showError("Failed to load podcasts");
                }
            }

            @Override
            public void onFailure(Call<List<Podcast>> call, Throwable t) {
                showError("Error: " + t.getMessage());
            }
        });
    }

    private void showError(String message) {
        Toast.makeText(this, message, Toast.LENGTH_LONG).show();
    }
}
