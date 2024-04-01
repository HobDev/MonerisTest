using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace MonerisTest.Pages;

public class TransactionDetailPage : ContentPage
{
    enum ReceiptRows {OnlineReceipt, MerchantCompanyName,MerchantAddress, MerchantWebsite, TransactionType,OrederId, DateTimeAndApprovalCode, ReferenceNumberAndResponseISoCode, ItemsInformation, ServiceInfo, CustomerInformation, BillTo, BillToAddress, ReturnAndRefundPolicy, ReturnAndRefundPolicyDetails }

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
							 (ReceiptRows.BillToAddress, Auto),
							 (ReceiptRows.ReturnAndRefundPolicy, Auto),
							 (ReceiptRows.ReturnAndRefundPolicyDetails, Auto)
                             ),
                ColumnDefinitions = Columns.Define(
                            (ReceiptColumns.First, Star),
							(ReceiptColumns.Second, Star)
                             ),
				Children =
				{
					new Label { Text = "ONLINE RECEIPT", FontSize = 15, FontAttributes = FontAttributes.Bold, HorizontalOptions=LayoutOptions.Center }.Row(ReceiptRows.OnlineReceipt).Column(ReceiptColumns.First).ColumnSpan(2),

					new Label { FontSize = 15, FontAttributes = FontAttributes.Bold, HorizontalOptions=LayoutOptions.Center }.Bind(Label.TextProperty,$"{nameof(viewModel.BookingInvoice)}.{nameof(viewModel.BookingInvoice.MerchantName)}").Row(ReceiptRows.MerchantCompanyName).Column(ReceiptColumns.First).ColumnSpan(2),

					new Label { FontSize = 15,  HorizontalOptions=LayoutOptions.Center }.Bind(Label.TextProperty,$"{nameof(viewModel.BookingInvoice)}.{nameof(viewModel.BookingInvoice.StoreAddress)}").Row(ReceiptRows.MerchantAddress).Column(ReceiptColumns.First).ColumnSpan(2),

					new Label { FontSize = 15, FontAttributes = FontAttributes.Bold, HorizontalOptions=LayoutOptions.Center }.Bind(Label.TextProperty,$"{nameof(viewModel.BookingInvoice)}.{nameof(viewModel.BookingInvoice.MerchantUrl)}").Row(ReceiptRows.MerchantWebsite).Column(ReceiptColumns.First).ColumnSpan(2),

					new Label {  FontSize = 15, FontAttributes = FontAttributes.Bold }.Row(ReceiptRows.TransactionType).Column(ReceiptColumns.First),
				}
            }
			
		};

		BindingContext = viewModel;
	}
}