using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.UserInterface
{

	// TODO: Interface

	/// <summary>
	/// Interaction logic for <see cref="ManageGrades.xaml"/>.
	/// </summary>
	internal sealed partial class ManageGrades : Window
	{

		private BusinessLogic.BusinessObjectProcessor<Subject, Teacher> businessObjectParentProcessor = new BusinessLogic.SubjectProcessor();
		private BusinessLogic.BusinessObjectProcessor<Grade, Subject> businessObjectProcessor = new BusinessLogic.GradeProcessor();


		internal ManageGrades()
		{
			InitializeComponent();

			Common.ExtendWindowName(this);
		}

		#region Interface

		//TODO NBI: businessObjectParentProcessor.Get(); implement

		private Subject SelectedBusinessObjectParent { get { return (Subject)cbSubjects.SelectedItem; } }

		private void UpdateBusinessObjectParents()
		{
			IEnumerable<Subject> businessObjectParents = businessObjectParentProcessor.Get();

			cbSubjects.ItemsSource = businessObjectParents;
			if (ApplicationPropertyManager.Workspace.Subject != null)
			{
				cbSubjects.SelectedValue = ApplicationPropertyManager.Workspace.Subject.Id;
			}
			else
			{
				cbSubjects.SelectedIndex = 0;
			}
		}

		private void UpdateBusinessObjects()
		{
			IEnumerable<Grade> businessObjects = businessObjectProcessor.Get();

			if (businessObjects.Count() > 0)
			{
				lbSchools.ItemsSource = businessObjects;
			}
			else
			{
				ListBoxItem noBusinessObjectsPlaceholder = new ListBoxItem()
				{
					// TODO:
					Content = "Keine Noten gefunden.\nKlicken Sie auf \"Hinzufügen\" um eine neue Noten zu erstellen.",
					FontSize = 10,
					IsEnabled = false
				};
				lbSchools.ItemsSource = new List<ListBoxItem>() { noBusinessObjectsPlaceholder };
			}
		}


		private BusinessObjectManagerAction OpenAddBusinessObjectManager()
		{
			// TODO:
			Window addBusinessObjectManager = new ManageGrade() { Owner = this };
			addBusinessObjectManager.ShowDialog();
			return ((IBusinessObjectManager)addBusinessObjectManager).Action;
		}

		private BusinessObjectManagerAction OpenEditBusinessObjectManager()
		{
			// TODO:
			Window editBusinessObjectManager = new ManageGrade((Grade)lbSchools.SelectedItem) { Owner = this };
			//editBusinessObjectManager.ShowDialog();
			return ((IBusinessObjectManager)editBusinessObjectManager).Action;
		}

		private bool CheckForElementDeletion()
		{
			// TODO:
			if (Common.AskForElementDeletion("Wollen Sie diese Schule und alle zugehörigen Daten (Lehrer, Fächer, Ereignisse, Dokumente, Notizen und Graden) wirklich löschen?", "Schule"))
			{
				businessObjectProcessor.Delete((Grade)lbSchools.SelectedItem);
				return true;
			}

			return false;
		}

		private void UpdateControlsAvailability()
		{
			bool isBusinessObjectSelected = lbSchools.SelectedIndex != -1;

			foreach (Button button in new Button[] { btnEdit, btnDelete })
			{
				button.IsEnabled = isBusinessObjectSelected;
			}
		}

		#endregion

		#region Event wiring

		private void wManageGrades_Loaded(object sender, RoutedEventArgs e)
		{
			UpdateBusinessObjects();
			UpdateControlsAvailability();
		}


		// TODO:
		private void lbSchools_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UpdateControlsAvailability();
		}


		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			if (OpenAddBusinessObjectManager() == BusinessObjectManagerAction.Saved)
			{
				UpdateBusinessObjects();
			}
		}

		private void btnEdit_Click(object sender, RoutedEventArgs e)
		{
			if (OpenEditBusinessObjectManager() == BusinessObjectManagerAction.Saved)
			{
				UpdateBusinessObjects();
			}
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			if (CheckForElementDeletion())
			{
				UpdateBusinessObjects();
			}
		}

		#endregion

	}

}
