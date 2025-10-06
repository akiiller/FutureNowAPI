package com.futurenow.app.api;

import com.futurenow.app.models.Music;
import com.futurenow.app.models.Video;
import com.futurenow.app.models.Podcast;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Path;
import retrofit2.http.Query;

public interface ApiService {
    
    @GET("api/Music")
    Call<List<Music>> getAllMusic();
    
    @GET("api/Music/{id}")
    Call<Music> getMusicById(@Path("id") int id);
    
    @GET("api/Music/search")
    Call<List<Music>> searchMusic(
        @Query("genre") String genre,
        @Query("artist") String artist,
        @Query("title") String title
    );
    
    @GET("api/Videos")
    Call<List<Video>> getAllVideos();
    
    @GET("api/Videos/{id}")
    Call<Video> getVideoById(@Path("id") int id);
    
    @GET("api/Videos/search")
    Call<List<Video>> searchVideos(
        @Query("category") String category,
        @Query("title") String title
    );
    
    @GET("api/Podcasts")
    Call<List<Podcast>> getAllPodcasts();
    
    @GET("api/Podcasts/{id}")
    Call<Podcast> getPodcastById(@Path("id") int id);
    
    @GET("api/Podcasts/search")
    Call<List<Podcast>> searchPodcasts(
        @Query("category") String category,
        @Query("host") String host,
        @Query("title") String title
    );
}
