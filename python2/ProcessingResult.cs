namespace PythonProcessor
{
	public class ProcessingResult
	{
		public float[] ValuesByHour;

		public float[] Coordinates;

		public string[] StreetNameNum;

		public bool IsAvg;

		public ProcessingResult()
		{
			ValuesByHour = new float[24];
			Coordinates = new float[2];
			StreetNameNum = new string[2];
			IsAvg = false;
		}
	}
}
