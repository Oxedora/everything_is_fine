using System;
using System.IO;

public static class GameVariables {
	public static int volMusic;
	public static int volSound;
	public static bool help;
	private static String userPrefFile = "UserPref.txt";

	public static int SliderValueToVolume(float value) {
		return (int) (value * 100.0f);
	}

	public static float VolumeToSliderValue(int volume) {
		return (float)volume / 100.0f;
	}

	public static void UserPrefToVariables(){
		using (StreamReader stream = new StreamReader(userPrefFile)) {
			volMusic = int.Parse(stream.ReadLine());
			volSound = int.Parse(stream.ReadLine());
			help = (stream.ReadLine() == "v" ? true : false);
			stream.Close();
		}
	}

	public static void VariablesToUserPref(){
		using (StreamWriter stream = new StreamWriter(userPrefFile)) {
			stream.Write(volMusic);
			stream.Write("\n");
			stream.Write(volSound);
			stream.Write("\n");
			stream.Write((help ? "v" : "f"));
			stream.Close();
		}
	}
}
