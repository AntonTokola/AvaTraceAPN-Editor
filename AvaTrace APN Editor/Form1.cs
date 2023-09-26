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
        //private const string TargetIp = "192.168.1.31";
        //http://192.168.1.34/
        private const int TargetPort = 4578;
        private const int RetryDelayMilliseconds = 5000; // 5 sekunnin odotus ennen uudelleenyritt‰mist‰
        private const string ConnectionSuccessMessage = "AvaTrace\nCommand Line Interface";
        private string selectedSimProviderDomain;
        private StreamWriter _currentWriter;
        private Process _process;
        private string _publicDeviceOutput = "";

        private void ProcessOutputHandler(object sender, DataReceivedEventArgs e)
        {
            _publicDeviceOutput += e.Data + "\n";
        }

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

                _process = new Process();


                _process.StartInfo = startInfo;
                _process.OutputDataReceived -= ProcessOutputHandler;
                _process.OutputDataReceived += (sender, e) =>
                {
                    //Console.WriteLine(e.Data);
                    ProcessOutputHandler(sender, e);
                    //outputData += e.Data + "\n"; // Lis‰‰ uuden rivin jokaisen rivin loppuun

                    if (_publicDeviceOutput.Contains(ConnectionSuccessMessage))
                    {
                        isConnected = true;
                    }
                };
                //process.ErrorDataReceived += (sender, e) => Console.WriteLine($"{e.Data}");

                _process.Start();
                _process.BeginOutputReadLine();
                _process.BeginErrorReadLine();

                await Task.Run(() => _process.WaitForExit(10000));

                if (isConnected)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        statusText.Text = "Yhteys muodostettu onnistuneesti.";
                    });                    

                    _currentWriter = _process.StandardInput;

                    _currentWriter.WriteLine("sh c"); // Kirjoita kovakoodattu komentosi t‰h‰n
                    Thread.Sleep(5000);

                    string forcedApnValue = ExtractForcedApnValue("ForcedApn", _publicDeviceOutput);
                    this.Invoke((MethodInvoker)delegate
                    {
                        unitIdentifier.Text = ExtractForcedApnValue("InstrumentType", _publicDeviceOutput) + " - " + ExtractForcedApnValue("InstrumentID", _publicDeviceOutput);
                                            unitIdentifier.Visible = true;
                    });
                    


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
                        this.Invoke((MethodInvoker)delegate
                        {
                            statusText.Text = "APN-osoitetta ei lˆytynyt. Mittarille ei ole mahdollisesti m‰‰ritelty osoitetta.";
                        });
                        
                    }
                    this.Invoke((MethodInvoker)delegate
                    {
                        label1.Visible = true;
                        label4.Visible = true;
                        apnComboBox.Visible = true;
                        applyApnAddressButton.Visible = true;
                    });
                    



                }
                else
                {
                    //Console.WriteLine($"Yhteysyritys ep‰onnistui, kokeillaan uudelleen {RetryDelayMilliseconds / 1000} sekunnin kuluttua...");
                    await Task.Delay(RetryDelayMilliseconds);
                }

            }
        }

        private async void applyApnAddressButton_Click(object sender, EventArgs e)
        {

            this.Invoke((MethodInvoker)delegate
            {
                currentApnAddress.Visible = false;
                label1.Visible = false;
                label4.Visible = false;
                apnComboBox.Visible = false;
                applyApnAddressButton.Visible = false;
                loadingImage.Visible = true;
            });


            statusText.Text = "Odota ole hyv‰, APN-osoitetta vaihdetaan..\n\nƒl‰ irrota kaapelia ennen kuin toiminto on suoritettu loppuun.";

            string newApnAddress = "\"\"";

            if (apnTextBox.Text != "")
            {
                newApnAddress = apnTextBox.Text;
            }
            else
            {
                newApnAddress = selectedSimProviderDomain;
            }
            _publicDeviceOutput = "";

            _currentWriter.WriteLine("set para ForcedApn " + newApnAddress);
            await Task.Delay(5000);

            _currentWriter.WriteLine("sh c");
            await Task.Delay(5000);

            _process.OutputDataReceived -= ProcessOutputHandler;
            _process.OutputDataReceived += (sender, e) =>
            {
                _publicDeviceOutput += e.Data + "\n";
            };

            string forcedApnValue = ExtractForcedApnValue("ForcedApn", _publicDeviceOutput);
            if (forcedApnValue != null)
            {
                if (forcedApnValue == "")
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        currentApnAddress.Text = "APN-osoitteen vaihto onnistui!\n\nMittarin uusi APN-osoite: mittarille on m‰‰ritelty tyhj‰ APN-osoite.\n(t‰m‰ valinta sopii mm. Elisan ja DNA:n liittymille).\n\nNyt voit sulkea sovelluksen.";
                    });
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        currentApnAddress.Text = ($"APN-osoitteen vaihto onnistui!\n\nMittarin uusi APN-osoite:\n{forcedApnValue}\n\nNyt voit sulkea sovelluksen.");
                    });
                }

            }
            else
            {
                statusText.Visible = true;
                statusText.Text = "APN-osoitetta ei lˆytynyt. Mittarille ei ole mahdollisesti m‰‰ritelty osoitetta.\n\nSulje sovellus.";
            }

            this.Invoke((MethodInvoker)delegate
            {
                loadingImage.Visible = false;
                statusText.Visible = false;
                currentApnAddress.Visible = true;
                quitButton.Visible = true;
            });


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
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // T‰ss‰ esimerkiss‰ kysyt‰‰n k‰ytt‰j‰lt‰, haluaako h‰n varmasti sulkea sovelluksen.
            if (MessageBox.Show("Haluatko varmasti sulkea sovelluksen?", "AvaTraceAPN Editor", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;  // Est‰ sovelluksen sulkeminen
            }
            if (_currentWriter != null)
            {
                //Sulje yhteys laitteeseen
                _currentWriter.WriteLine("exit");
                //Sulje proserssin kaikki resurssit
                _process.Dispose();
                await Task.Delay(2000);
            }




        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormClosing += MainForm_FormClosing;
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
            loadingImage.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("AvaTrace_APN_Editor.Spinner-2.gif"));

            avaImage.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("AvaTrace_APN_Editor.AVAMonitoring-Produkt-M80-1.png"));
        }

        private async void scanAvaTraceUnit_Click(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                quitButton.Visible = true;
                statusText.Text = "Yhteytt‰ muodostetaan, t‰ss‰ saattaa kest‰‰ hetki..";
                scanAvaTraceUnit.Visible = false;
                loadingImage.Visible = true;
            });

            await Task.Run(() => ExecuteNcatCommand());

            this.Invoke((MethodInvoker)delegate
            {
                loadingImage.Visible = false;
                quitButton.Visible = false;
            });
            
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