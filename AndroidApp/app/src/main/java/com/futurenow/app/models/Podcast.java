package com.futurenow.app.models;

import com.google.gson.annotations.SerializedName;

public class Podcast {
    @SerializedName("id")
    private int id;
    
    @SerializedName("title")
    private String title;
    
    @SerializedName("episode")
    private String episode;
    
    @SerializedName("host")
    private String host;
    
    @SerializedName("durationSeconds")
    private int durationSeconds;
    
    @SerializedName("category")
    private String category;
    
    @SerializedName("url")
    private String url;
    
    @SerializedName("createdAt")
    private String createdAt;

    public int getId() { return id; }
    public void setId(int id) { this.id = id; }

    public String getTitle() { return title; }
    public void setTitle(String title) { this.title = title; }

    public String getEpisode() { return episode; }
    public void setEpisode(String episode) { this.episode = episode; }

    public String getHost() { return host; }
    public void setHost(String host) { this.host = host; }

    public int getDurationSeconds() { return durationSeconds; }
    public void setDurationSeconds(int durationSeconds) { this.durationSeconds = durationSeconds; }

    public String getCategory() { return category; }
    public void setCategory(String category) { this.category = category; }

    public String getUrl() { return url; }
    public void setUrl(String url) { this.url = url; }

    public String getCreatedAt() { return createdAt; }
    public void setCreatedAt(String createdAt) { this.createdAt = createdAt; }

    public String getDuration() {
        int minutes = durationSeconds / 60;
        int seconds = durationSeconds % 60;
        return String.format("%d:%02d", minutes, seconds);
    }
}
