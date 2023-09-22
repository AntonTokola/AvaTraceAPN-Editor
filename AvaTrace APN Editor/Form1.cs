namespace AvaTrace_APN_Editor
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        private Label lblInstruction;
        private ComboBox cmbApnAddresses;
        private Button btnApply;
        private TextBox txtManualApn;

        private static readonly string NcatPath = FindNmapPath(); //Etsii nmapin asennushakemiston
        private const string SourceIp = "192.168.0.220";
        private const string TargetIp = "192.168.0.1";
        private const int TargetPort = 4578;
        private const int RetryDelayMilliseconds = 5000; // 5 sekunnin odotus ennen uudelleenyritt‰mist‰
        private const string ConnectionSuccessMessage = "AvaTrace\nCommand Line Interface";
        private string selectedSimProviderDomain;
        private StreamWriter _currentWriter;


        public async Task ExecuteNcatCommand()
        {
            bool isConnected = false;
            Console.WriteLine("Paina Avan 'Connect'-nappia v‰hint‰‰n 5-sekuntia pohjassa, ja paina sen j‰lkeen enter, tai mit‰ tahansa nappia.");
            Console.ReadLine();
            Console.WriteLine("Yhteytt‰ yritet‰‰n muodostaa, t‰ss‰ saattaa kest‰‰ hetki.. Odota ole hyv‰.");
            while (!isConnected)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = NcatPath,
                    Arguments = $"-s {SourceIp} {TargetIp} {TargetPort}",
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (Process process = new Process())
                {
                    string outputData = "";

                    process.StartInfo = startInfo;
                    process.OutputDataReceived += (sender, e) =>
                    {
                        //Console.WriteLine(e.Data);
                        outputData += e.Data + "\n"; // Lis‰‰ uuden rivin jokaisen rivin loppuun
                        if (outputData.Contains(ConnectionSuccessMessage))
                        {
                            isConnected = true;
                        }
                    };
                    //process.ErrorDataReceived += (sender, e) => Console.WriteLine($"{e.Data}");

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    await Task.Run(() => process.WaitForExit(10000));

                    if (isConnected)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            statusText.Text = "Yhteys muodostettu onnistuneesti.";
                        });

                        _currentWriter = process.StandardInput;

                        _currentWriter.WriteLine("sh c"); // Kirjoita kovakoodattu komentosi t‰h‰n
                        Thread.Sleep(5000);

                        string forcedApnValue = ExtractForcedApnValue("ForcedApn", outputData);
                        unitIdentifier.Text = ExtractForcedApnValue("InstrumentType", outputData) + " - " + ExtractForcedApnValue("InstrumentID", outputData);
                        unitIdentifier.Visible = true;


                        if (forcedApnValue != null)
                        {
                            if (forcedApnValue == "")
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    currentApnAddress.Text = "Mittarille ei ole m‰‰ritetty t‰ll‰ hetkell‰ erillist‰ APN-osoitetta.\n(t‰m‰ valinta sopii mm. Elisan ja DNA:n liittymille).";
                                });
                            }
                            else
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    currentApnAddress.Text = ($"Mittarin nykyinen APN-osoite:\n{forcedApnValue}");
                                });
                            }

                        }
                        else
                        {
                            statusText.Text = "APN-osoitetta ei lˆytynyt. Mittarille ei ole mahdollisesti m‰‰ritelty osoitetta.";
                        }

                        label1.Visible = true;
                        label4.Visible = true;
                        apnComboBox.Visible = true;
                        applyApnAddressButton.Visible = true;



                    }
                    else
                    {
                        //Console.WriteLine($"Yhteysyritys ep‰onnistui, kokeillaan uudelleen {RetryDelayMilliseconds / 1000} sekunnin kuluttua...");
                        await Task.Delay(RetryDelayMilliseconds);
                    }
                }
            }
        }

        public static string FindNmapPath()
        {
            string[] foldersToCheck = new[]
            {
        Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
        Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
    };

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    foreach (string folder in foldersToCheck)
                    {
                        string potentialPath = Path.Combine(drive.RootDirectory.FullName, folder, "Nmap");

                        if (Directory.Exists(potentialPath))
                        {
                            string ncatPath = Path.Combine(potentialPath, "ncat.exe");

                            if (File.Exists(ncatPath))
                            {
                                return ncatPath; // Palauttaa ensimm‰isen lˆydetyn sijainnin
                            }
                        }
                    }
                }
            }
            return null; // Jos Nmap ei lˆytynyt mist‰‰n
        }

        // T‰m‰ funktio palauttaa ForcedApn arvon annetusta tekstist‰ tai null jos arvoa ei lˆydy.
        public static string ExtractForcedApnValue(string key, string outputData)
        {

            string[] lines = outputData.Split('\n');
            foreach (var line in lines)
            {
                if (line.Contains(key))
                {
                    // Etsi rivi, joka alkaa "ForcedApn" avaimella ja hae sen j‰lkeen ":" merkin j‰lkeinen arvo.
                    var parts = line.Split(':');
                    if (parts.Length > 1)
                    {
                        return parts[1].Trim();
                    }
                }
            }
            return null;
        }



        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            statusText.Text = "Nmap sovellusta etsit‰‰n..";
            avaImage.Visible = false;
            startButton.Visible = false;
            loadingImage.Visible = true;            
            string nmapPath = FindNmapPath();
            loadingImage.Visible = false;
            if (nmapPath == null)
            {
                statusText.Text = "Nmap-sovelluksen asennushakemistoa ei lˆydy.\nAPN-osoitteen m‰‰rityst‰ ei voida tehd‰.\nSulje ohjelma, ja asenna Nmap ensin.";
                quitButton.Visible = true;
            }
            else
            {                
                statusText.Text = "Nmap-sovellus lˆytyi tietokoneeltasi seuraavasta hakemistosta:\n'" + nmapPath + "'.\n\nSeuraavaksi paina Avan 'Connect'-nappia v‰hint‰‰n 5-sekuntia pohjassa.\nKun ensimm‰inen yhteysvalo alkaa vilkkua, suorita laitteen luku.";
                scanAvaTraceUnit.Visible = true;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            label4.Visible = false;
            label3.Visible = false;
            apnComboBox.Visible = false;
            apnTextBox.Visible = false;
            applyApnAddressButton.Visible = false;
            scanAvaTraceUnit.Visible = false;
            unitIdentifier.Visible = false;
            statusText.AutoSize = true;
            quitButton.Visible = false;

            loadingImage.Visible = false;
            apnComboBox.Items.Add("Elisa (IoT/Pool) - APN osoitetta ei m‰‰ritet‰");
            apnComboBox.Items.Add("DNA (IoT) - APN osoitetta ei m‰‰ritet‰");
            apnComboBox.Items.Add("Telia (IoT/Pool) - APN osoite m‰‰ritet‰‰n");
            apnComboBox.Items.Add("M‰‰rittele APN-osoite manuaalisesti");
            loadingImage.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("AvaTrace_APN_Editor.Blinking squares.gif"));
            loadingImage.Left = (this.ClientSize.Width - loadingImage.Width) / 2;
            loadingImage.Top = (this.ClientSize.Height - loadingImage.Height) / 2;
            loadingImage.Size = new Size(128, 128);

            avaImage.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("AvaTrace_APN_Editor.AVAMonitoring-Produkt-M80-1.png"));
        }

        private async void scanAvaTraceUnit_Click(object sender, EventArgs e)
        {
            quitButton.Visible = true;
            statusText.Text = "Yhteytt‰ muodostetaan, t‰ss‰ saattaa kest‰‰ hetki..";
            scanAvaTraceUnit.Visible = false;
            loadingImage.Visible = true;
            await ExecuteNcatCommand();
            loadingImage.Visible = false;
            quitButton.Visible = false;
        }

        private void apnComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (apnComboBox.SelectedItem.ToString())
            {
                case "Elisa (IoT/Pool) - APN osoitetta ei m‰‰ritet‰":
                    selectedSimProviderDomain = "\"\"";
                    break;
                case "DNA (IoT) - APN osoitetta ei m‰‰ritet‰":
                    selectedSimProviderDomain = "\"\"";
                    break;
                case "Telia (IoT/Pool) - APN osoite m‰‰ritet‰‰n":
                    selectedSimProviderDomain = "internet.telia.iot";
                    break;
                case "M‰‰rittele APN-osoite manuaalisesti":
                    selectedSimProviderDomain = "manual";
                    break;
                default:
                    selectedSimProviderDomain = "\"\"";
                    break;
            }

            if (selectedSimProviderDomain == "manual")
            {
                label3.Visible = true;
                apnTextBox.Visible = true;
            }
            else
            {
                label3.Visible = false;
                apnTextBox.Visible = false;
            }
        }

        private void currentApnAddress_Click(object sender, EventArgs e)
        {

        }

        private void applyApnAddressButton_Click(object sender, EventArgs e)
        {
            string newApnAddress = "\"\"";


            if (apnTextBox.Text != "")
            {
                newApnAddress = apnTextBox.Text;
            }
            else
            {
                newApnAddress = selectedSimProviderDomain;
            }

            _currentWriter.WriteLine("set para ForcedApn " + newApnAddress); //t‰m‰n suoritus pit‰isi saada "applyApnAddressButton" napin taakse!
            Thread.Sleep(5000);
            _currentWriter.WriteLine("sh c");
            Thread.Sleep(5000);
        }

        private void apnTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void loadingImage_Click(object sender, EventArgs e)
        {

        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
