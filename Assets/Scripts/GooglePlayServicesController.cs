using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayServicesController : MonoBehaviour
{
	void Start ()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        if (!Social.localUser.authenticated)
            Social.localUser.Authenticate(success => { });
    }

    // Leaderboard methods:
    public static void SubmitScoreToLeaderboard(string leaderboardId, long score)
    {
        if (Social.localUser.authenticated)
            Social.ReportScore(score, leaderboardId, success => { });
    }

    public static void ShowLeaderboardUI()
    {
        if (!Social.localUser.authenticated)
            Social.localUser.Authenticate(success => { Social.ShowLeaderboardUI(); });
        else
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GooglePlayIds.leaderboard_high_scores);
    }
}