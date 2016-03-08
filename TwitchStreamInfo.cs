using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.Xml.Linq;
using TwitchViewerUtility;


namespace TwitchStreamInfo //change this if you want
{
    public class TwitchTvapiModel
    {
        public string ChannelOwner { get; set; }
        public string ChannelUrl { get; set; }
        public int ViewerCount { get; set; }
        public string Status { get; set; }
        public string Username { get; set; }
        public string GameTitle { get; set; }
        public int Followers { get; set; }
        public int TotalViews { get; set; }
    }
    public static class TwitchStreamInfo
    {
        public static TwitchTvapiModel getStreamInfo(string channel)
        {
            var streamInfo = new TwitchTvapiModel();

            string uriEndPoint = @"https://api.twitch.tv/kraken/streams/" + channel;

            WebClient webClient = new WebClient();
            dynamic result = DynamicJson.Parse(webClient.DownloadString(@uriEndPoint));

            dynamic data = DynamicJson.Serialize(result.channel);

            dynamic viewerCount = data.viewers;
            dynamic status = data.status;
            dynamic channelUrl = data.url;
            dynamic gameTitle = data.game;
            dynamic username = result.display_name;
            dynamic followers = result.followers;
            dynamic totalViews = result.views;

            

            streamInfo.ViewerCount = Int32.Parse(viewerCount);
            streamInfo.Status = status.ToString();
            streamInfo.ChannelUrl = channelUrl.ToString();
            streamInfo.GameTitle = gameTitle.ToString();
            streamInfo.Username = username.ToString();
            streamInfo.Followers = Int32.Parse(followers);
            streamInfo.TotalViews = Int32.Parse(totalViews);

            return streamInfo;
        }
    }
}
