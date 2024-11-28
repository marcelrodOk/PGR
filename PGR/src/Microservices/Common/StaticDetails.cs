namespace Common
{
	public class StaticDetails
	{
		public static string SecManAPIBase { get; set; }
		public static string GatewayAPIBase { get; set; }

		public static string ReportAPIBase { get; set; }
		public static string ShippingInvoiceAPIBase { get; set; }
		public static string CentralizerAPIBase { get; set; }
		public static string FormulaAPIBase { get; set; }
		public static string SupplyAPIBase { get; set; }
		public static string LoteClosureAPIBase { get; set; }
		public static string ApiKey { get; set; }

		public enum ApiType
		{
			GET,
			POST,
			PUT,
			DELETE
		}
	}
}
