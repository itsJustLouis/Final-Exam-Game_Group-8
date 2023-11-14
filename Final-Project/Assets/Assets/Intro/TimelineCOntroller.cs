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
        //if (skipTimeline)
        //{
        //    SkipTimeline();
        //    skipTimeline = false;
        //}
    }

    //void SkipTimeline()
    //{
    //    if (playableDirector != null)
    //    {
    //        double endTime = playableDirector.playableAsset.duration;           
    //        playableDirector.time = endTime;
    //        playableDirector.Evaluate();
    //    }
    //}



    public void MoveToNextSlide()
    {
        if (playableDirector != null)
        {
            double currentTime = playableDirector.time;
            playableDirector.Pause();
            double nextKeyframe = GetNextKeyframe(currentTime);
            playableDirector.time = nextKeyframe;
            playableDirector.Play();
        }
    }

    double GetNextKeyframe(double currentTime)
    {
        if (playableDirector != null && playableDirector.playableAsset != null)
        {
            TimelineAsset timelineAsset = playableDirector.playableAsset as TimelineAsset;

            if (timelineAsset != null)
            {
                if (timelineAsset.markerTrack != null)
                {
                    foreach (IMarker marker in timelineAsset.markerTrack.GetMarkers())
                    {
                        if (marker.time > currentTime)
                        {
                            return marker.time;
                        }
                    }
                }

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

        return playableDirector != null ? playableDirector.playableAsset.duration : 0;
    }



//public void OnSkipButtonClicked()
//    {
//        skipTimeline = true;
//    }
    public void NextSlide()
    {
        MoveToNextSlide();
    }
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main-Scene");
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
