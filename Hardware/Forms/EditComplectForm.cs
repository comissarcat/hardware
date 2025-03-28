using Hardware.Models;

namespace Hardware.Forms
{
	public partial class EditComplectForm : Form
	{
		private readonly ApplicationContext context;
		Complect? complect;
		public EditComplectForm(ApplicationContext context, Complect? complect, Cabinet cabinet)
		{
			InitializeComponent();
			this.context = context;
			DialogResult = DialogResult.Cancel;
			buildingCBox.DataSource = context.Buildings.ToList();
			buildingCBox.DisplayMember = "Name";
			this.complect = complect;
			if (this.complect is not null)
			{
				idTBox.Text = this.complect.Id.ToString();
				nameTBox.Text = this.complect.Name;
				editBtn.Enabled = true;
				cabinet = complect.Cabinet;
				var complectDevices = context.Devices.Where(d => d.Complect == complect).ToList();
				if (complectDevices.Count == 0)
					removeBtn.Enabled = true;
			}
			buildingCBox.SelectedItem = cabinet.Building;
			cabinetCBox.SelectedItem = cabinet;
		}
	}
}
