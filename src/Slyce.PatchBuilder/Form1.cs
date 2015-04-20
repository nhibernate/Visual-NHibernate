using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Slyce.Common;

namespace PatchBuilder
{
    public partial class Form1 : Form
    {
        private string ArchiveFolder = @"D:\Projects\Slyce\ArchAngel\trunk\Installer\Archive";
        private string NsisGenFolder = @"D:\Downloads\nsisPatchGen";
        private string NsisFolder = @"C:\Program Files (x86)\NSIS";
        private string newVersion;
        private List<string> oldVersions;

        public Form1()
        {
            InitializeComponent();

            Populate();
        }

        public void Populate()
        {
            Slyce.Common.VersionNumberUtility.VersionNumberComparer c = new Slyce.Common.VersionNumberUtility.VersionNumberComparer();
            List<string> dirs = new List<string>(Directory.GetDirectories(ArchiveFolder, "2.*"));

            for (int i = 0; i < dirs.Count; i++)
                dirs[i] = dirs[i].Substring(dirs[i].LastIndexOf(Path.DirectorySeparatorChar) + 1);

            dirs.Sort(c);
            dirs.Reverse();

            string currentVersion = "";
            List<string> uniqueDirs = new List<string>();

            for (int i = 0; i < dirs.Count; i++)
            {
                string ver = dirs[i].Substring(0, dirs[i].LastIndexOf("."));

                if (ver != currentVersion)
                {
                    currentVersion = ver;
                    uniqueDirs.Add(dirs[i]);
                }
            }
            listView1.Items.Clear();

            for (int i = 1; i < uniqueDirs.Count; i++)
            {
                listView1.Items.Add(uniqueDirs[i]);
            }
            comboBox1.Text = uniqueDirs.First();
            
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            richTextBox1.Clear();
            newVersion = comboBox1.Text;
            oldVersions = new List<string>();

            foreach (ListViewItem item in listView1.Items)
            {
                if (!item.Checked)
                    continue;

                oldVersions.Add(item.Text);
            }
            //CreateUpdateXml(newVersion, oldVersions);
            backgroundWorker1.RunWorkerAsync();
        }

        private void CreatePatches()
        {
            string deployDir = Path.Combine(Path.Combine(ArchiveFolder, newVersion), "Patches");
            Slyce.Common.Utility.DeleteDirectoryBrute(deployDir);
            Directory.CreateDirectory(deployDir);
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            oldVersions.EachParallel(oldVer =>
            {
                string newVersionDir = Path.Combine(Path.Combine(ArchiveFolder, newVersion), "Files");
                string oldVersionDir = Path.Combine(Path.Combine(ArchiveFolder, oldVer), "Files");
                string outputDir = @"C:\Users\Gareth\Desktop\VPatch test\" + oldVer;
                Slyce.Common.Utility.DeleteDirectoryBrute(outputDir);
                Directory.CreateDirectory(outputDir);
                string outputFilename = string.Format("Visual NHibernate Patch {0} to {1}.nsi", oldVer, newVersion);
                outputFilename = Path.Combine(outputDir, outputFilename);

                WriteLineStatus("Creating patches: " + oldVer);

                Slyce.Common.Utility.DeleteDirectoryContentsBrute(outputDir);
                string filename = Path.Combine(NsisGenFolder, "nsisPatchGen.exe");
                string args = string.Format(@"--hidden --system ""{0}"" ""{1}"" ""{2}"" ""{3}""", oldVersionDir, newVersionDir, outputDir, outputFilename);
                CommandLine(filename, args, NsisGenFolder, false);
                WriteLineStatus(sw.Elapsed.ToString());

                System.IO.TextReader tr = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PatchBuilder.InstallScript.nsi"));
                string nsis = tr.ReadToEnd();

                nsis = nsis.Replace(@"!include ""NewPatch.nsi""", string.Format(@"!include ""{0}""", outputFilename));
                string exeFileName = string.Format(@"Visual NHibernate Patch - to {0} from {1}.exe", newVersion, oldVer);
                nsis = nsis.Replace(@"OutFile ""testInstaller.exe""", string.Format(@"OutFile ""{0}""", Path.Combine(deployDir, exeFileName)));
                nsis = nsis.Replace(@"!define PATCH_SOURCE_ROOT ""C:\PathOfNewFiles""", string.Format(@"!define PATCH_SOURCE_ROOT ""{0}""", newVersionDir));
                nsis = nsis.Replace(@"!define PATCH_FILES_ROOT ""C:\Users\Gareth\Desktop\VPatch test""", string.Format(@"!define PATCH_FILES_ROOT ""{0}""", outputDir));

                outputFilename = Path.Combine(outputDir, "xxx.nsi");
                Slyce.Common.Utility.DeleteFileBrute(outputFilename);
                File.WriteAllText(outputFilename, nsis);

                filename = Path.Combine(NsisFolder, "makensis.exe");
                args = string.Format(@"""{0}""", outputFilename);
                WriteLineStatus("Creating installer: " + oldVer);
                CommandLine(filename, args, NsisFolder, false);

                WriteLineStatus(sw.Elapsed.ToString());
                WriteLineStatus("");
            });
            WriteLineStatus("Finished");
            WriteLineStatus(sw.Elapsed.ToString());
        }

