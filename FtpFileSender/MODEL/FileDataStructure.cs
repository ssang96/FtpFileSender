using System;

namespace FtpFileSender.MODEL
{
    /// <summary>
    /// 데이터 전송 전, 파일을 만들기 위한 Base 정보 클래스
    /// </summary>
    class FileDataStructure
    {
        public DateTime dates { get; set; }
        public string rowData { get; set; }
        public string type { get; set; }
    }
}
