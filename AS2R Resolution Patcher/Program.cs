using System.Text;

namespace AS2R_Resolution_Patcher {

	internal static class Program {

		[STAThread]
		static void Main() {
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			ApplicationConfiguration.Initialize();
			Application.Run(new Form1());
			
		}

	}
}