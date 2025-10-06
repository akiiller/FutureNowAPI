package com.futurenow.app.adapters;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;
import com.futurenow.app.models.Podcast;
import java.util.ArrayList;
import java.util.List;

public class PodcastAdapter extends RecyclerView.Adapter<PodcastAdapter.ViewHolder> {
    
    private List<Podcast> podcastList = new ArrayList<>();
    private OnItemClickListener listener;

    public interface OnItemClickListener {
        void onItemClick(Podcast podcast);
    }

    public void setOnItemClickListener(OnItemClickListener listener) {
        this.listener = listener;
    }

    public void setPodcastList(List<Podcast> podcastList) {
        this.podcastList = podcastList;
        notifyDataSetChanged();
    }

    @NonNull
    @Override
    public ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext())
                .inflate(android.R.layout.simple_list_item_2, parent, false);
        return new ViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull ViewHolder holder, int position) {
        Podcast podcast = podcastList.get(position);
        holder.titleText.setText(podcast.getTitle() + " - " + podcast.getEpisode());
        holder.subtitleText.setText(podcast.getHost() + " - " + podcast.getCategory() + " (" + podcast.getDuration() + ")");
        
        holder.itemView.setOnClickListener(v -> {
            if (listener != null) {
                listener.onItemClick(podcast);
            }
        });
    }

    @Override
    public int getItemCount() {
        return podcastList.size();
    }

    static class ViewHolder extends RecyclerView.ViewHolder {
        TextView titleText;
        TextView subtitleText;

        ViewHolder(View itemView) {
            super(itemView);
            titleText = itemView.findViewById(android.R.id.text1);
            subtitleText = itemView.findViewById(android.R.id.text2);
        }
    }
}
