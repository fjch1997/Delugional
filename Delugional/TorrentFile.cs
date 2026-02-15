using System;
using System.Collections.Generic;

namespace Delugional
{
    public class TorrentFile
    {
        internal TorrentFile(object data)
        {
            var collection = data as Dictionary<object, object>;
            foreach (var item in collection)
            {
                if ((string)item.Key == "index")
                    Index = Convert.ToInt32(item.Value);
                if ((string)item.Key == "path")
                    Path = item.Value as string;
                if ((string)item.Key == "size")
                    Size = Convert.ToInt64(item.Value);
                if ((string)item.Key == "offset")
                    Offset = Convert.ToInt64(item.Value);
            }
        }

        public int Index { get; private set; }
        public string Path { get; private set; }
        public long Size { get; private set; }
        public long Offset { get; private set; }
    }
}