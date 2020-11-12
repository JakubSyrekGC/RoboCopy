using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Diagnostics;

namespace Robo_Copy
{
    [Cmdlet("Robo", VerbsCommon.Copy)]
    public class RoboCopyCmdlet : PSCmdlet
    {
        private string _source;
        private string _destination;
        private string _filename;
        private string[] _params;

        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "Source folder path"
            )]
        [Alias("Source", "SourcePath")]
        public string SourceFolder
        {
            get { return _source;  }
            set { _source = value; }
        }

        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            ValueFromPipeline = true,
            Position = 1,
            HelpMessage = "Destination folder path"
            )]
        [Alias("Destination", "DetinationPath")]
        public string DestinationFolder
        {
            get { return _destination; }
            set { _destination = value; }
        }

        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            ValueFromPipeline = true,
            Position = 2,
            HelpMessage = "Source file"
            )]
        [Alias("SourceFileName")]
        public string SourceFile
        {
            get { return _filename; }
            set { _filename = value; }
        }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            ValueFromPipeline = true,
            Position = 3,
            HelpMessage = "Robocopy parameters as array of strings"
            )]
        [Alias("Params","Arguments")]
        public string[] Parameters
        {
            get { return _params; }
            set { _params = value; }
        }

        protected override void ProcessRecord()
        {
            using (Process p = new Process())
            {
                StringBuilder execBuilder = new StringBuilder();
                execBuilder.Append(string.Format("/C ROBOCOPY {0} {1} {2}", _source, _destination, _filename));
                
                if (this._params != null)
                {
                    foreach (var par in this._params)
                    {
                        execBuilder.Append(par);
                    }
                }

                p.StartInfo.Arguments = execBuilder.ToString();

                p.StartInfo.FileName = "CMD.EXE";
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.Start();
                p.WaitForExit();
            }
            base.ProcessRecord();
        }
        

    }
}
