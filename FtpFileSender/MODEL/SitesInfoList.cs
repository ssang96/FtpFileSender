using System;
using System.Collections;

namespace FtpFileSender.MODEL
{
    /// <summary>
    /// 각 지점의 정보를 관리하기 위한 List 클래스
    /// </summary>
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
         
        /// <summary>
        /// 정보 관리 리스트에서 한개의 사이트의 하나의 정보 삭제하는 메소드
        /// 각 지점별로 2개의 데이터 파일이 생성됨(slow, flux)
        /// </summary>
        /// <param name="siteCode"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 사이트 코드로 이미 있는지 체크하는 메소드
        /// </summary>
        /// <param name="siteCode"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 사이트의 파일명으로 이미 있는지 체크하는 메소드
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 원본 데이터 파일에서 데이터를 읽어와서 파일을 생성하는데, 
        /// 그 마지막으로 파일을 생성한 시간을 Update 하는 메소드
        /// 프로그램이 중지되거나, 재 실행되면, 아래 시간을 기준으로 그 이후 시간의 데이터만 파일로 생성하기 위함
        /// </summary>
        /// <param name="siteName"></param>
        /// <param name="lastedDate"></param>
        /// <returns></returns>
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
