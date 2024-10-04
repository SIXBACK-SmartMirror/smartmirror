namespace SmartMirror.Helpers
{
    internal class FindForm
    {
        public Form findForm(string openFormName)
        {
            Form openForm = Application.OpenForms[openFormName] as Form;
            return openForm;
        }
    }
}