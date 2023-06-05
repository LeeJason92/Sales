namespace FinanceServices.Models
{
    public class ModelHelper
    {

        public static void CopyProperty(TInvoice source, ref TInvoice target)
        {
            var sProp = source.GetType().GetProperties().ToList();
            foreach (var tProp in target.GetType().GetProperties())
            {
                var eqProp = sProp.Find(m => m.Name == tProp.Name & m.PropertyType == tProp.PropertyType);
                if (eqProp != null & tProp.CanWrite)
                    tProp.SetValue(target, eqProp.GetValue(source));
            }
        }
    }
}
