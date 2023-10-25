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
        private const int RetryDelayMilliseconds = 5000; // 5 sekunnin odotus ennen uudelleenyritt�mist�
        private const string ConnectionSuccessMessage = "AvaTrace\nCommand Line Interface";
        private string selectedSimProviderDomain;
        private StreamWriter _currentWriter;
        private Process _process;
        private string _publicDeviceOutput = "";
        string selectedLanguage;

        //Language variables
        public string statusText_yhteysMuodostettuOnnistuneesti;
        public string currentApnAddress_apnOsoitettaEiOleM��ritetty;
        public string currentApnAddress_nykyinenOsoite;
        public string statusText_apnOsoitettaEiL�ytynyt;
        public string statusText_osoitettaAPNosoitettaVaihdetaan;
        public string currentApnAddress_APNosoitteenVaihtoOnnistuiEiApnOsoitettaM��ritelty;
        public string currentApnAddress_APNosoiteVaihdettu_1;
        public string currentApnAddress_APNosoiteVaihdettu_2;
        public string statusText_APNosoitettaEiL�ytynytSuljeSovellus;
        public string statusText_nmapAsennustaEiL�ydy;
        public string statusText_nmapSovellusL�ytyi_1;
        public string statusText_nmapSovellusL�ytyi_2;
        public string statusText_nmapSovellustaEtsit��n;
        public string messageBox_HaluatkoSulkeaSovelluksen;
        public string statusText_yhteytt�Muodostetaan;
        public string scanAvaTraceUnitButton_text;
        public string quitButton_text;
        public string applyApnAddressButton_text;
        public string APNAddressInfoText_text;
        public string pickUpAddressFromList_text;
        public string addAddressManually_text;

        private void ProcessOutputHandler(object sender, DataReceivedEventArgs e)
        {
            _publicDeviceOutput += e.Data + "\n";
        }

        public async Task ExecuteNcatCommand()
        {
            bool isConnected = false;
            Console.WriteLine("Paina Avan 'Connect'-nappia v�hint��n 5-sekuntia pohjassa, ja paina sen j�lkeen enter, tai mit� tahansa nappia.");
            Console.ReadLine();
            Console.WriteLine("Yhteytt� yritet��n muodostaa, t�ss� saattaa kest�� hetki.. Odota ole hyv�.");
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
                    //outputData += e.Data + "\n"; // Lis�� uuden rivin jokaisen rivin loppuun

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
                        statusText.Text = statusText_yhteysMuodostettuOnnistuneesti;
                        confirmedImage.Visible = true;
                    });



                    _currentWriter = _process.StandardInput;

                    _currentWriter.WriteLine("sh c"); // Kirjoita kovakoodattu komentosi t�h�n
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
                                currentApnAddress.Text = currentApnAddress_apnOsoitettaEiOleM��ritetty;
                            });
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                currentApnAddress.Text = (currentApnAddress_nykyinenOsoite + $"\n{forcedApnValue}");
                            });
                        }

                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            statusText.Text = statusText_apnOsoitettaEiL�ytynyt;
                        });

                    }
                    this.Invoke((MethodInvoker)delegate
                    {
                        APNAddressInfoText.Visible = true;
                        pickUpAddressFromList.Visible = true;
                        apnComboBox.Visible = true;
                        applyApnAddressButton.Visible = true;
                    });




                }
                else
                {
                    //Console.WriteLine($"Yhteysyritys ep�onnistui, kokeillaan uudelleen {RetryDelayMilliseconds / 1000} sekunnin kuluttua...");
                    await Task.Delay(RetryDelayMilliseconds);
                }

            }
        }

        private async void applyApnAddressButton_Click(object sender, EventArgs e)
        {

            this.Invoke((MethodInvoker)delegate
            {
                confirmedImage.Visible = false;
                currentApnAddress.Visible = false;
                APNAddressInfoText.Visible = false;
                pickUpAddressFromList.Visible = false;
                apnComboBox.Visible = false;
                applyApnAddressButton.Visible = false;
                loadingImage.Visible = true;
            });


            statusText.Text = statusText_osoitettaAPNosoitettaVaihdetaan;

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
                        currentApnAddress.Text = currentApnAddress_APNosoitteenVaihtoOnnistuiEiApnOsoitettaM��ritelty;
                    });
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        currentApnAddress.Text = (currentApnAddress_APNosoiteVaihdettu_1 + $"\n{forcedApnValue}\n\n" + currentApnAddress_APNosoiteVaihdettu_2);
                    });
                }

            }
            else
            {
                statusText.Visible = true;
                statusText.Text = statusText_APNosoitettaEiL�ytynytSuljeSovellus;
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
                                return ncatPath; // Palauttaa ensimm�isen l�ydetyn sijainnin
                            }
                        }
                    }
                }
            }
            return null; // Jos Nmap ei l�ytynyt mist��n
        }

        // T�m� funktio palauttaa ForcedApn arvon annetusta tekstist� tai null jos arvoa ei l�ydy.
        public static string ExtractForcedApnValue(string key, string outputData)
        {

            string[] lines = outputData.Split('\n');
            foreach (var line in lines)
            {
                if (line.Contains(key))
                {
                    // Etsi rivi, joka alkaa "ForcedApn" avaimella ja hae sen j�lkeen ":" merkin j�lkeinen arvo.
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
            finLanguageRadioButton.Visible = false;
            engLanguageRadioButton.Visible = false;
            statusText.Text = statusText_nmapSovellustaEtsit��n;
            avaImage.Visible = false;
            startButton.Visible = false;
            loadingImage.Visible = true;
            string nmapPath = FindNmapPath();
            loadingImage.Visible = false;
            if (nmapPath == null)
            {
                statusText.Text = statusText_nmapAsennustaEiL�ydy;
                quitButton.Visible = true;
            }
            else
            {
                statusText.Text = statusText_nmapSovellusL�ytyi_1 + "\n'" + nmapPath + "'.\n\n" + statusText_nmapSovellusL�ytyi_2;
                scanAvaTraceUnit.Visible = true;
            }

        }
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // T�ss� esimerkiss� kysyt��n k�ytt�j�lt�, haluaako h�n varmasti sulkea sovelluksen.
            if (MessageBox.Show(messageBox_HaluatkoSulkeaSovelluksen, "AvaTraceAPN Editor", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;  // Est� sovelluksen sulkeminen
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
            APNAddressInfoText.Visible = false;
            pickUpAddressFromList.Visible = false;
            addAddressManually.Visible = false;
            apnComboBox.Visible = false;
            apnTextBox.Visible = false;
            applyApnAddressButton.Visible = false;
            scanAvaTraceUnit.Visible = false;
            unitIdentifier.Visible = false;
            statusText.AutoSize = true;
            quitButton.Visible = false;
            confirmedImage.Visible = false;
            confirmedImage.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("AvaTrace_APN_Editor.confirmed_1.png"));
            engLanguageRadioButton.Visible = true;
            finLanguageRadioButton.Visible = true;
            engLanguageRadioButton.Checked = true;


            loadingImage.Visible = false;
            apnComboBox.Items.Add("Elisa (IoT/Pool)");
            apnComboBox.Items.Add("DNA (IoT)");
            apnComboBox.Items.Add("Telia (IoT/Pool)");
            apnComboBox.Items.Add("Define APN-address manually");
            loadingImage.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("AvaTrace_APN_Editor.Spinner-2.gif"));

            avaImage.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("AvaTrace_APN_Editor.AVAMonitoring-Produkt-M80-1.png"));
        }

        private async void scanAvaTraceUnit_Click(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                quitButton.Visible = true;
                statusText.Text = statusText_yhteytt�Muodostetaan;
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
                case "Elisa (IoT/Pool)":
                    selectedSimProviderDomain = "\"\"";
                    break;
                case "DNA (IoT)":
                    selectedSimProviderDomain = "\"\"";
                    break;
                case "Telia (IoT/Pool)":
                    selectedSimProviderDomain = "internet.telia.iot";
                    break;
                case "Define APN-address manually":
                    selectedSimProviderDomain = "manual";
                    break;
                default:
                    selectedSimProviderDomain = "\"\"";
                    break;
            }

            if (selectedSimProviderDomain == "manual")
            {
                addAddressManually.Visible = true;
                apnTextBox.Visible = true;
            }
            else
            {
                addAddressManually.Visible = false;
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void engLanguageRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            startButton.Text = "Check Nmap-application installation";
            selectedLanguage = "english";
            confirmedImage.Location = new Point(255, confirmedImage.Location.Y);

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            statusText_yhteysMuodostettuOnnistuneesti = Strings.statusText_yhteysMuodostettuOnnistuneesti;
            currentApnAddress_nykyinenOsoite = Strings.currentApnAddress_nykyinenOsoite;
            statusText_apnOsoitettaEiL�ytynyt = Strings.statusText_apnOsoitettaEiL�ytynyt;
            statusText_osoitettaAPNosoitettaVaihdetaan = Strings.statusText_osoitettaAPNosoitettaVaihdetaan;
            currentApnAddress_APNosoitteenVaihtoOnnistuiEiApnOsoitettaM��ritelty = Strings.currentApnAddress_APNosoitteenVaihtoOnnistuiEiApnOsoitettaM��ritelty;
            currentApnAddress_APNosoiteVaihdettu_1 = Strings.currentApnAddress_APNosoiteVaihdettu_1;
            currentApnAddress_APNosoiteVaihdettu_2 = Strings.currentApnAddress_APNosoiteVaihdettu_2;
            statusText_APNosoitettaEiL�ytynytSuljeSovellus = Strings.statusText_APNosoitettaEiL�ytynytSuljeSovellus;
            statusText_nmapAsennustaEiL�ydy = Strings.statusText_nmapAsennustaEiL�ydy;
            statusText_nmapSovellusL�ytyi_1 = Strings.statusText_nmapSovellusL�ytyi_1;
            statusText_nmapSovellusL�ytyi_2 = Strings.statusText_nmapSovellusL�ytyi_2;
            statusText_nmapSovellustaEtsit��n = Strings.statusText_nmapSovellustaEtsit��n;
            messageBox_HaluatkoSulkeaSovelluksen = Strings.messageBox_HaluatkoSulkeaSovelluksen;
            statusText_yhteytt�Muodostetaan = Strings.statusText_yhteytt�Muodostetaan;
            scanAvaTraceUnitButton_text = Strings.scanAvaTraceUnitButton_text;
            quitButton_text = Strings.quitButton_text;
            applyApnAddressButton_text = Strings.applyApnAddressButton_text;
            APNAddressInfoText.Text = Strings.APNAddressInfoText_text;
            pickUpAddressFromList.Text = Strings.pickUpAddressFromList;
            addAddressManually.Text = Strings.addAddressManually;
            currentApnAddress_apnOsoitettaEiOleM��ritetty = Strings.apnOsoitettaEiOleM��ritetty;
            scanAvaTraceUnit.Text = scanAvaTraceUnitButton_text;
            quitButton.Text = quitButton_text;
            applyApnAddressButton.Text = applyApnAddressButton_text;

        }

        private void finLanguageRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            startButton.Text = "Aloita tarkistamalla Nmap-sovelluksen sijainti";
            selectedLanguage = "finnish";
            confirmedImage.Location = new Point(245, confirmedImage.Location.Y);

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fi-FI");
            statusText_yhteysMuodostettuOnnistuneesti = Strings.statusText_yhteysMuodostettuOnnistuneesti;
            currentApnAddress_nykyinenOsoite = Strings.currentApnAddress_nykyinenOsoite;
            statusText_apnOsoitettaEiL�ytynyt = Strings.statusText_apnOsoitettaEiL�ytynyt;
            statusText_osoitettaAPNosoitettaVaihdetaan = Strings.statusText_osoitettaAPNosoitettaVaihdetaan;
            currentApnAddress_APNosoitteenVaihtoOnnistuiEiApnOsoitettaM��ritelty = Strings.currentApnAddress_APNosoitteenVaihtoOnnistuiEiApnOsoitettaM��ritelty;
            currentApnAddress_APNosoiteVaihdettu_1 = Strings.currentApnAddress_APNosoiteVaihdettu_1;
            currentApnAddress_APNosoiteVaihdettu_2 = Strings.currentApnAddress_APNosoiteVaihdettu_2;
            statusText_APNosoitettaEiL�ytynytSuljeSovellus = Strings.statusText_APNosoitettaEiL�ytynytSuljeSovellus;
            statusText_nmapAsennustaEiL�ydy = Strings.statusText_nmapAsennustaEiL�ydy;
            statusText_nmapSovellusL�ytyi_1 = Strings.statusText_nmapSovellusL�ytyi_1;
            statusText_nmapSovellusL�ytyi_2 = Strings.statusText_nmapSovellusL�ytyi_2;
            statusText_nmapSovellustaEtsit��n = Strings.statusText_nmapSovellustaEtsit��n;
            messageBox_HaluatkoSulkeaSovelluksen = Strings.messageBox_HaluatkoSulkeaSovelluksen;
            statusText_yhteytt�Muodostetaan = Strings.statusText_yhteytt�Muodostetaan;
            scanAvaTraceUnitButton_text = Strings.scanAvaTraceUnitButton_text;
            quitButton_text = Strings.quitButton_text;
            applyApnAddressButton_text = Strings.applyApnAddressButton_text;
            APNAddressInfoText.Text = Strings.APNAddressInfoText_text;
            pickUpAddressFromList.Text = Strings.pickUpAddressFromList;
            addAddressManually.Text = Strings.addAddressManually;
            currentApnAddress_apnOsoitettaEiOleM��ritetty = Strings.apnOsoitettaEiOleM��ritetty;
            scanAvaTraceUnit.Text = scanAvaTraceUnitButton_text;
            quitButton.Text = quitButton_text;
            applyApnAddressButton.Text = applyApnAddressButton_text;
        }
    }
}