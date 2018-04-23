using System;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess;

namespace Dashboard_CreateCards {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        private CardDashboardItem CreateCards(DashboardObjectDataSource dataSource) {

            // Creates a card dashboard item and specifies its data source.
            CardDashboardItem cards = new CardDashboardItem();
            cards.DataSource = dataSource;

            // Creates a Card object with measures that provide data for calculating the actual and
            // target values. Then, adds this object to the Cards collection of the card dashboard item.
            Card card = new Card();            
            card.ActualValue = new Measure("Extended Price", SummaryType.StdDev);
            card.TargetValue = new Measure("Extended Price", SummaryType.StdDevp);            
            cards.Cards.Add(card);

            // Specifies the dimension that provides data for card dashboard item series.
            cards.SeriesDimensions.Add(new Dimension("Sales Person"));
            // Specifies the dimension used to provide sparkline argument values.
            cards.SparklineArgument = new Dimension("OrderDate", DateTimeGroupInterval.MonthYear);

            return cards;
        }
        private void Form1_Load(object sender, EventArgs e) {

            // Creates a dashboard and sets it as a currently opened dashboard in the dashboard viewer.
            dashboardViewer1.Dashboard = new Dashboard();

            // Creates a data source and adds it to the dashboard data source collection.
            DashboardObjectDataSource dataSource = new DashboardObjectDataSource();
            dataSource.DataSource = (new nwindDataSetTableAdapters.SalesPersonTableAdapter()).GetData();
            dashboardViewer1.Dashboard.DataSources.Add(dataSource);

            // Creates a card dashboard item with the specified data source and
            // adds it to the Items collection to display within the dashboard.
            CardDashboardItem cards = CreateCards(dataSource);
            dashboardViewer1.Dashboard.Items.Add(cards);

            // Reloads data in the data sources.
            dashboardViewer1.ReloadData();
        }
    }
}
