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
    }
}
