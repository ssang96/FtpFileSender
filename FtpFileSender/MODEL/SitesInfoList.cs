using System;
using System.Collections;

namespace FtpFileSender.MODEL
{
    public class SitesInfoList
    {
        private static Hashtable _siteInfoLists = new Hashtable();
                
        public static bool AddInfo(string siteName, string siteCode, string fileName, string lastedDate = "")
        {
            bool result = true;

            SiteInfo siteInfo       = new SiteInfo();
            siteInfo.SiteCode       = siteCode;
            siteInfo.SiteName       = siteName;
            siteInfo.DataFile       = fileName;
            siteInfo.LastestDate    = lastedDate;
           
            try
            {
                _siteInfoLists.Add(fileName, siteInfo);
            }
            catch(ArgumentException ae)
            {
                result = false;
            }
                       
            return result;
        }
         
        public static bool RemoveInfo(string siteCode, string fileName)
        {
            bool result = true;

            try
            {
                _siteInfoLists.Remove(fileName);
            }
            catch (ArgumentException ae)
            {
                result = false;
            }

            return result;
        }

        public static bool CheckExisWithSiteCodet(string siteCode)
        {
            var result = false;
            if (_siteInfoLists.Count > 0)
            {
                foreach(SiteInfo  siteInfo in _siteInfoLists.Values)
                {
                    if (siteInfo.SiteCode == siteCode)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public static bool CheckExistWithSiteName(string siteName )
        {
            var result = false;
            if (_siteInfoLists.Count > 0)
            {
                result = _siteInfoLists.Contains(siteName);
            }
            return result;
        }

        public static Hashtable GetSitesInfoList()
        {
            return _siteInfoLists;
        }

        public static SiteInfo GetSiteInfo(string siteName)
        {
            return (SiteInfo) _siteInfoLists[siteName];
        }

        public static bool UpdateLastedDate(string siteName, DateTime lastedDate)
        {
            bool result = true;

            try
            {
                ((SiteInfo)_siteInfoLists[siteName]).LastestDate = lastedDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            return result;
        }
    }
}
