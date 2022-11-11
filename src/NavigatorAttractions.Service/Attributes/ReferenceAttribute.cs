namespace NavigatorAttractions.Service.Attributes
{
    public class ReferenceAttribute : System.Attribute
    {
        internal ReferenceAttribute(string fullName)
        {
            FullName = fullName;
        }

        internal ReferenceAttribute(string fullName, string controlType)
        {
            FullName = fullName;
            ControlType = controlType;
        }

        public string FullName { get; private set; }

        public string ControlType { get; private set; }
    }
}
