using System;
using System.Collections.Generic;
using System.Linq;

namespace Timetable.Site.Infrastructure
{
    public class CutNameService
    {
        public string Cut(string t)
        {
            var wordCount = 0;
            var words = new List<string>();
            var result = "";

            foreach (var tt in t.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                words.Add(tt);
                wordCount++;
            }

            if (wordCount == 1)
                foreach (var w in words)
                {
                    var len = w.Length;
                    if (len > 6)
                        result = w[0] + w[1] + w[2] + "-" + w[len-3] + w[len-2] + w[len-1];
                    else
                        result = w;
                }

            if (wordCount == 2)
                foreach (var w in words)
                {
                    var len = w.Length;
                    if (len > 3)
                        result += w[0] + w[1] + w[2] + ".";
                    else
                        result += w + ".";
                }

            if (wordCount > 2)
                result = words.Aggregate(result, (current, w) => current + w[0].ToString().ToUpper());

            return result;
        }
    }
}
