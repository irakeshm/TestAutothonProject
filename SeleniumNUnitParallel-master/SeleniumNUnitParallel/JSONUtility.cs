using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNUnitParallel
{
    enum variablename
    {
        team,
        video,
        upcomingvideo
    }
    public class VideoResult
    {
        public string team { get; set; }
        public string video { get; set; }
        public IList<string> upcomingvideo { get; set; }
    }
    public class JSONUtility
    {
        public static void createJSONFile(List<string> listofupcomingVideos)
        {
            VideoResult data = new VideoResult
            {
                team = "Stryker",
                video = "step-inforum",
                upcomingvideo = listofupcomingVideos,
            };
            File.WriteAllText(@"..\\..\\ResultsData\result.json", JsonConvert.SerializeObject(data));
        }



         
        
    }
}
