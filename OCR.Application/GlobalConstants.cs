using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR.Application
{
    public static class GlobalConstants
    {
        public const string FilesLocalStorage = "Files";
        public const string AudiosLocalStorage = "Audios";
        public const string IOExceptionBadRequest = "Posible formato de archivo no soportado.";
        private static string WebApiURL = "";
        private static string WebRootPath = "";

        public static void SetWebApiURL(string url)
        {
            WebApiURL = url;
        }

        public static string GetWebApiURL()
        {
            return WebApiURL;
        }

        public static void SetWebRootPath(string rootPath)
        {
            WebRootPath = rootPath;
        }

        public static string GetWebRootPath()
        {
            return WebRootPath;
        }
    }
}
