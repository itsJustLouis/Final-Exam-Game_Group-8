using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;
public class TimelineCOntroller : MonoBehaviour
{
    public bool skipTimeline = false;
    public PlayableDirector playableDirector;

    void Update()
    {
        if (skipTimeline)
        {
            SkipTimeline();
            skipTimeline = false;
        }
    }

    void SkipTimeline()
    {
        if (playableDirector != null)
        {
            double endTime = playableDirector.playableAsset.duration;           
            playableDirector.time = endTime;
            playableDirector.Evaluate();
        }
    }
    public void OnSkipButtonClicked()
    {
        skipTimeline = true;
    }



    public void NextSlide()
    {
        MoveToNextSlide();
    }

    public void MoveToNextSlide()
    {
        if (playableDirector != null)
        {
            double currentTime = playableDirector.time;

            // Find the next keyframe or marker on the timeline
            double nextKeyframe = GetNextKeyframe(currentTime);

            // Move the timeline to the next keyframe
            playableDirector.time = nextKeyframe;

            // Evaluate to make sure the changes take effect
            playableDirector.Evaluate();
        }
    }

    double GetNextKeyframe(double currentTime)
    {
        if (playableDirector != null && playableDirector.playableAsset != null)
        {
            TimelineAsset timelineAsset = playableDirector.playableAsset as TimelineAsset;

            if (timelineAsset != null)
            {
                // Iterate through the markers and keyframes on the timeline
                foreach (IMarker marker in timelineAsset.markerTrack.GetMarkers())
                {
                    if (marker.time > currentTime)
                    {
                        return marker.time;
                    }
                }

                // If no markers are found, find the next keyframe
                foreach (TrackAsset track in timelineAsset.GetRootTracks())
                {
                    foreach (TimelineClip clip in track.GetClips())
                    {
                        if (clip.start > currentTime)
                        {
                            return clip.start;
                        }
                    }
                }
            }
        }

        // If no keyframes or markers are found, return the end time of the timeline
        return playableDirector.playableAsset.duration;
    }




    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main-Scene");
    }
}
