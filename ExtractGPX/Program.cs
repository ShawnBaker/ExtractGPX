using System.Reflection;
using FFmpeg.AutoGen;
using FrozenNorth.Gpx;
using FrozenNorth.Gpmf;

namespace ExtractGPX
{
	internal class Program
	{
		static unsafe void Main(string[] args)
		{
			// get and check the parameters
			if (args.Length != 1 && args.Length != 2)
			{
				Console.WriteLine("Usage: ExtractGPX mp4-file-name [gpx-file-name]");
				Environment.Exit(-1);
				return;
			}
			string videoFileName = args[0];
			if (!File.Exists(videoFileName))
			{
				Console.WriteLine("File {0} doesn't exist.", videoFileName);
				Environment.Exit(-2);
				return;
			}
			if (Path.GetExtension(videoFileName).ToLower() != ".mp4")
			{
				Console.WriteLine("The input file must be an MP4 file.");
				Environment.Exit(-2);
				return;
			}
			string gpxFileName = (args.Length == 2) ? args[1] : Path.ChangeExtension(videoFileName, "gpx");

            // configure FFMPEG
            Assembly assembly = Assembly.GetExecutingAssembly();
			ffmpeg.RootPath = Path.Combine(Path.GetDirectoryName(assembly.Location), "FFmpeg");

			// get the GPMF items
			Console.Write("Reading MP4 file {0}...", Path.GetFileName(videoFileName));
			GpmfItems gpmfItems = Gpmf.LoadMP4(videoFileName);
			Console.WriteLine();

			// get the GPS items as a GPX object
			Console.Write("Extracting GPS data...");
			Gpx gpx= Gpmf.GetGpx(gpmfItems);
			Console.WriteLine();

			// save the GPX object to a file
			if (gpx.Tracks.Count > 0)
			{
				Version version = assembly.GetName().Version;
				gpx.Creator = "ExtractGPX " + version.ToString((version.Revision != 0) ? 3 : 2);
				gpx.Tracks[0].Name = Path.GetFileName(videoFileName);
				Console.Write("Saving GPX file {0}...", Path.GetFileName(gpxFileName));
				GpxWriter.Save(gpx, gpxFileName);
			}
			else
			{
				Console.WriteLine("ERROR: No GPS data found in MP4 file.");
			}
		}
	}
}