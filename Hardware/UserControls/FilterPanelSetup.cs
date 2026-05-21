using Timer = System.Windows.Forms.Timer;

namespace Hardware.UserControls
{
    public static class FilterPanelSetup
    {
        public static void AddFilter(TableLayoutPanel parent,
                                     string labelText,
                                     string tag,
                                     Timer timer,
                                     List<TextBox> filters)
        {
            Label label = new()
            {
                Anchor = AnchorStyles.Left,
                TextAlign = ContentAlignment.MiddleLeft,
                Text = $"{labelText}:",
                AutoSize = true
            };

            TextBox textBox = new()
            {
                Width = 150,
                Tag = tag
            };
            textBox.TextChanged += (sender, e) =>
            {
                timer.Stop();
                timer.Start();
            };

            int rowIndex = parent.RowCount++;
            parent.RowStyles.Add(new(SizeType.AutoSize));

            parent.Controls.Add(label, 0, rowIndex);
            parent.Controls.Add(textBox, 1, rowIndex);

            filters.Add(textBox);
        }

        public static void AddFilter(TableLayoutPanel parent, ref CheckBox checkBox, string labelText, ref DateTimePicker dtp, Action action)
        {
            checkBox = new()
            {
                Anchor = AnchorStyles.Left,
                TextAlign = ContentAlignment.MiddleLeft,
                Text = $"{labelText}:",
                AutoSize = true
            };
            checkBox.CheckedChanged += (sender, e) => action();

            dtp = new() { Format = DateTimePickerFormat.Short };
            dtp.ValueChanged += (sender, e) => action();

            int rowIndex = parent.RowCount++;
            parent.RowStyles.Add(new(SizeType.AutoSize));

            parent.Controls.Add(checkBox, 0, rowIndex);
            parent.Controls.Add(dtp, 1, rowIndex);
        }
    }
}
