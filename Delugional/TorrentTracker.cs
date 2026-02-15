using System;
using System.Collections.Generic;

namespace Delugional
{
    public class TorrentTracker
    {
        internal TorrentTracker(object data)
        {
            var collection = data as Dictionary<object, object>;
            foreach (var item in collection)
            {
                switch ((string)item.Key)
                {
                    case "url":
                        Url = item.Value as string;
                        break;
                    case "tier":
                        Tier = Convert.ToInt32(item.Value);
                        break;
                    case "fail_limit":
                        FailLimit = Convert.ToInt32(item.Value);
                        break;
                    case "verified":
                        Verified = Convert.ToBoolean(item.Value);
                        break;
                    case "source":
                        Source = Convert.ToInt32(item.Value);
                        break;
                    case "trackerid":
                        TrackerId = item.Value as string;
                        break;
                    case "message":
                        Message = item.Value as string;
                        break;
                    case "last_error":
                        LastError = item.Value as Dictionary<object, object>;
                        break;
                    case "next_announce":
                        NextAnnounce = item.Value != null ? TimeSpan.FromSeconds(Convert.ToDouble(item.Value)) : null;
                        break;
                    case "min_announce":
                        MinAnnounce = item.Value != null ? TimeSpan.FromSeconds(Convert.ToDouble(item.Value)) : null;
                        break;
                    case "scrape_incomplete":
                        ScrapeIncomplete = Convert.ToInt32(item.Value);
                        break;
                    case "scrape_complete":
                        ScrapeComplete = Convert.ToInt32(item.Value);
                        break;
                    case "scrape_downloaded":
                        ScrapeDownloaded = Convert.ToInt32(item.Value);
                        break;
                    case "fails":
                        Fails = Convert.ToInt32(item.Value);
                        break;
                    case "updating":
                        Updating = Convert.ToBoolean(item.Value);
                        break;
                    case "start_sent":
                        StartSent = Convert.ToBoolean(item.Value);
                        break;
                    case "complete_sent":
                        CompleteSent = Convert.ToBoolean(item.Value);
                        break;
                    case "endpoints":
                        Endpoints = item.Value as object[];
                        break;
                    case "send_stats":
                        SendStats = Convert.ToBoolean(item.Value);
                        break;
                }
            }
        }

        public string Url { get; private set; }
        public int Tier { get; private set; }
        public int FailLimit { get; private set; }
        public bool Verified { get; private set; }
        public int Source { get; private set; }
        public string TrackerId { get; private set; }
        public string Message { get; private set; }
        public Dictionary<object, object> LastError { get; private set; }
        public TimeSpan? NextAnnounce { get; private set; }
        public TimeSpan? MinAnnounce { get; private set; }
        public int ScrapeIncomplete { get; private set; }
        public int ScrapeComplete { get; private set; }
        public int ScrapeDownloaded { get; private set; }
        public int Fails { get; private set; }
        public bool Updating { get; private set; }
        public bool StartSent { get; private set; }
        public bool CompleteSent { get; private set; }
        public object[] Endpoints { get; private set; }
        public bool SendStats { get; private set; }
    }
}