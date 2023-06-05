namespace PricingServices.Models
{
    public class ModelHelper
    {
        public static void CopyPricingProperty(MPricing source, ref MPricing target)
        {
            var sProp = source.GetType().GetProperties().ToList();
            foreach (var tProp in target.GetType().GetProperties())
            {
                var eqProp = sProp.Find(m => m.Name == tProp.Name & m.PropertyType == tProp.PropertyType);
                if (eqProp.Name == "ID" || eqProp.Name == "Id" || eqProp.Name == "id" )
                {
                    continue;
                }
                if (eqProp != null & tProp.CanWrite)
                    tProp.SetValue(target, eqProp.GetValue(source));
            }
        }
    }
}
