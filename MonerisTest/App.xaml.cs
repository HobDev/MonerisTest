namespace MonerisTest
{
    public partial class App : Application
    {

        public static PaymentRepository<Entity> PaymentRepo { get; set; }

        public App( PaymentRepository<Entity> paymentRepo)
        {
            InitializeComponent();

            PaymentRepo = paymentRepo; 

         
            MainPage = new AppShell();
        }

      
    }
}
