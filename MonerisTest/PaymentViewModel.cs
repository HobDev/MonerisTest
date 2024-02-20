

using MonerisTest.Services.Interfaces;

namespace MonerisTest
{
    public class PaymentViewModel
    {
        public Command CardVerificationCommand { get; }
        public Command PurchaseCommand { get; }
        public Command PurchaseCorrectionCommand { get; }
        public Command RefundCommand { get; }
        public Command IndependentRefundCommand { get; }
        public Command OpenTotalsCommand { get; }
        public Command BatchCloseCommand { get; }

        private readonly ICardVerificationService cardVerificationService;
        private readonly IPurchaseService purchaseService;
        private readonly IPurchaseCorrectionService purchaseCorrectionService;
        private readonly IRefundService refundService;
        private readonly IIndependentRefundService independentRefundService;
        private readonly IOpenTotalsService openTotalsService;
        private readonly IBatchCloseService batchCloseService;

        public PaymentViewModel(ICardVerificationService cardVerificationService, IPurchaseService purchaseService, IPurchaseCorrectionService purchaseCorrectionService, IRefundService refundService, IIndependentRefundService independentRefundService, IOpenTotalsService openTotalsService, IBatchCloseService batchCloseService)
        {

            this.cardVerificationService = cardVerificationService;
            this.purchaseService = purchaseService;
            this.purchaseCorrectionService = purchaseCorrectionService;
            this.refundService = refundService;
            this.independentRefundService = independentRefundService;
            this.openTotalsService = openTotalsService;
            this.batchCloseService = batchCloseService;

            CardVerificationCommand = new Command(async () => await CardVerification());
            PurchaseCommand = new Command(async () => await Purchase());
            PurchaseCorrectionCommand = new Command(async () => await PurchaseCorrection());
            RefundCommand = new Command(async () => await Refund());
            IndependentRefundCommand = new Command(async () => await IndependentRefund());
            OpenTotalsCommand = new Command(async () => await OpenTotals());
            BatchCloseCommand = new Command(async () => await BatchClose());
            
        }



        private async Task CardVerification()
        {
            await cardVerificationService.VerifyCard();
        }

        private async Task Purchase()
        {
            await purchaseService.Purchase();
        }

        private async Task PurchaseCorrection()
        {
            await purchaseCorrectionService.CorrectPurchase();
        }

        private async Task Refund()
        {
            await refundService.Refund();
        }

        private async Task IndependentRefund()
        {
            await independentRefundService.IndependentRefundToAnotherAccount();
        }

        private async Task OpenTotals()
        {
            await openTotalsService.GetOpenTotals();
        }

        private async Task BatchClose()
        {
            await batchCloseService.BatchCloseAllTransactions();
        }   


    }
}
