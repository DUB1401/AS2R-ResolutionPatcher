using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace AS2R_Resolution_Patcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Состояния: выполнены ли все условия патчинга.
            bool SteamInstalled = false, SavesFolderExists = false;
            // Имя текущего пользователя.
            string UserName = Environment.UserName;
            // Путь к папке игры.
            string GameFolder = "";
            // Количество найденных нативных файлов.
            int NativeFiles = 0;
            // Установка выбранного по умолчанию разрешения.
            comboBox1.SelectedIndex = 2;

            // Если установлен Steam, то вывести об этом соответствующую надпись.
            if (File.Exists("C:\\Program Files (x86)\\Steam\\steam.exe"))
            {
                label3.Text = "OK";
                label3.ForeColor = System.Drawing.Color.Green;
                SteamInstalled = true;
            }

            // Если найдена директория сохранений, то вывести об этом соответствующую надпись и получить папку игры.
            if (Directory.Exists("C:\\Users\\" + UserName + "\\Documents\\AlienShooter2 Reloaded Saves"))
            {
                label4.Text = "OK";
                label4.ForeColor = System.Drawing.Color.Green;
                SavesFolderExists = true;

                FileStream ReadStream = File.OpenRead("C:\\Users\\" + UserName + "\\Documents\\AlienShooter2 Reloaded Saves\\Saves\\_global.dat");
                // Выделение массива для помещение туда считываемых байтов.
                byte[] Buffer = new byte[ReadStream.Length];
                // Считывание байтов.
                ReadStream.Read(Buffer, 0, Buffer.Length);
                // Декодирование байтов в строку.
                GameFolder = Encoding.Default.GetString(Buffer);
                // Получение пути к папке игры.
                GameFolder = GameFolder.Split("\n")[0].Replace("sGamePath=", "").Trim();
                // Закрытие потока чтения.
                ReadStream.Close();

                label6.Text = GameFolder;
                label6.ForeColor = System.Drawing.Color.Green;

            }

            // Проверка существования нативного файла: damageframe1024.men.
            if (File.Exists(GameFolder + "\\Maps\\damageframe1024.men")) { label16.ForeColor = System.Drawing.Color.Green; NativeFiles += 1; }
            else label16.ForeColor = System.Drawing.Color.Red;

            // Проверка существования нативного файла: gamebar_gun1024.men.
            if (File.Exists(GameFolder + "\\Maps\\gamebar_gun1024.men")) { label11.ForeColor = System.Drawing.Color.Green; NativeFiles += 1; }
            else label11.ForeColor = System.Drawing.Color.Red;

            // Проверка существования нативного файла: gamebar_net1024.men.
            if (File.Exists(GameFolder + "\\Maps\\gamebar_net1024.men")) { label12.ForeColor = System.Drawing.Color.Green; NativeFiles += 1; }
            else label12.ForeColor = System.Drawing.Color.Red;

            // Проверка существования нативного файла: gamebar1024.men.
            if (File.Exists(GameFolder + "\\Maps\\gamebar1024.men")) { label13.ForeColor = System.Drawing.Color.Green; NativeFiles += 1; }
            else label13.ForeColor = System.Drawing.Color.Red;

            // Проверка существования нативного файла: radiotalk1024.men.
            if (File.Exists(GameFolder + "\\Maps\\radiotalk1024.men")) { label14.ForeColor = System.Drawing.Color.Green; NativeFiles += 1; }
            else label14.ForeColor = System.Drawing.Color.Red;

            // Проверка существования нативного файла: nag1024.men.
            if (File.Exists(GameFolder + "\\Maps\\nag1024.men")) { label15.ForeColor = System.Drawing.Color.Green; NativeFiles += 1; }
            else label15.ForeColor = System.Drawing.Color.Red;

            // Получение выбранного разрешения по оси Х.
            object ResolutionItem = comboBox1.SelectedItem;
            string ResolutionString = Convert.ToString(ResolutionItem);
            string ResolutionX = ResolutionString.Split("(")[1].Split("×")[0].Trim();
            string ResolutionY = ResolutionString.Split("×")[1].Replace(")", "").Trim();

            // Установка новых имён файлов.
            label17.Text = label16.Text.Replace("1024", ResolutionX);
            label22.Text = label11.Text.Replace("1024", ResolutionX);
            label21.Text = label12.Text.Replace("1024", ResolutionX);
            label20.Text = label13.Text.Replace("1024", ResolutionX);
            label19.Text = label14.Text.Replace("1024", ResolutionX);
            label18.Text = label15.Text.Replace("1024", ResolutionX);

            // Проверка существования нативного файла: CONST.LGC.
            if (File.Exists(GameFolder + "\\Maps\\CONST.LGC"))
            {
                label24.ForeColor = System.Drawing.Color.Green;

                string FileCONST = File.ReadAllText(GameFolder + "\\Maps\\CONST.LGC", Encoding.GetEncoding(1251));

                // Проверка нативности файла.
                if (FileCONST.Contains("static int RT_SCREEN_X[] = { 800, 1024 };")) NativeFiles += 1;
                else label24.ForeColor = System.Drawing.Color.Red;

            }
            else label24.ForeColor = System.Drawing.Color.Red;

            // Проверка выполнения условий патчинга.
            if (NativeFiles == 7 && SteamInstalled && SavesFolderExists) button1.Enabled = true;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Открытие страницы на GitHub.
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/DUB1401/AS2R-ResolutionPatcher",
                UseShellExecute = true
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Получение выбранного разрешения по оси Х.
            object ResolutionItem = comboBox1.SelectedItem;
            string ResolutionString = Convert.ToString(ResolutionItem);
            string ResolutionX = ResolutionString.Split("(")[1].Split("×")[0].Trim();
            string ResolutionY = ResolutionString.Split("×")[1].Replace(")", "").Trim();

            // Путь к папке игры.
            string GameFolder = label6.Text;

            // Переименование файлов.
            System.IO.File.Move(GameFolder + "\\Maps\\damageframe1024.men", GameFolder + "\\Maps\\damageframe" + ResolutionX + ".men", true);
            label17.ForeColor = System.Drawing.Color.Green;
            System.IO.File.Move(GameFolder + "\\Maps\\gamebar_gun1024.men", GameFolder + "\\Maps\\gamebar_gun" + ResolutionX + ".men", true);
            label22.ForeColor = System.Drawing.Color.Green;
            System.IO.File.Move(GameFolder + "\\Maps\\gamebar_net1024.men", GameFolder + "\\Maps\\gamebar_net" + ResolutionX + ".men", true);
            label21.ForeColor = System.Drawing.Color.Green;
            System.IO.File.Move(GameFolder + "\\Maps\\gamebar1024.men", GameFolder + "\\Maps\\gamebar" + ResolutionX + ".men", true);
            label20.ForeColor = System.Drawing.Color.Green;
            System.IO.File.Move(GameFolder + "\\Maps\\radiotalk1024.men", GameFolder + "\\Maps\\radiotalk" + ResolutionX + ".men", true);
            label19.ForeColor = System.Drawing.Color.Green;
            System.IO.File.Move(GameFolder + "\\Maps\\nag1024.men", GameFolder + "\\Maps\\nag" + ResolutionX + ".men", true);
            label18.ForeColor = System.Drawing.Color.Green;

            string FileCONST = File.ReadAllText(GameFolder + "\\Maps\\CONST.LGC", Encoding.GetEncoding(1251));
            // Установка разрешения игры в файл CONST.LGC.
            FileCONST = FileCONST.Replace("static int RT_SCREEN_X[] = { 800, 1024 };", "static int RT_SCREEN_X[] = { 800, " + ResolutionX + " };");
            FileCONST = FileCONST.Replace("static int RT_SCREEN_Y[] = { 600, 768 };", "static int RT_SCREEN_Y[] = { 600, " + ResolutionY + " };");
            File.WriteAllText(GameFolder + "\\Maps\\CONST.LGC", FileCONST, Encoding.GetEncoding(1251));
            label25.ForeColor = System.Drawing.Color.Green;

            // Инактивация кнопки.
            button1.Text = "Patched!";
            button1.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Получение выбранного разрешения по оси Х.
            object ResolutionItem = comboBox1.SelectedItem;
            string ResolutionString = Convert.ToString(ResolutionItem);
            string ResolutionX = ResolutionString.Split("(")[1].Split("×")[0].Trim();
            string ResolutionY = ResolutionString.Split("×")[1].Replace(")", "").Trim();

            // Установка новых имён файлов.
            label17.Text = label16.Text.Replace("1024", ResolutionX);
            label22.Text = label11.Text.Replace("1024", ResolutionX);
            label21.Text = label12.Text.Replace("1024", ResolutionX);
            label20.Text = label13.Text.Replace("1024", ResolutionX);
            label19.Text = label14.Text.Replace("1024", ResolutionX);
            label18.Text = label15.Text.Replace("1024", ResolutionX);
        }
    }
}