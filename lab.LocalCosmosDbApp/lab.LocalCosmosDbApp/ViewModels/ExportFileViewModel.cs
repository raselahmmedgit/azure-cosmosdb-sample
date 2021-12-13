using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lab.LocalCosmosDbApp.ViewModels
{
    public class ExportFileViewModel
    {
        public MemoryStream FileStream { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
