using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace MonerisTest.Pages;

public class TransactionDetailPage : ContentPage
{
    enum ReceiptRows {OnlineReceipt, MerchantCompanyName,MerchantAddress, MerchantWebsite, TransactionType,OrederId, DateTimeAndApprovalCode, ReferenceNumberAndResponseISoCode, ItemsInformation, ServiceInfo, CustomerInformation, BillTo,BillToName, BillToAddress, ReturnAndRefundPolicy, ReturnAndRefundPolicyDetails }

	enum ReceiptColumns { First, Second}

	public TransactionDetailPage(TransactionDetailViewModel viewModel)
	{
		Content = new ScrollView
		{
			Content = new Grid
			{
				RowDefinitions = Rows.Define(
							 (ReceiptRows.OnlineReceipt, Auto),
							 (ReceiptRows.MerchantCompanyName, Auto),
							 (ReceiptRows.MerchantAddress, Auto),
							 (ReceiptRows.MerchantWebsite, Auto),
							 (ReceiptRows.TransactionType, Auto),
							 (ReceiptRows.OrederId, Auto),
							 (ReceiptRows.DateTimeAndApprovalCode, Auto),
							 (ReceiptRows.ReferenceNumberAndResponseISoCode, Auto),
							 (ReceiptRows.ItemsInformation, Auto),
							 (ReceiptRows.ServiceInfo, Auto),
							 (ReceiptRows.CustomerInformation, Auto),
							 (ReceiptRows.BillTo, Auto),
							 (ReceiptRows.BillToName,Auto),
							 (ReceiptRows.BillToAddress, Auto),
							 (ReceiptRows.ReturnAndRefundPolicy, Auto),
							 (ReceiptRows.ReturnAndRefundPolicyDetails, Auto)
							 ),
				ColumnDefinitions = Columns.Define(
							(ReceiptColumns.First, Star),
							(ReceiptColumns.Second, Star)
							 ),
				BackgroundColor = Colors.Gray,
				Padding = new Thickness(10),	
				RowSpacing=5,
				Children =
				{
					new Label { Text = "ONLINE RECEIPT", FontSize = 15, FontAttributes = FontAttributes.Bold, HorizontalOptions=LayoutOptions.Center }.Row(ReceiptRows.OnlineReceipt).Column(ReceiptColumns.First).ColumnSpan(2),

					new Label { FontSize = 15, FontAttributes = FontAttributes.Bold, HorizontalOptions=LayoutOptions.Center }.Bind(Label.TextProperty,$"{nameof(viewModel.BookingInvoice)}.{nameof(viewModel.BookingInvoice.MerchantName)}").Margins(0,10,0,0).Row(ReceiptRows.MerchantCompanyName).Column(ReceiptColumns.First).ColumnSpan(2),

					new Label { FontSize = 15,  HorizontalOptions=LayoutOptions.Center, WidthRequest=100 }.Bind(Label.TextProperty,$"{nameof(viewModel.BookingInvoice)}.{nameof(viewModel.BookingInvoice.StoreAddress)}").Row(ReceiptRows.MerchantAddress).Column(ReceiptColumns.First).ColumnSpan(2),

					new Label { FontSize = 15, FontAttributes = FontAttributes.Bold, HorizontalOptions=LayoutOptions.Center }.Bind(Label.TextProperty,$"{nameof(viewModel.BookingInvoice)}.{nameof(viewModel.BookingInvoice.MerchantUrl)}").Margins(0,10,0,0).Row(ReceiptRows.MerchantWebsite).Column(ReceiptColumns.First).ColumnSpan(2),

					new Label {  FontSize = 15, FontAttributes = FontAttributes.Bold }.Bind(Label.TextProperty,$"{nameof(viewModel.BookingInvoice)}.{nameof(viewModel.BookingInvoice.TransactionType)}", stringFormat:"Transaction Type: {0}").Margins(0,10,0,0).Row(ReceiptRows.TransactionType).Column(ReceiptColumns.First),

					new Label {  FontSize = 15, FontAttributes = FontAttributes.Bold }.Bind(Label.TextProperty,$"{nameof(viewModel.BookingInvoice)}.{nameof(viewModel.BookingInvoice.OrderNumber)}", stringFormat:"Order Id: {0}").Row(ReceiptRows.OrederId).Column(ReceiptColumns.First),

					new Label { FontSize = 15, FontAttributes = FontAttributes.Bold }.Bind(Label.TextProperty,$"{nameof(viewModel.BookingInvoice)}.{nameof(viewModel.BookingInvoice.Transaction_DateTime)}", stringFormat:"Date/Time: {0:YYYY MM dd HH:MM:SS }").Row(ReceiptRows.DateTimeAndApprovalCode).Column(ReceiptColumns.First),


                    new Label { FontSize = 15, FontAttributes = FontAttributes.Bold }.Bind(Label.TextProperty,$"{nameof(viewModel.BookingInvoice)}.{nameof(viewModel.BookingInvoice.AuthorizationNumber)}", stringFormat:"Approval Code: {0}").Margins(0,10,0,0).Row(ReceiptRows.DateTimeAndApprovalCode).Column(ReceiptColumns.Second),

                    new Label { FontSize = 15, FontAttributes = FontAttributes.Bold }.Bind(Label.TextProperty,$"{nameof(viewModel.BookingInvoice)}.{nameof(viewModel.BookingInvoice.ReferenceNumber)}", stringFormat:"Reference Number: {0}").Row(ReceiptRows.ReferenceNumberAndResponseISoCode).Column(ReceiptColumns.First),

					new Label { FontSize = 15 }.Bind(Label.TextProperty,binding1:new Binding( $"{nameof(viewModel.BookingInvoice)}.{nameof(viewModel.BookingInvoice.ResponseCode)}"),binding2: new Binding( $"{nameof(viewModel.BookingInvoice)}.{nameof(viewModel.BookingInvoice.ISOCode)}"), convert:((string? response, string? iso) values)=>$"Response/ISO Code: {values.response}/{values.iso}" ).Row(ReceiptRows.ReferenceNumberAndResponseISoCode).Column(ReceiptColumns.Second),

					new Label {Text="Item Information", FontSize = 15, FontAttributes = FontAttributes.Bold }.Margins(0,10,0,0).Row(ReceiptRows.ItemsInformation).Column(ReceiptColumns.First).ColumnSpan(2),	
					
					new Label {Text="Service Description here", FontSize = 15 }.Bind(Label.TextProperty,$"{nameof(viewModel.BookingInvoice)}.{nameof(viewModel.BookingInvoice.Goods_Description)}").Row(ReceiptRows.ServiceInfo).Column(ReceiptColumns.First).ColumnSpan(2),

					new Label {Text="Customer Information", FontSize = 15, FontAttributes = FontAttributes.Bold }.Margins(0,10,0,0).Row(ReceiptRows.CustomerInformation).Column(ReceiptColumns.First).ColumnSpan(2),

					new Label {Text="Bill To", FontSize = 15, FontAttributes = FontAttributes.Bold }.Row(ReceiptRows.BillTo).Column(ReceiptColumns.First),

					new Label { FontSize = 15, FontAttributes = FontAttributes.Bold }.Bind(Label.TextProperty, $"{nameof(viewModel.BookingInvoice)}.{nameof(viewModel.BookingInvoice.CardHolderName)}").Row(ReceiptRows.BillToName).Column(ReceiptColumns.First),

					new Label { FontSize = 15, FontAttributes = FontAttributes.Bold, WidthRequest=100 }.Bind(Label.TextProperty, $"{nameof(viewModel.BookingInvoice)}.{nameof(viewModel.BookingInvoice.CardHolderAddress)}").Row(ReceiptRows.BillToAddress).Column(ReceiptColumns.First),

					new Label {Text="RETURNS AND REFUND POLICY", FontSize = 15, FontAttributes = FontAttributes.Bold, HorizontalOptions=LayoutOptions.Center }.Margins(0,10,0,0).Row(ReceiptRows.ReturnAndRefundPolicy).Column(ReceiptColumns.First).ColumnSpan(2),

					new Label { FontSize = 15, HorizontalOptions=LayoutOptions.Center }.Bind(Label.TextProperty, $"{nameof(viewModel.BookingInvoice)}.{nameof(viewModel.BookingInvoice.Restrictions)}").Row(ReceiptRows.ReturnAndRefundPolicyDetails).Column(ReceiptColumns.First).ColumnSpan(2)

				}
            }
			
		};

		BindingContext = viewModel;
	}
}