        private void CreateUpdateXml()
        {
            string deployDir = Path.Combine(Path.Combine(ArchiveFolder, newVersion), "Patches");
            string newFile = Path.Combine(Path.Combine(ArchiveFolder, newVersion), string.Format("Visual NHibernate Setup {0}.exe", newVersion));
            string filesize = new FileInfo(newFile).Length.ToString();
            string md5 = Slyce.Common.Utility.GetCheckSumOfFile(newFile);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format(@"<updatepatches latest_version=""{0}"" file=""Visual%20NHibernate%20Setup%20{0}.exe"" filesize=""{1}"" md5=""{2}"">", newVersion, filesize, md5));

            foreach (string oldVersion in oldVersions)
            {
                string patchFile = Path.Combine(deployDir, string.Format(@"Visual NHibernate Patch - to {0} from {1}.exe", newVersion, oldVersion));
                filesize = new FileInfo(patchFile).Length.ToString();
                sb.AppendLine(string.Format(@"<patch fromVersion=""{0}"" file=""{2}"" filesize=""{3}"" />", oldVersion, newVersion, Path.GetFileName(patchFile), filesize));
            }
            sb.AppendLine("</updatepatches>");
            File.WriteAllText(Path.Combine(Path.Combine(ArchiveFolder, newVersion), "update_paths_visualnhibernate.xml"), sb.ToString());
        }

        private bool CommandLine(string exePath, string args, string workingDirectory, bool showOutput)
        {
            StringBuilder sb = new StringBuilder(500);

            try
            {
                // See: http://msdn2.microsoft.com/en-us/library/a4sf02ac(VS.80).aspx
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(exePath, args);
                psi.RedirectStandardOutput = false;
                psi.RedirectStandardInput = false;
                psi.RedirectStandardError = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.WorkingDirectory = workingDirectory;
                System.Diagnostics.Process process = System.Diagnostics.Process.Start(psi);
                process.WaitForExit();


                if (process == null)
                    throw new Exception("Could not create a new process.");

                using (StreamReader oReader2 = process.StandardOutput)
                {
                    //using (StreamWriter myStreamWriter = process.StandardInput)
                    //{
                    string output;

                    while (!process.HasExited)
                    {
                        //THIS IS THE LINE THAT BLOCKS
                        //if (oReader2.Peek() > 0 && (output = oReader2.ReadLine()) != null)
                        //if ((output = oReader2.ReadLine()) != null)
                        //if (!string.IsNullOrEmpty(output = oReader2.ReadLine()))
                        //{
                        //    if (showOutput)
                        //    {
                        //        WriteLineStatus(output);
                        //    }
                        //    else
                        //    {
                        //        // Store the output
                        //        sb.AppendLine(output);
                        //    }
                        //}
                        oReader2.DiscardBufferedData();

                        //oReader2.ReadToEnd();
                        System.Threading.Thread.Sleep(200);
                    }
                //    // Final cleanup
                //    if ((output = oReader2.ReadToEnd()) != null)
                //    {
                //        if (showOutput)
                //        {
                //            WriteLineStatus(output);
                //        }
                //        else
                //        {
                //            // Store the output
                //            sb.AppendLine(output);
                //        }
                //    }
                //    //}
                    oReader2.Close();
                }
                //process.WaitForExit();
                int exitCode = process.ExitCode;
                string err = process.StandardError.ReadToEnd();

                if (exitCode == 0)// && string.IsNullOrEmpty(err))
                {
                    //WriteStatus("OK");
                    return true;
                }
                else
                {
                    WriteStatus(string.Format("{0}\nFAILED", sb));

                    if (!string.IsNullOrEmpty(err))
                    {
                        WriteStatus(string.Format("ERRORS: " + err));
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                WriteStatus(string.Format("FAILED\nCOMMAND LINE OUTPUT:\n{0}\n\nEXCEPTION:\n{1}", sb, ex.Message));
                return false;
            }
        }

        private void WriteStatus(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => WriteStatus(text)));
                return;
            }
            richTextBox1.AppendText(text);
            Application.DoEvents();
        }

        private void WriteLineStatus(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => WriteLineStatus(text)));
                return;
            }
            richTextBox1.AppendText(text + Environment.NewLine);
            Application.DoEvents();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            CreatePatches();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CreateUpdateXml();
            Cursor = Cursors.Default;
            MessageBox.Show(this, "Finished!");
        }

    }
}
