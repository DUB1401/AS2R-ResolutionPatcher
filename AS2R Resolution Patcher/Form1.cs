using System.Diagnostics;
using System.Text;

namespace AS2R_Resolution_Patcher {

	public partial class Form1 : Form {

		public Form1() {
			InitializeComponent();
		}

		//================================================================================//
		//---> Данные.
		//================================================================================//

		// Состояния: выполнены ли условия патчинга.
		bool SteamInstalled = false, SavesFolderExists = false;
		// Количество найденных нативных файлов.
		int ExistsNativeFilesCount = 0;
		// Разрешение экрана по оси X.
		string ResolutionX = "";
		// Разрешение экрана по оси Y.
		string ResolutionY = "";
		// Путь к папке игры.
		string GameFolder = "";

		//================================================================================//
		//---> Вспомогательные методы.
		//================================================================================//

		// Проверяет наличие нативных файлов и устанавливает для них цвета в контейнере.
		private void CheckNativeFiles(string GameFolder) {

			// Проверка существования нативного файла: damageframe1024.men.
			if (File.Exists(GameFolder + "\\Maps\\damageframe1024.men")) { label16.ForeColor = System.Drawing.Color.Green; ExistsNativeFilesCount += 1; }
			else label16.ForeColor = System.Drawing.Color.Red;

			// Проверка существования нативного файла: gamebar_gun1024.men.
			if (File.Exists(GameFolder + "\\Maps\\gamebar_gun1024.men")) { label11.ForeColor = System.Drawing.Color.Green; ExistsNativeFilesCount += 1; }
			else label11.ForeColor = System.Drawing.Color.Red;

			// Проверка существования нативного файла: gamebar_net1024.men.
			if (File.Exists(GameFolder + "\\Maps\\gamebar_net1024.men")) { label12.ForeColor = System.Drawing.Color.Green; ExistsNativeFilesCount += 1; }
			else label12.ForeColor = System.Drawing.Color.Red;

			// Проверка существования нативного файла: gamebar1024.men.
			if (File.Exists(GameFolder + "\\Maps\\gamebar1024.men")) { label13.ForeColor = System.Drawing.Color.Green; ExistsNativeFilesCount += 1; }
			else label13.ForeColor = System.Drawing.Color.Red;

			// Проверка существования нативного файла: radiotalk1024.men.
			if (File.Exists(GameFolder + "\\Maps\\radiotalk1024.men")) { label14.ForeColor = System.Drawing.Color.Green; ExistsNativeFilesCount += 1; }
			else label14.ForeColor = System.Drawing.Color.Red;

			// Проверка существования нативного файла: nag1024.men.
			if (File.Exists(GameFolder + "\\Maps\\nag1024.men")) { label15.ForeColor = System.Drawing.Color.Green; ExistsNativeFilesCount += 1; }
			else label15.ForeColor = System.Drawing.Color.Red;

			// Проверка существования нативного файла: CONST.LGC.
			if (File.Exists(GameFolder + "\\Maps\\CONST.LGC")) {
				// Текст файла конфигурации текстурирования.
				string FileCONST = File.ReadAllText(GameFolder + "\\Maps\\CONST.LGC", Encoding.GetEncoding(1251));

				// Проверка нативности файла.
				if (FileCONST.Contains("static int RT_SCREEN_X[] = { 800, 1024 };")) { label24.ForeColor = System.Drawing.Color.Green; ExistsNativeFilesCount += 1; }
				else label24.ForeColor = System.Drawing.Color.Red;
			}
			else label24.ForeColor = System.Drawing.Color.Red;
		}

		// Получает выбранное разрешение игры.
		private void GetResolution() {

			// Если выбрано предустановленное разрешение.
			if (comboBox1.SelectedIndex < 4) {
				// Выбранный объект.
				object ResolutionItem = comboBox1.SelectedItem;
				// Строка выбранного объекта.
				string ResolutionString = Convert.ToString(ResolutionItem);
				// Получение разрешения экрана по оси X.
				ResolutionX = ResolutionString.Split("(")[1].Split("×")[0].Trim();
				// Получение разрешения экрана по оси Y.
				ResolutionY = ResolutionString.Split("×")[1].Replace(")", "").Trim();
			}
			else {
				// Получение разрешения экрана по оси X.
				ResolutionX = textBox1.Text.Trim();
				// Получение разрешения экрана по оси Y.
				ResolutionY = textBox2.Text.Trim();
			}
		}

		// Выполняет патчинг игры.
		private void Patch() {
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
			// Текст файла конфигурации текстурирования.
			string FileCONST = File.ReadAllText(GameFolder + "\\Maps\\CONST.LGC", Encoding.GetEncoding(1251));
			// Изменение разрешения в тексте файла конфигурации текстурирования.
			FileCONST = FileCONST.Replace("static int RT_SCREEN_X[] = { 800, 1024 };", "static int RT_SCREEN_X[] = { 800, " + ResolutionX + " };");
			FileCONST = FileCONST.Replace("static int RT_SCREEN_Y[] = { 600, 768 };", "static int RT_SCREEN_Y[] = { 600, " + ResolutionY + " };");
			// Сохранение файла конфигурации текстурирования.
			File.WriteAllText(GameFolder + "\\Maps\\CONST.LGC", FileCONST, Encoding.GetEncoding(1251));
			label25.ForeColor = System.Drawing.Color.Green;
		}

