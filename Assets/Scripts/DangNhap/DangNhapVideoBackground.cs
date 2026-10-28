using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public sealed class DangNhapVideoBackground : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Image anhChoVideo;

    private void Awake()
    {
        if (videoPlayer == null) videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer == null || videoPlayer.clip == null) return;

        if (anhChoVideo != null) anhChoVideo.enabled = true;
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = true;
        videoPlayer.waitForFirstFrame = true;
        videoPlayer.sendFrameReadyEvents = true;
        videoPlayer.prepareCompleted += OnPrepared;
        videoPlayer.frameReady += OnFrameReady;
        videoPlayer.Prepare();
    }

    private void OnPrepared(VideoPlayer source)
    {
        source.Play();
    }

    private void OnFrameReady(VideoPlayer source, long frameIndex)
    {
        source.frameReady -= OnFrameReady;
        source.sendFrameReadyEvents = false;
        if (anhChoVideo != null) anhChoVideo.enabled = false;
    }

    private void OnDestroy()
    {
        if (videoPlayer == null) return;
        videoPlayer.prepareCompleted -= OnPrepared;
        videoPlayer.frameReady -= OnFrameReady;
    }
}
