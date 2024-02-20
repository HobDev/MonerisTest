namespace MonerisTest;

public class PaymentPage : ContentPage
{
	public PaymentPage(PaymentViewModel viewModel)
	{
		Content = new VerticalStackLayout
		{
			Spacing=20,

			Margin=new Thickness(20, 30, 20,0),
			Children = 
			{
				new Button{Text="Card Verification", Command= viewModel.CardVerificationCommand},
				new Button{Text="PurChase", Command= viewModel.PurchaseCommand},
				new Button{Text="Purchase Correction", Command= viewModel.PurchaseCorrectionCommand},
				new Button{Text="Refund", Command=viewModel.RefundCommand},
				new Button{Text="Independent Refund", Command= viewModel.IndependentRefundCommand},
				new Button{Text="Open Totals", Command= viewModel.OpenTotalsCommand},
				new Button{Text="Batch Close", Command= viewModel.BatchCloseCommand}
			}
		};

		BindingContext = viewModel;
	}
}