		// Автоматически определяет и, если возможно, устанавливает разрешение экрана по умолчанию.
		private void ResolutionAutodetection() {
			// Установка выбора кастомного разрешения.
			comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
			// Ширина экрана.
			string Width = Screen.PrimaryScreen.Bounds.Width.ToString();
			// Высота экрана.
			string Height = Screen.PrimaryScreen.Bounds.Height.ToString();

			// Установка ширины и высоты в кастомное разрешение.
			textBox1.Text = Width;
			textBox2.Text = Height;

			// Проверка каждого разрешение на соответствие реальному.
			for (int i = 0; i < comboBox1.Items.Count; i++) if (comboBox1.Items[i].ToString().Contains(Width) && comboBox1.Items[i].ToString().Contains(Height)) comboBox1.SelectedIndex = i;
		}

		// Переименовывает файлы в правой колонке согласно выбранному разрешению.
		private void SetRightResolutions() {
			// Переименование файлов в правой колонке согласно выбранному разрешению.
			label17.Text = label16.Text.Replace("1024", ResolutionX);
			label22.Text = label11.Text.Replace("1024", ResolutionX);
			label21.Text = label12.Text.Replace("1024", ResolutionX);
			label20.Text = label13.Text.Replace("1024", ResolutionX);
			label19.Text = label14.Text.Replace("1024", ResolutionX);
			label18.Text = label15.Text.Replace("1024", ResolutionX);
		}

		//================================================================================//
		//---> Методы обработки взаимодействий с UI.
		//================================================================================//

		private void Form1_Load(object sender, EventArgs e) {
			
			// Имя текущего пользователя ОС Windows.
			string UserName = Environment.UserName;
			// Автоматическое определение разрешения экрана.
			ResolutionAutodetection();
			// Получение выбранного разрешения экрана.
			GetResolution();
			// Установка версии продукта в футер приложения.
			label8.Text = Application.ProductVersion;

			// Если установлен Steam.
			if (File.Exists("C:\\Program Files (x86)\\Steam\\steam.exe")) {
				// Установка успешного состояния для поля «Steam».
				label3.Text = "OK";
				label3.ForeColor = System.Drawing.Color.Green;
				// Отметка о выполнении условия патчинга: клиент Steam установлен.
				SteamInstalled = true;
			}

			// Если найдена директория сохранений.
			if (Directory.Exists("C:\\Users\\" + UserName + "\\Documents\\AlienShooter2 Reloaded Saves")) {
				// Установка успешного состояния для поля «Saves folder».
				label4.Text = "OK";
				label4.ForeColor = System.Drawing.Color.Green;
				// Отметка о выполнении условия патчинга: игра запускалась.
				SavesFolderExists = true;
				// Получение текста файла конфигурации.
				GameFolder = File.ReadAllText("C:\\Users\\" + UserName + "\\Documents\\AlienShooter2 Reloaded Saves\\Saves\\_global.dat", Encoding.UTF8);
				// Получение пути к папке игры.
				GameFolder = GameFolder.Split("\n")[0].Replace("sGamePath=", "").Trim();

				// Вывод пути к папке игры.
				label6.Text = GameFolder;
				label6.ForeColor = System.Drawing.Color.Green;
			}

			// Получение количества найденных нативных файлов.
			CheckNativeFiles(GameFolder);

			// Проверка выполнения условий патчинга.
			if (ExistsNativeFilesCount == 7 && SteamInstalled && SavesFolderExists) button1.Enabled = true; 
			else button1.Enabled = false;

		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			// Открытие страницы проекта на GitHub.
			System.Diagnostics.Process.Start(new ProcessStartInfo { FileName = "https://github.com/DUB1401/AS2R-ResolutionPatcher", UseShellExecute = true });
		}

		private void button1_Click(object sender, EventArgs e) {
			// Установка патча.
			Patch();

			// Инактивация кнопки.
			button1.Text = "Patched!";
			button1.Enabled = false;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {

			// Если выбрано кастомное разрешение.
			if (comboBox1.SelectedIndex == 4) {
				// Включение видимости блока кастомного разрашения.
				textBox1.Visible = true;
				textBox2.Visible = true;
				label27.Visible = true;
				label28.Visible = true;

				// Если вручную не указано кастомное разрешение.
				if (textBox1.Text == "" || textBox2.Text == "") button1.Enabled = false;
				else button1.Enabled = true;
			}
			else {
				// Выключение видимости блока кастомного разрашения.
				textBox1.Visible = false;
				textBox2.Visible = false;
				label27.Visible = false;
				label28.Visible = false;

				// Проверка выполнения условий патчинга.
				if (ExistsNativeFilesCount == 7 && SteamInstalled && SavesFolderExists) button1.Enabled = true;
			}

			// Получение выбранного разрешения экрана.
			GetResolution();
			// Переименовать файлы в правой колонке согласно выбранному разрешению.
			SetRightResolutions();
		}

		private void textBox1_TextChanged(object sender, EventArgs e) {

			// Если вручную не указано кастомное разрешение.
			if (textBox1.Text == "" || textBox2.Text == "") button1.Enabled = false;
			else {
				// Получение выбранного разрешения экрана.
				GetResolution();
				// Переименовать файлы в правой колонке согласно выбранному разрешению.
				SetRightResolutions();

				// Если патч не установлен, то активировать кнопку.
				if (button1.Text != "Patched!") button1.Enabled = true;
			}
		}

		private void textBox2_TextChanged(object sender, EventArgs e) {

			// Если вручную не указано кастомное разрешение.
			if (textBox1.Text == "" || textBox2.Text == "") button1.Enabled = false;
			else {
				// Получение выбранного разрешения экрана.
				GetResolution();
				// Переименовать файлы в правой колонке согласно выбранному разрешению.
				SetRightResolutions();

				// Если патч не установлен, то активировать кнопку.
				if (button1.Text != "Patched!") button1.Enabled = true;
			}
		}

	}
}
