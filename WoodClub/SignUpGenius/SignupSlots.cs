namespace WoodClub
{
	public class Rootobject
	{
		public Data data { get; set; }
		public string[] message { get; set; }
		public bool success { get; set; }
	}

	public class Data
	{
		public Customquestion[] customquestions { get; set; }
		public SignupSlot[] signup { get; set; }
	}

	public class Customquestion
	{
		public int customfieldid { get; set; }
		public string title { get; set; }
	}

	public class SignupSlot
	{
		public string address1 { get; set; }
		public string address2 { get; set; }
		public string amountpaid { get; set; }
		public string city { get; set; }
		public string country { get; set; }
		public Customfield[] customfields { get; set; }
		public double enddate { get; set; }
		public string enddatestring { get; set; }
		public double endtime { get; set; }
		public string email { get; set; }
		public string firstname { get; set; }
		public string signupid { get; set; }
		public string item { get; set; }
		public string lastname { get; set; }
		public int myqty { get; set; }
		public string phone { get; set; }
		public string phonetype { get; set; }
		public double startdate { get; set; }
		public string startdatestring { get; set; }
		public double starttime { get; set; }
		public string state { get; set; }
		public string status { get; set; }
		public string zipcode { get; set; }
		//public double itemmemberid { get; set; }
		public double slotitemid { get; set; }
	}

	public class Customfield
	{
		public int customfieldid { get; set; }
		public string value { get; set; }
	}
}
