using System.ComponentModel;

namespace Hardware
{
    public class SortableBindingList<T> : BindingList<T>
    {
        private bool _isSorted;
        private ListSortDirection _sortDirection;
        private PropertyDescriptor _sortProperty;

        public SortableBindingList() { }

        public SortableBindingList(IList<T> list) : base(list) { }

        protected override bool SupportsSortingCore => true;
        protected override bool IsSortedCore => _isSorted;
        protected override ListSortDirection SortDirectionCore => _sortDirection;
        protected override PropertyDescriptor SortPropertyCore => _sortProperty;

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            _sortProperty = prop;
            _sortDirection = direction;

            var items = this.Items as List<T>;
            if (items == null) return;

            var propertyInfo = typeof(T).GetProperty(prop.Name);
            if (propertyInfo == null) return;

            if (direction == ListSortDirection.Ascending)
                items.Sort((x, y) => CompareValues(propertyInfo.GetValue(x), propertyInfo.GetValue(y)));
            else
                items.Sort((x, y) => CompareValues(propertyInfo.GetValue(y), propertyInfo.GetValue(x)));

            _isSorted = true;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        private int CompareValues(object x, object y)
        {
            // Обработка null значений
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            // Сравнение для DateOnly
            if (x is DateOnly dateX && y is DateOnly dateY)
                return dateX.CompareTo(dateY);

            // Сравнение для строк
            if (x is string strX && y is string strY)
                return string.Compare(strX, strY, StringComparison.CurrentCulture);

            // Сравнение для чисел
            if (x is IComparable comparable)
                return comparable.CompareTo(y);

            // По умолчанию
            return x.ToString().CompareTo(y.ToString());
        }

        protected override void RemoveSortCore()
        {
            _isSorted = false;
            _sortProperty = null;
        }

        protected override bool SupportsSearchingCore => true;

        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            var items = this.Items as List<T>;
            if (items == null) return -1;

            var propertyInfo = typeof(T).GetProperty(prop.Name);
            if (propertyInfo == null) return -1;

            for (int i = 0; i < items.Count; i++)
            {
                var value = propertyInfo.GetValue(items[i]);
                if (value != null && value.Equals(key))
                    return i;
            }

            return -1;
        }
    }
}
