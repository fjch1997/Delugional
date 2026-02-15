using System;
using System.Collections.Generic;

namespace Delugional
{
    public class TorrentPeer
    {
        internal TorrentPeer(object data)
        {
            var collection = data as Dictionary<object, object>;
            foreach (var item in collection)
            {
                if ((string)item.Key == "client")
                    Client = item.Value as string;
                if ((string)item.Key == "country")
                    Country = item.Value as string;
                if ((string)item.Key == "down_speed")
                    DownSpeed = Convert.ToInt64(item.Value);
                if ((string)item.Key == "ip")
                    Ip = item.Value as string;
                if ((string)item.Key == "progress")
                    Progress = Convert.ToDouble(item.Value);
                if ((string)item.Key == "seed")
                    Seed = Convert.ToInt32(item.Value);
                if ((string)item.Key == "up_speed")
                    UpSpeed = Convert.ToInt64(item.Value);
            }
        }

        public string Client { get; private set; }
        public string Country { get; private set; }
        public long DownSpeed { get; private set; }
        public string Ip { get; private set; }
        public double Progress { get; private set; }
        public int Seed { get; private set; }
        public long UpSpeed { get; private set; }
    }
}