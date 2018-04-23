Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess

Namespace Dashboard_CreateCards
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub
		Private Function CreateCards(ByVal dataSource As DataSource) As CardDashboardItem

			' Creates a card dashboard item and specifies its data source.
			Dim cards As New CardDashboardItem()
			cards.DataSource = dataSource

            ' Creates a Card object with measures that provide data for calculating the actual and target
            ' values, and then adds this object to the Cards collection of the card dashboard item.
			Dim card As New Card()
			card.ActualValue = New Measure("Extended Price", SummaryType.Average)
			card.TargetValue = New Measure("Extended Price", SummaryType.Max)
			cards.Cards.Add(card)

			' Specifies the dimension that provides data for a card dashboard item series.
			cards.SeriesDimensions.Add(New Dimension("Sales Person"))

			Return cards
		End Function
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

			' Creates a dashboard and sets it as the currently opened dashboard in the dashboard viewer.
			dashboardViewer1.Dashboard = New Dashboard()

			' Creates a data source and adds it to the dashboard data source collection.
			Dim dataSource As New DataSource("Sales Person")
			dashboardViewer1.Dashboard.DataSources.Add(dataSource)

			' Creates a card dashboard item with the specified data source and
			' adds it to the Items collection to display within the dashboard.
			Dim cards As CardDashboardItem = CreateCards(dataSource)
			dashboardViewer1.Dashboard.Items.Add(cards)

			' Reloads data in the data sources.
			dashboardViewer1.ReloadData()
		End Sub
        Private Sub dashboardViewer1_DataLoading(ByVal sender As Object, _
                    ByVal e As DataLoadingEventArgs) Handles dashboardViewer1.DataLoading

            ' Specifies data for the current data source.
            e.Data = (New nwindDataSetTableAdapters.SalesPersonTableAdapter()).GetData()
        End Sub
	End Class
End Namespace
