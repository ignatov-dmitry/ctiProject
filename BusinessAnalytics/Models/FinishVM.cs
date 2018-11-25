using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessAnalytics.Models
{
    public class FinishVM
    {
        public string NameVid { get; set; }
        public byte[] Img { get; set; }
        public List<VidFinishVM> finishVMs { get; set; }
        public FinishVM()
        {
            finishVMs = new List<VidFinishVM>();
        }
    }
}